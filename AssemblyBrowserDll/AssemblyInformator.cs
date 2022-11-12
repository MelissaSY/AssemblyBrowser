using System.Reflection;

namespace AssemblyBrowserDll
{
    public class AssemblyInformator
    {
        public string? assemblyName;
        public List<NamespaceInformator> namespaces { get; }
        public List<TypeInformator> NoNamespaceTypes { get; }
        private string _exceptionMessage;
        public string ExceptionMessage { get { return _exceptionMessage; } }
        
        public AssemblyInformator(string path):this()
        {
            ChangeAssembly(path);
        }
        public AssemblyInformator()
        {
            namespaces = new List<NamespaceInformator>();
            NoNamespaceTypes = new List<TypeInformator>();
            _exceptionMessage = "";
        }
        public void ChangeAssembly(string path)
        {
            try
            {
                Assembly assembly = Assembly.LoadFile(path);
                assemblyName = assembly.GetName().Name;
                Dictionary<string, List<TypeInformator>> namespaceTypes = new Dictionary<string, List<TypeInformator>>();
                Dictionary<Type, TypeInformator> Types = new Dictionary<Type, TypeInformator>();
                List<Type> types = assembly.GetTypes().ToList();
                foreach(Type type in types)
                {
                    Types.Add(type, new TypeInformator(type));
                }
                foreach (KeyValuePair <Type, TypeInformator> type in Types)
                {
                    var extensionMethods = type.Value.GetExtensionMethods();
                    foreach(KeyValuePair<Type, List<MethodInformator>> typeExtensions in extensionMethods)
                    {
                        if (Types.ContainsKey(typeExtensions.Key))
                        {
                            Types[typeExtensions.Key].AddExtensionMethod(typeExtensions.Value);
                        }
                    }
                }
                foreach (KeyValuePair<Type, TypeInformator> type in Types)
                {
                    if(type.Key.Namespace != null)
                    {
                        if (!namespaceTypes.ContainsKey(type.Key.Namespace))
                        {
                            namespaceTypes.Add(type.Key.Namespace, new List<TypeInformator>());
                        }
                        namespaceTypes[type.Key.Namespace].Add(type.Value);
                    }
                    else
                    {
                        NoNamespaceTypes.Add(type.Value);
                    }
                }
                foreach(KeyValuePair<string, List<TypeInformator>> Namespace in namespaceTypes)
                {
                    namespaces.Add(new NamespaceInformator(Namespace.Key, Namespace.Value));
                }
            }
            catch (Exception e) 
            {
                _exceptionMessage = e.Message;
            }
        }
    }
}
