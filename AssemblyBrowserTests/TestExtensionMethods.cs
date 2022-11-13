namespace AssemblyBrowserTests
{
    public class A<T, L>
    {

    }
    public enum TestEnum
    {
        TEST_1,
        TEST_2
    }
    public struct TestStruct
    {
        public int Value;
        private TestEnum test;
    }
    public static class TestExtensionMethods
    {
        public static string GetFullname(this TestNameClass testName)
        {
            return testName.Surname + " " + testName.Name + " " + testName.Patronimic;
        }
        public static void TestMethod(this TestNameClass testName, int a)
        {

        }
        private static readonly (int, char, decimal, string, byte, bool, uint, long, double, float, DateTime)[]? values;
    }
    public class TestPrivate
    {
        private string someLine = "";
        private class NestedClass { };

    }
    public class TestNameClass
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronimic { get; set; }
        public TestNameClass(string name, string surname, string patronimic)
        {
            Name = name;
            Surname = surname;
            Patronimic = patronimic;
        }
        public TestNameClass(string name, string surname)
        {
            Name = name;
            Surname = surname;
            Patronimic = "";
        }
    }
}
