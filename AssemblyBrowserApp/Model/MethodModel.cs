using AssemblyBrowserDll;
using System;
using System.Reflection;

namespace AssemblyBrowserApp.Model
{
    public class MethodModel : ModelNode
    {
        public MethodModel(MethodInformator informator)
        {
            ImagePath = "Method.png";
            if (informator.IsExtension)
            {
                ImagePath = "ExtensionMethod.png";
            }
            else if (informator.Method.IsPrivate)
            {
                ImagePath = "MethodPrivate.png";
            }
            else if (informator.Method.IsAssembly)
            {
                ImagePath = "MethodInternal.png";
            }
            else if (informator.Method.IsFamily)
            {
                ImagePath = "MethodProtected.png";
            }
            NodeResult = ComposeSignature(informator);
        }
        private string ComposeSignature(MethodInformator informator)
        {
            string parametersType = "";
            ParameterInfo[] parameters = informator.GetActualParameters();
            string[] Parameters = new string[parameters.Length];
            string result = $"{InformatorModel.GetGeneric(informator.Method.ReturnType)} {informator.Method.Name}";
            for (int i = 0; i < parameters.Length; i++)
            {
                Parameters[i] = InformatorModel.GetGeneric(parameters[i].ParameterType);
                if (parameters[i].ParameterType.IsByRef)
                {
                    Parameters[i] = Parameters[i].Replace("&", "");
                    if (parameters[i].IsIn)
                    {
                        Parameters[i] = Parameters[i].Insert(0, "in ");
                    }
                    else if (parameters[i].IsOut)
                    {
                        Parameters[i] = Parameters[i].Insert(0, "out ");
                    }
                    else
                    {
                        Parameters[i] = Parameters[i].Insert(0, "ref ");
                    }

                }
            }
            if (Parameters.Length > 0)
            {
                parametersType = Parameters[0];
                for (int i = 1; i < Parameters.Length; i++)
                {
                    parametersType += $", {Parameters[i]}";
                }
            }
            if (informator.Method.IsGenericMethod)
            {
                Type[] genericArguments = informator.Method.GetGenericArguments();

                result += $"<{genericArguments[0].Name}";
                for (int i = 1; i < genericArguments.Length; i++)
                {
                    result += $", {genericArguments[i].Name}";
                }
                result += ">";
            }
            return $"{result}({parametersType})";
        }
    }
}
