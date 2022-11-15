using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserDll
{
    public abstract class MemberInformator
    {
        public MemberInfo Member { get; protected set; }

    }
}
