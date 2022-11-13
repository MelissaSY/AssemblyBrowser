using System.Reflection;

namespace AssemblyBrowserDll
{
    public class FieldInformator
    {
        public FieldInfo Field { get; }
        public FieldInformator(FieldInfo field)
        {
            Field = field;
        }
        public override string ToString()
        {
            return $"{Field.FieldType} {Field.Name}";
        }
    }
}
