using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowserDll
{
    public class AssemblyInformator
    {
        private string? _assemblyName;
        public string? AssemblyName
        {
            get { return _assemblyName; }
        }
        public ReadOnlyCollection<NamespaceInformator> Namespaces { get; }
        public ReadOnlyCollection<TypeInformator> NoNamespaceTypes { get; }
        private Exception? _exception;
        public Exception? ExceptionMessage { get { return _exception; } }

        public AssemblyInformator(string path)
        {
            _exception = null;
            (var namespaces, var noNamespaceTypes) = ChangeAssembly(path);

            this.Namespaces = new ReadOnlyCollection<NamespaceInformator>(namespaces);
            this.NoNamespaceTypes = new ReadOnlyCollection<TypeInformator>(noNamespaceTypes);

        }
        public (List<NamespaceInformator>, List<TypeInformator>) ChangeAssembly(string path)
        {
            List<NamespaceInformator> namespaces = new List<NamespaceInformator>();
            List<TypeInformator> noNamespaceTypes = new List<TypeInformator>();
            try
            {
                Assembly assembly = Assembly.LoadFile(path);
                _assemblyName = assembly.GetName().Name;
                Dictionary<string, List<TypeInformator>> namespaceTypes = new Dictionary<string, List<TypeInformator>>();
                Dictionary<Type, TypeInformator> Types = new Dictionary<Type, TypeInformator>();
                List<Type> types = assembly.GetTypes().ToList();
                foreach (Type type in types)
                {
                    if(!IsCompilerGenerated(type))
                    {
                        Types.Add(type, new TypeInformator(type));
                    }
                }

                Dictionary<Type, TypeInformator> NewTypes = new Dictionary<Type, TypeInformator>(Types);
                foreach (KeyValuePair<Type, TypeInformator> type in Types)
                {
                    var extensionMethods = type.Value.GetExtensionMethods();
                    foreach (KeyValuePair<Type, List<MethodInformator>> typeExtensions in extensionMethods)
                    {
                        if (!Types.ContainsKey(typeExtensions.Key))
                        {
                            NewTypes.Add(typeExtensions.Key, new TypeInformator(typeExtensions.Key));
                        }
                        NewTypes[typeExtensions.Key].AddExtensionMethod(typeExtensions.Value);
                    }
                }
                Types = NewTypes;
                foreach (KeyValuePair<Type, TypeInformator> type in Types)
                {
                    if (type.Key.Namespace != null)
                    {
                        if (!namespaceTypes.ContainsKey(type.Key.Namespace))
                        {
                            namespaceTypes.Add(type.Key.Namespace, new List<TypeInformator>());
                        }
                        namespaceTypes[type.Key.Namespace].Add(type.Value);
                    }
                    else
                    {
                        noNamespaceTypes.Add(type.Value);
                    }
                }
                foreach (KeyValuePair<string, List<TypeInformator>> Namespace in namespaceTypes)
                {
                    namespaces.Add(new NamespaceInformator(Namespace.Key, Namespace.Value));
                }

            }
            catch (Exception e)
            {
                _exception = e;
            }
            return (namespaces, noNamespaceTypes);
        }
        
        public static bool IsCompilerGenerated(MemberInfo memberInfo)
        {
            bool isCompilerGenerated = false;
            try
            {
                isCompilerGenerated = memberInfo.IsDefined(typeof(CompilerGeneratedAttribute));
            }
            catch(Exception e) { }
            return isCompilerGenerated;
        }
    }
}
