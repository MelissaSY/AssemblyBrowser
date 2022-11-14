using AssemblyBrowserDll;
using System;
using System.Reflection;

namespace AssemblyBrowserApp.Model
{
    public class FieldModel : ModelNode
    {
        public FieldModel(FieldInformator informator)
        {
            NodeResult = InformatorModel.GetGeneric(informator.Field.FieldType) + " " + informator.Field.Name;
            ImagePath = "Images/Field.png";
            if (informator.Field.IsPrivate)
            {
                ImagePath = "Images/FieldPrivate.png";
            }
            else if (informator.Field.IsAssembly)
            {
                ImagePath = "Images/FieldInternal.png";
            }
            else if (informator.Field.IsFamily)
            {
                ImagePath = "Images/FieldProtected.png";
            }
            if (IsEnumConstant(informator.Field))
            {
                ImagePath = "Images/EnumerationItemPublic.png";
            }
        }
        private bool IsEnumConstant(FieldInfo field)
        {
            bool isEnumConstant = false;
            if(field.FieldType.IsEnum)
            {
                string[] enumConstants = field.FieldType.GetEnumNames();
                int i = 0;
                for(i= 0; i < enumConstants.Length && !enumConstants[i].Equals(field.Name); i++)
                {    }
                isEnumConstant = i < enumConstants.Length;
            }
            return isEnumConstant;
        }   
    }
}
