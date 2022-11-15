using System.Reflection;

namespace AssemblyBrowserDll
{
    public class FieldInformator : MemberInformator
    {
        public FieldInfo Field { get; }
        public FieldInformator(FieldInfo field)
        {
            Member = field;
            Field = field;
        }
    }
}
