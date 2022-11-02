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
    }
}