using AssemblyBrowserDll;

namespace AssemblyBrowserApp.Model
{
    public class AssemblyModel : ModelNode
    {
        public string ExceptionMessage { get; }
        public AssemblyModel(AssemblyInformator informator)
        {
            NodeResult = informator.AssemblyName == null ? "" : informator.AssemblyName;
            ImagePath = "Assembly.png";
            ExceptionMessage = informator.ExceptionMessage == null ? "" : informator.ExceptionMessage.Message;
            foreach (NamespaceInformator namespaceInformator in informator.Namespaces)
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
