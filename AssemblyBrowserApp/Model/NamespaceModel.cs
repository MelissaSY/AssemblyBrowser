using AssemblyBrowserDll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserApp.Model
{
    public class NamespaceModel : ModelNode
    {
        public NamespaceModel(NamespaceInformator informator)
        {
            NodeResult = informator.Namespace == null ? "" : informator.Namespace;
            ImagePath = "Images/Namespace.png";

            foreach(TypeInformator typeInformator in informator.types)
            {
                Children.Add(new TypeModel(typeInformator));
            }
        }
    }
}
