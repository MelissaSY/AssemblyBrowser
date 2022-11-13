using AssemblyBrowserDll;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Documents;

namespace AssemblyBrowserApp.Model
{
    public class TypeModel : ModelNode
    {
        public TypeModel(TypeInformator informator)
        {
            NodeResult = GetGeneric(informator.type);
            ImagePath = "Images/Class.png";
            if (informator.type.IsInterface)
            {
                ImagePath = "Images/Interface.png";
            } else if(informator.type.IsEnum)
            {
                ImagePath = "Images/EnumerationPublic.png";
            } else if(informator.type.IsValueType)
            {
                ImagePath = "Images/Structure.png";
            }
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
        /// <summary>
        /// Before passing ValueTuple type GetElementType() must be call 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Type[] GetValueTupleParameters(Type type)
        {
            List<Type> result = new List<Type>();
            FieldInfo[]? fields = type?.GetFields();
            if (fields != null)
            {
                foreach (FieldInfo field in fields)
                {
                    if(field.Name.Equals("Rest"))
                    {
                        result.AddRange(GetValueTupleParameters(field.FieldType));
                    }
                    else
                    {
                        result.Add(field.FieldType);
                    }
                }
            }
            return result.ToArray();
        }
        public static string GetGeneric(Type type)
        {
            string result = type.Name;
            
            if(result.Contains("ValueTuple`"))
            {
                Type? elementType = type.GetElementType();
                if (elementType != null)
                {
                    Type[] tupleTypes = GetValueTupleParameters(elementType);

                    result = "(";
                    result += GetGeneric(tupleTypes[0]);
                    for(int i = 1; i < tupleTypes.Length; i++)
                    {
                        result += $", {GetGeneric(tupleTypes[i])}";
                    }
                    result += ")";
                }
            }
            if (type.IsGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();
                string genericTypeName = genericType.Name;
                result = genericTypeName.Substring(0, genericTypeName.IndexOf("`"));
                result += "<";
                Type[] genericArguments = genericType.GetGenericArguments();
                result += genericArguments[0].Name;
                for(int i = 1; i < genericArguments.Length; i++)
                {
                    result += $", {genericArguments[i]}";
                }
                result += ">";
            }
            return result;
        }
    }
}
