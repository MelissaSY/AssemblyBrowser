using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowserDll
{
    public class TypeInformator
    {
        public Type type { get; }
        public ReadOnlyCollection<MethodInformator> Methods { get; }
        public ReadOnlyCollection<PropertyInformator> Properties { get; }
        public ReadOnlyCollection<FieldInformator> Fields { get; }
        public List<MethodInformator> ExtensionMethods { get; }
        private Dictionary<Type, List<MethodInformator>> _extensions;
        private BindingFlags _flags;
        public TypeInformator(Type type)
        {
            _flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public;
            this.type = type;
            ExtensionMethods = new List<MethodInformator>();
            _extensions = new Dictionary<Type, List<MethodInformator>>();

            Methods = new ReadOnlyCollection<MethodInformator>(InitializeMethods(type));
            Fields = new ReadOnlyCollection<FieldInformator>(InitializeFields(type));
            Properties = new ReadOnlyCollection<PropertyInformator>(InitializeProperties(type));
        }
        private List<MethodInformator> InitializeMethods(Type type)
        {
            List<MethodInformator> methodInformators = new List<MethodInformator>();
            MethodInfo[] methods = type.GetMethods(_flags);

            foreach (MethodInfo method in methods)
            {
                bool extensionMethod = false;
                try
                {
                    extensionMethod = method.IsDefined(typeof(ExtensionAttribute));
                }
                catch (Exception e) { };
                if (!AssemblyInformator.IsCompilerGenerated(method))
                {
                    if (extensionMethod)
                    {
                        MethodInformator methodInformator = new MethodInformator(method, extensionMethod);
                        if (methodInformator.CallingType != null)
                        {
                            if (!_extensions.ContainsKey(methodInformator.CallingType))
                            {
                                _extensions.Add(methodInformator.CallingType, new List<MethodInformator>());
                            }
                            _extensions[methodInformator.CallingType].Add(methodInformator);
                        }
                    }
                    else
                    {
                        methodInformators.Add(new MethodInformator(method, extensionMethod));
                    }
                }
            }
            return methodInformators;
        }
        private List<FieldInformator> InitializeFields(Type type)
        {
            List<FieldInformator> fieldInformators = new List<FieldInformator>();
            FieldInfo[] fields = type.GetFields(_flags);
            foreach (FieldInfo field in fields)
            {
                if (!AssemblyInformator.IsCompilerGenerated(field))
                {
                    fieldInformators.Add(new FieldInformator(field));
                }
            }
            return fieldInformators;
        }
        private List<PropertyInformator> InitializeProperties(Type type)
        {
            List<PropertyInformator> propertyInformators = new List<PropertyInformator>();
            PropertyInfo[] properties = type.GetProperties(_flags);
            foreach (PropertyInfo property in properties)
            {
                if (!AssemblyInformator.IsCompilerGenerated(property))
                {
                    propertyInformators.Add(new PropertyInformator(property));
                }
            }
            return propertyInformators;
        }
        public Dictionary<Type, List<MethodInformator>> GetExtensionMethods()
        {
            return new Dictionary<Type, List<MethodInformator>>(_extensions);
        }
        public void AddExtensionMethod(MethodInformator methodInformator)
        {
            if (methodInformator.CallingType == type && !Methods.Contains(methodInformator) && !Methods.Contains(methodInformator))
            {
                ExtensionMethods.Add(methodInformator);
            }
        }
        public void AddExtensionMethod(List<MethodInformator> methodInformators)
        {
            foreach (MethodInformator method in methodInformators)
            {
                AddExtensionMethod(method);
            }
        }
    }
}
