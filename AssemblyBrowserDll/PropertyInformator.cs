using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public override string ToString()
        {
            return $"{Property.PropertyType} {Property.Name}";
        }
    }
}
