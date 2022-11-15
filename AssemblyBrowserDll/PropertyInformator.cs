using System.Reflection;

namespace AssemblyBrowserDll
{
    public class PropertyInformator : MemberInformator
    {
        public PropertyInfo Property { get; }
        public PropertyInformator(PropertyInfo property)
        {
            Member = property;
            Property = property;
        }
    }
}
