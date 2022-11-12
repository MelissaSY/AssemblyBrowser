using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AssemblyBrowserDll;
using System.Xml.Linq;

namespace AssemblyBrowserApp.Model
{
    public class TypeModel : ModelNode
    {


        public TypeModel(TypeInformator informator)
        {
            NodeResult = informator.type.Name;
            ImagePath = "Images/Class.png";
            if (informator.type.IsInterface)
            {
                ImagePath = "Images/Interface.png";
            }
            foreach (PropertyInformator property in informator.Properties)
            {
                Children.Add(new PropertyModel(property));
            }
            foreach(FieldInformator field in informator.Fields)
            {
                Children.Add(new FieldModel(field));
            }
            foreach(MethodInformator method in informator.Methods)
            {
                Children.Add(new MethodModel(method));
            }
            foreach(MethodInformator extension in informator.ExtensionMethods)
            {
                Children.Add(new MethodModel(extension));
            }
        }
    }
}
