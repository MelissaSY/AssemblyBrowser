using AssemblyBrowserDll;
using AssemblyBrowserTests.TestNestedNamespace;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace AssemblyBrowserTests
{
    public class Tests
    {
        private AssemblyInformator _assemblyInformator;

        private Assembly _assembly;
        [SetUp]
        public void Setup()
        {
            _assembly = Assembly.GetExecutingAssembly();

            _assemblyInformator = new AssemblyInformator(Assembly.GetExecutingAssembly().Location);
        }

        [Test]
        public void Namespaces_Count_Names()
        {
            var namespaceInformators = _assemblyInformator.Namespaces;
            List<string>? namespaces = null;
            if (namespaceInformators != null)
            {
                namespaces = new List<string>();
                foreach (NamespaceInformator Namespace in namespaceInformators)
                {
                    if (Namespace.Namespace != null)
                    {
                        namespaces.Add(Namespace.Namespace);
                    }
                }
            }
            Assert.NotNull(namespaces);
            Assert.GreaterOrEqual(namespaces.Count, 2, "There should be at least 2 namespaces");
            Assert.That(namespaces.Contains("AssemblyBrowserTests"), "AssemblyBrowserTests namespce is missing");
            Assert.That(namespaces.Contains("AssemblyBrowserTests.TestNestedNamespace"), "TestNestedNamespace namespace is missing");
        }
        
        [Test]
        public void AssemblyBrowserTests_Contains_TestClasses()
        {
            int namespaceNum = GetNamespaceNum(_assemblyInformator, "AssemblyBrowserTests");
            NamespaceInformator informator = _assemblyInformator.Namespaces[namespaceNum];
            int typesNum = informator.types.Count;

            bool containsTestEnum = GetTypeNum(informator, typeof(TestEnum)) < typesNum;
            bool containsTestStruct = GetTypeNum(informator, typeof(TestStruct)) < typesNum;
            bool containsTestMethods = GetTypeNum(informator, typeof(TestMethods)) < typesNum;
            bool containsTestNestedClass = GetTypeNum(informator, typeof(TestNestedClass)) < typesNum;
            bool containsTestProperties = GetTypeNum(informator, typeof(TestProperties)) < typesNum;

            Assert.That(containsTestEnum, "TestEnum must be in AssemblyBrowserTests namespace");
            Assert.That(containsTestStruct, "TestStruct must be in AssemblyBrowserTests namespace");
            Assert.That(containsTestMethods, "TestMethods must be in AssemblyBrowserTests namespace");
            Assert.That(containsTestNestedClass, "TestNestedClass must be in AssemblyBrowserTests namespace");
            Assert.That(containsTestProperties, "TestProperties must be in AssemblyBrowserTests namespace");
        }
        [Test]
        public void TestNestedNamespace_Contains_TestInternalClass()
        {
            int namespaceNum = GetNamespaceNum(_assemblyInformator, "AssemblyBrowserTests.TestNestedNamespace");
            NamespaceInformator informator = _assemblyInformator.Namespaces[namespaceNum];
            int typesNum = informator.types.Count;

            bool containsTestInternalClass = GetTypeNum(informator, typeof(TestInternalClass)) < typesNum;
            Assert.That(containsTestInternalClass, "TestInternalClass must be in AssemblyBrowserTests namespace");
        }
        [Test]
        public void TestProperties_Class_Contains_Properties()
        {
            int namespaceNum = GetNamespaceNum(_assemblyInformator, "AssemblyBrowserTests");
            NamespaceInformator informator = _assemblyInformator.Namespaces[namespaceNum];
            int typeNum = GetTypeNum(informator, typeof(TestProperties));
            TypeInformator typeInformator = informator.types[typeNum];


            PropertyInfo PrivateSet = (typeof(TestProperties)).GetProperty("PrivateSet");
            PropertyInfo InternalGet = (typeof(TestProperties)).GetProperty("InternalGet");
            PropertyInfo ProtecteSet = (typeof(TestProperties)).GetProperty("ProtecteSet");

            bool containsPrivateSet = ContainsMember(PrivateSet, typeInformator.Properties.ToArray());
            bool containsInternalGet = ContainsMember(InternalGet, typeInformator.Properties.ToArray());
            bool containsProtecteSet = ContainsMember(ProtecteSet, typeInformator.Properties.ToArray());

            Assert.That(containsPrivateSet, "PrivateSet not found as the property of TestProperties class");
            Assert.That(containsInternalGet, "InternalGet not found as the property of TestProperties class");
            Assert.That(containsProtecteSet, "ProtecteSet not found as the property of TestProperties class");
        }
        [Test]
        public void ExtensionMethodTest()
        {
            int namespaceNum = GetNamespaceNum(_assemblyInformator, "AssemblyBrowserTests");
            NamespaceInformator informator = _assemblyInformator.Namespaces[namespaceNum];
            int typeNum = GetTypeNum(informator, typeof(TestProperties));
            TypeInformator typeInformator = informator.types[typeNum];
            MethodInfo extensionMethod = (typeof(TestMethods)).GetMethod("TestExtensionMethod");
            MethodInfo extensionMethod_2 = (typeof(TestMethods)).GetMethod("TestExtensionMethod_2");
            bool containsExtensionMethod = ContainsMember(extensionMethod, typeInformator.ExtensionMethods.ToArray());
            bool containsExtensionMethod_2 = ContainsMember(extensionMethod_2, typeInformator.ExtensionMethods.ToArray());

            Assert.That(containsExtensionMethod, "TestExtensionMethod not found in extesion methods of the type");
            Assert.That(containsExtensionMethod_2, "TestExtensionMethod_2 not found in extesion methods of the type");
        }
        [Test]
        public void AssemblyBrowserTests_Contains_NonPublicNestedClasses()
        {
            int namespaceNum = GetNamespaceNum(_assemblyInformator, "AssemblyBrowserTests");
            NamespaceInformator informator = _assemblyInformator.Namespaces[namespaceNum];
            int typesNum = informator.types.Count; 

            bool containsTestPrivateNestedClass = GetTypeNum(informator, "TestPrivateNestedClass") < typesNum;
            bool containsIPrivateNested = GetTypeNum(informator, "IPrivateNested") < typesNum;
            bool containsIProtectedNested = GetTypeNum(informator, "IProtectedNested") < typesNum;
            bool containsIInternalNested = GetTypeNum(informator, "IInternalNested") < typesNum;

            Assert.That(containsTestPrivateNestedClass, "TestPrivateNestedClass not found in AssemblyBrowserTests");
            Assert.That(containsIPrivateNested, "IPrivateNested not found in AssemblyBrowserTests");
            Assert.That(containsIProtectedNested, "IProtectedNested not found in AssemblyBrowserTests");
            Assert.That(containsIInternalNested, "IInternalNested not found in AssemblyBrowserTests");
        }
        [Test]
        public void TestProperties_Contains_NonPublicProperties()
        {
            int namespaceNum = GetNamespaceNum(_assemblyInformator, "AssemblyBrowserTests");
            NamespaceInformator informator = _assemblyInformator.Namespaces[namespaceNum];
            int typeNum = GetTypeNum(informator, "TestProperties");

            TypeInformator typeInformator = informator.types[typeNum];

            bool containsPrivate = ContainsMember(typeInformator.Properties.ToArray(), "Private", typeof(string).Name);
            bool containsInternal = ContainsMember(typeInformator.Properties.ToArray(), "Internal", typeof(string).Name);
            bool containsProtected = ContainsMember(typeInformator.Properties.ToArray(), "Protected", typeof(string).Name);
           
            Assert.That(containsPrivate, "Property Private not found in TestProperties");
            Assert.That(containsInternal, "Property Internal not found in TestProperties");
            Assert.That(containsProtected, "Property Protected not found in TestProperties");
        }
        private int GetNamespaceNum(AssemblyInformator informator, string namespaceName)
        {
            int num = 0;
            while(num < informator.Namespaces.Count &&
                informator.Namespaces[num].Namespace != namespaceName)
            {
                num++;
            }
            return num;
        }
        private int GetTypeNum(NamespaceInformator informator, Type type)
        {
            int num = 0;
            while (num < informator.types.Count &&
                informator.types[num].type.MetadataToken != type.MetadataToken)
            {
                num++;
            }
            return num;
        }
        private int GetTypeNum(NamespaceInformator informator, string name)
        {
            int num = 0;
            while (num < informator.types.Count &&
                !informator.types[num].type.Name.Equals(name))
            {
                num++;
            }
            return num;
        }
        private bool ContainsMember(MemberInfo memberInfo, MemberInformator[] members)
        {
            int num = 0;
            while (num < members.Length &&
                members[num].Member.MetadataToken != memberInfo.MetadataToken)
            {
                num++;
            }
            return num < members.Length;
        }
        private bool ContainsMember(MemberInformator[] members, string memeberName, string memberType)
        {
            int num = 0;
            while (num < members.Length &&
                members[num].Member.Name != memeberName && members[num].Member.GetType().Name != memberType)
            {
                num++;
            }
            return num < members.Length;
        }
    }
}