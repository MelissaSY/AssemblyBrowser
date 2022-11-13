using AssemblyBrowserDll;

namespace AssemblyBrowserApp.Model
{
    public class AssemblyModel : ModelNode
    {
        public string ExceptionMessage { get; }
        public AssemblyModel(AssemblyInformator informator)
        {
            NodeResult = informator.assemblyName == null ? "" : informator.assemblyName;
            ImagePath = "Images/Assembly.png";
            ExceptionMessage = informator.ExceptionMessage;
            foreach (NamespaceInformator namespaceInformator in informator.namespaces)
            {
                Children.Add(new NamespaceModel(namespaceInformator));
            }

            foreach (TypeInformator typeInformator in informator.NoNamespaceTypes)
            {
                Children.Add(new TypeModel(typeInformator));
            }
        }
    }
}
