using System.Reflection;

namespace AssemblyBrowserTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            MethodInformator methodInformator = new(this.GetType().GetMethod("Test1"));
            Assert.Pass(methodInformator.ToString());
        }
        [Test]
        public void ExtensionMethodTest()
        {
            AssemblyInformator informator = new AssemblyInformator(Assembly.GetExecutingAssembly().Location);
        }
    }
}