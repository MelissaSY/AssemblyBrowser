using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowserDll
{
    public class TypeInformator
    {
        public Type type { get; }
        public List<MethodInformator> Methods { get; }
        public List<PropertyInformator> Properties { get; }
        public List<FieldInformator> Fields { get; }
        public List<MethodInformator> ExtensionMethods { get; }
        private Dictionary<Type, List<MethodInformator>> _extensions;
        private BindingFlags _flags;
        public TypeInformator(Type type)
        {
            _flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;
            this.type = type;
            Methods = new List<MethodInformator>();
            Properties = new List<PropertyInformator>();
            Fields = new List<FieldInformator>();
            ExtensionMethods = new List<MethodInformator>();
            _extensions = new Dictionary<Type, List<MethodInformator>>();

            InitializeMethods();
            InitializeProperties();
            InitializeFields();
        }
        private void InitializeMethods()
        {
            if (type == null || Methods == null)
                return;
            MethodInfo[] methods = type.GetMethods(_flags);

            foreach (MethodInfo method in methods)
            {
                bool extensionMethod = false;
                try
                {
                    extensionMethod = method.IsDefined(typeof(ExtensionAttribute));
                }
                catch (Exception) { };
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
                    Methods.Add(new MethodInformator(method, extensionMethod));
                }
            }
        }
        private void InitializeFields()
        {
            if (type == null || Fields == null)
                return;
            FieldInfo[] fields = type.GetFields(_flags);
            foreach (FieldInfo field in fields)
            {
                Fields.Add(new FieldInformator(field));
            }
        }
        private void InitializeProperties()
        {
            if (type == null || Properties == null)
                return;
            PropertyInfo[] properties = type.GetProperties(_flags);
            foreach (PropertyInfo property in properties)
            {
                Properties.Add(new PropertyInformator(property));
            }
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
        public override string ToString()
        {
            return type.Name;
        }
    }
}
