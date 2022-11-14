namespace AssemblyBrowserDll
{
    public class NamespaceInformator
    {
        public string? Namespace { get; }
        public List<TypeInformator> types { get; }
        public NamespaceInformator(string? Namespace, List<Type> types)
        {
            this.Namespace = Namespace;
            this.types = new List<TypeInformator>();
            foreach (Type type in types)
            {
                this.types.Add(new TypeInformator(type));
            }
        }
        public NamespaceInformator(string? Namespace, List<TypeInformator> types)
        {
            this.Namespace = Namespace;
            this.types = new List<TypeInformator>(types);
        }
    }
}
