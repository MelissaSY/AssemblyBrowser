using System.Reflection;

namespace AssemblyBrowserDll
{
    public class MethodInformator
    {
        public Type? CallingType { get; }
        private ParameterInfo[] _parameters;
        public bool IsExtension { get; }
        public MethodInfo Method { get; }
        public MethodInformator(MethodInfo method, bool extensionMethod)
        {
            IsExtension = extensionMethod;
            if (extensionMethod)
            {
                CallingType = method.GetParameters()[0].ParameterType;
                _parameters = new ParameterInfo[method.GetParameters().Length - 1];
                Array.Copy(method.GetParameters(), 1, _parameters, 0, method.GetParameters().Length - 1);
            }
            else
            {
                CallingType = method.DeclaringType;
                _parameters = method.GetParameters();
            }
            Method = method;
        }
        /// <summary>
        /// for extension methods doesn't return the first parameter
        /// </summary>
        /// <returns></returns>
        public ParameterInfo[] GetActualParameters()
        {
            ParameterInfo[] parameters = new ParameterInfo[_parameters.Length];
            _parameters.CopyTo(parameters, 0);
            return parameters;
        }
        public override string ToString()
        {
            string parametersType = "";
            if (_parameters.Length > 0)
            {
                parametersType = _parameters[0].ParameterType.ToString();
                for (int i = 1; i < _parameters.Length; i++)
                {
                    parametersType += $", {_parameters[i].ParameterType}";
                }
            }
            return $"{Method.ReturnType} {Method.Name}({parametersType})";
        }
    }
}
