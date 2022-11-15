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
                ImagePath = "Interface.png";
            }
            else if (informator.type.IsEnum)
            {
                typeType = "Enumeration";
                ImagePath = "EnumerationPublic.png";
            }
            else if (informator.type.IsValueType)
            {
                typeType = "Structure";
                ImagePath = "Structure.png";
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
            ImagePath = $"{typeType}{typeAccess}.png";
            foreach (PropertyInformator property in informator.Properties)
            {
                try
                {
                    Children.Add(new PropertyModel(property));
                } catch(Exception) { }
            }
            foreach (FieldInformator field in informator.Fields)
            {
                try
                {
                    Children.Add(new FieldModel(field));
                } catch(Exception) { }
            }
            foreach (MethodInformator method in informator.Methods)
            {
                try
                {
                    Children.Add(new MethodModel(method));
                } catch(Exception) { }
            }
            foreach (MethodInformator extension in informator.ExtensionMethods)
            {
                try
                {
                    Children.Add(new MethodModel(extension));
                } catch (Exception) { }
            }
        }
    }
}
