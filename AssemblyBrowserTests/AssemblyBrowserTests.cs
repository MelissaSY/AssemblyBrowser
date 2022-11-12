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
        {        }
        [Test]
        public void ExtensionMethodTest()
        {
            AssemblyInformator informator = new AssemblyInformator(Assembly.GetExecutingAssembly().Location);
            AssemblyInformator assembly = new AssemblyInformator("D:\\5 sem\\ÑÏÏ\\ÑÏÏ\\ËÐ\\ëð1\\TracerDemonstrationApp\\bin\\Debug\\net6.0\\TracerDemonstrationApp.dll");
        }
    }
}