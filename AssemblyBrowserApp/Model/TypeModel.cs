using AssemblyBrowserDll;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowserApp.Model
{
    public class TypeModel : ModelNode
    {
        public TypeModel(TypeInformator informator)
        {
            NodeResult = InformatorModel.GetGeneric(informator.type);
            string typeType = "Class";
            string typeAccess = "Public";

            if (informator.type.IsInterface)
            {
                typeType = "Interface";
                ImagePath = "Images/Interface.png";
            }
            else if (informator.type.IsEnum)
            {
                typeType = "Enumeration";
                ImagePath = "Images/EnumerationPublic.png";
            }
            else if (informator.type.IsValueType)
            {
                typeType = "Structure";
                ImagePath = "Images/Structure.png";
            }
            if (informator.type.IsNestedFamily)
            {
                typeAccess = "Protected";
            }
            else if (informator.type.IsNestedPrivate)
            {
                typeAccess = "Private";
            }
            else if (informator.type.IsNestedAssembly || informator.type.IsNotPublic && informator.type.Namespace != null)
            {
                typeAccess = "Internal";
            }
            ImagePath = $"Images/{typeType}{typeAccess}.png";
            foreach (PropertyInformator property in informator.Properties)
            {
                Children.Add(new PropertyModel(property));
            }
            foreach (FieldInformator field in informator.Fields)
            {
                Children.Add(new FieldModel(field));
            }
            foreach (MethodInformator method in informator.Methods)
            {
                Children.Add(new MethodModel(method));
            }
            foreach (MethodInformator extension in informator.ExtensionMethods)
            {
                Children.Add(new MethodModel(extension));
            }
        }
    }
}
