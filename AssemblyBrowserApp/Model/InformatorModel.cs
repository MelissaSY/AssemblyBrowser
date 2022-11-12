using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowserDll;
using System.Collections.ObjectModel;

namespace AssemblyBrowserApp.Model
{
    public class InformatorModel
    {
        public AssemblyModel AssemblyModel { get; }
        public InformatorModel(string path)
        {
            AssemblyModel = new AssemblyModel(new AssemblyInformator(path));
        }
    }
}
