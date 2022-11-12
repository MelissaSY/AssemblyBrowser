using AssemblyBrowserDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserApp.Model
{
    public class FieldModel : ModelNode
    {
        public FieldModel(FieldInformator informator)
        {
            NodeResult = informator.Field.FieldType + " " + informator.Field.Name;
            ImagePath = "Images/Field.png";
            if(informator.Field.IsPrivate)
            {
                ImagePath = "Images/FieldPrivate.png";
            } else if(informator.Field.IsAssembly)
            {
                ImagePath = "Images/FieldInternal.png";
            } else if(informator.Field.IsFamily)
            {
                ImagePath = "Images/FieldProtected.png";
            }
        }
    }
}
