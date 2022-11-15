using AssemblyBrowserDll;
using System.Reflection;

namespace AssemblyBrowserApp.Model
{
    public class PropertyModel : ModelNode
    {
        public PropertyModel(PropertyInformator informator)
        {
            NodeResult = InformatorModel.GetGeneric(informator.Property.PropertyType) + " " + informator.Property.Name;
            string getter = GetGetter(informator.Property);
            string setter = GetSetter(informator.Property);
            string getSet = "";
            if(!getter.Equals(""))
            {
                getSet += getter;
            }
            if(!setter.Equals(""))
            {
                getSet += setter;
            }
            if(!getSet.Equals(""))
            {
                NodeResult += " {" + getSet + "}";
            }
            ImagePath = "Property.png";
            if(getter.Contains("private") && setter.Contains("private") || 
                getter.Contains("private") && setter.Equals(""))
            {
                ImagePath = "PropertyPrivate.png";
                NodeResult = NodeResult.Replace("private ", "");
            } 
            else if (getter.Contains("protected") && setter.Contains("protected") ||
                getter.Contains("protected") && setter.Equals(""))
            {
                ImagePath = "PropertyProtected.png";
                NodeResult = NodeResult.Replace("protected ", "");
            }
            else if (getter.Contains("internal") && setter.Contains("internal") ||
                getter.Contains("internal") && setter.Equals(""))
            {
                ImagePath = "PropertyInternal.png";
                NodeResult = NodeResult.Replace("internal ", "");
            }
        }
        private string GetEtter(MethodInfo? etter)
        {
            string method = "";
            if (etter != null)
            {
                if (etter.IsPublic)
                {
                    method = " ";
                }
                else if (etter.IsPrivate)
                {
                    method = "private ";
                }
                else if (etter.IsAssembly)
                {
                    method = "internal ";
                }
                else if (etter.IsFamily)
                {
                    method = "protected ";
                }
            }
            return method;
        }
        private string GetGetter(PropertyInfo property)
        {
            string getter = GetEtter(property.GetMethod);
            if (!getter.Equals(""))
            {
                getter += "get; ";
            }
            return getter;
        }
        private string GetSetter(PropertyInfo property)
        {
            string setter = GetEtter(property.SetMethod);
            if (!setter.Equals(""))
            {
                setter += "set; ";
            }
            return setter;
        }
    }
}
