using AssemblyBrowserDll;

namespace AssemblyBrowserApp.Model
{
    public class NamespaceModel : ModelNode
    {
        public NamespaceModel(NamespaceInformator informator)
        {
            NodeResult = informator.Namespace == null ? "" : informator.Namespace;
            ImagePath = "Images/Namespace.png";

            foreach (TypeInformator typeInformator in informator.types)
            {
                Children.Add(new TypeModel(typeInformator));
            }
        }
    }
}
