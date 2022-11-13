using AssemblyBrowserDll;

namespace AssemblyBrowserApp.Model
{
    public class PropertyModel : ModelNode
    {
        public PropertyModel(PropertyInformator informator)
        {
            NodeResult = TypeModel.GetGeneric(informator.Property.PropertyType) + " " + informator.Property.Name;
            ImagePath = "Images/Property.png";
        }
    }
}
