using AssemblyBrowserDll;
using System.Reflection;

namespace AssemblyBrowserApp.Model
{
    public class MethodModel : ModelNode
    {
        public MethodModel(MethodInformator informator)
        {
            ImagePath = "Images/Method.png";
            if (informator.IsExtension)
            {
                ImagePath = "Images/ExtensionMethod.png";
            }
            else if (informator.Method.IsPrivate)
            {
                ImagePath = "Images/MethodPrivate.png";
            }
            else if (informator.Method.IsAssembly)
            {
                ImagePath = "Images/MethodInternal.png";
            }
            else if (informator.Method.IsFamily)
            {
                ImagePath = "Images/MethodProtected.png";
            }
            NodeResult = ComposeSignature(informator);
        }
        private string[] GetParameters(ParameterInfo[] parameters)
        {
            string[] stringParameters = new string[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                stringParameters[i] = TypeModel.GetGeneric(parameters[i].ParameterType);
            }
            return stringParameters;
        }
        private string ComposeSignature(MethodInformator informator)
        {
            string parametersType = "";
            var Parameters = GetParameters(informator.GetActualParameters());
            if (Parameters.Length > 0)
            {
                parametersType = Parameters[0];
                for (int i = 1; i < Parameters.Length; i++)
                {
                    parametersType += $", {Parameters[i]}";
                }
            }
            return $"{TypeModel.GetGeneric(informator.Method.ReturnType)} {informator.Method.Name}({parametersType})";
        }
    }
}
