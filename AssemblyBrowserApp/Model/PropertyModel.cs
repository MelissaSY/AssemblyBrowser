using AssemblyBrowserDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserApp.Model
{
    public class PropertyModel : ModelNode
    {
        public PropertyModel(PropertyInformator informator)
        {
            NodeResult = informator.Property.PropertyType +" "+ informator.Property.Name;
            ImagePath = "Images/Property.png";
        }
    }
}
