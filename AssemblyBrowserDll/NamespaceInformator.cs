using System.Collections.ObjectModel;

namespace AssemblyBrowserDll
{
    public class NamespaceInformator
    {
        public string? Namespace { get; }
        public ReadOnlyCollection<TypeInformator> types { get; }
        public NamespaceInformator(string? Namespace, List<Type> types)
        {
            this.Namespace = Namespace;
            List<TypeInformator> Types = new List<TypeInformator>();
            foreach (Type type in types)
            {
                Types.Add(new TypeInformator(type));
            }
            this.types = new ReadOnlyCollection<TypeInformator>(Types);
        }
        public NamespaceInformator(string? Namespace, List<TypeInformator> types)
        {
            this.Namespace = Namespace;
            this.types = new ReadOnlyCollection<TypeInformator>(types);
        }
    }
}
