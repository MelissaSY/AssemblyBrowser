using AssemblyBrowserDll;

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
