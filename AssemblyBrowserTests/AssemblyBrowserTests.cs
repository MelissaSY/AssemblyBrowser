using AssemblyBrowserDll;
using System.Reflection;

namespace AssemblyBrowserTests
{
    public class Tests
    {
        private AssemblyInformator _assemblyInformator;
        private Dictionary<string, Dictionary<Type, List<MemberInfo>>> _assemblyMap;
        private List<NamespaceInformator> _namespaceInformators;
       /* private TypeInformator[] _typeInformators;
        private FieldInformator[] _fieldInformators;
        private PropertyInformator[] _propertyInformators;
        private MethodInformator[] _methodInformators;*/

        private Assembly _assembly;
        [SetUp]
        public void Setup()
        {
            _assembly = Assembly.GetExecutingAssembly();

            _assemblyInformator = new AssemblyInformator(Assembly.GetExecutingAssembly().Location);
            _namespaceInformators = _assemblyInformator.Namespaces.ToList();
            /*for(int i = 0; i < _namespaceInformators.Length; i++)
            {

            }*/
        }

        [Test]
        public void Namespaces_Count_Names()
        {
            List<string> namespaces = new List<string>();
            foreach(NamespaceInformator Namespace in _namespaceInformators)
            {
                if(Namespace.Namespace != null)
                {
                    namespaces.Add(Namespace.Namespace);
                }
            }
            Assert.GreaterOrEqual(namespaces.Count, 2, "There should be at least 2 namespaces");
            Assert.IsFalse(namespaces.Contains("AssemblyBrowserTests"), "AssemblyBrowserTests namespce is missing");
            Assert.IsFalse(namespaces.Contains("TestNestedNamespace"), "TestNestedNamespace namespce is missing");
        }
        [Test]
        public void Namespaces_ContainTestTypes()
        {

        }
        [Test]
        public void TestGenericClass_TField_LField_AssemblyBrowserTestsNamespace()
        {
            Type? type = _assembly.GetType("TestGenericClass");
            FieldInfo?[] fields = new FieldInfo[2];
            if (type != null)
            {
                fields[0] = type.GetField("TestFieldT");
                fields[1] = type.GetField("TestFieldL");
            }

        }
        [Test]
        public void ExtensionMethodTest()
        {

        }
    }
}