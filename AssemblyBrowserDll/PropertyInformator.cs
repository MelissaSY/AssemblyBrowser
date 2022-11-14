using System.Reflection;

namespace AssemblyBrowserDll
{
    public class PropertyInformator
    {
        public PropertyInfo Property { get; }
        public PropertyInformator(PropertyInfo property)
        {
            Property = property;
        }
    }
}
