namespace AssemblyBrowserTests
{
    public class TestGenericClass<T, L>
    {
        public T TestFieldT;
        public L TestFieldL;
        public TestGenericClass(T TType, L LType)
        {
            TestFieldT = TType;
            TestFieldL = LType;
        }
    }
    public enum TestEnum
    {
        TEST_1,
        TEST_2,
    }
    public struct TestStruct
    {
        public int Value;
        private TestEnum test;
    }
    public static class TestMethods
    {
        public static string GetFullname(this TestProperties testName)
        {
            return testName.InternalGet + " " + testName.PrivateSet + " " + testName.ProtecteSet;
        }
        public static void TestExtensionMethod(this TestProperties testName, out int a, in int b, ref int c)
        {
            a = b;
            c = a;
        }
        private static readonly (int, char, decimal, string, byte, bool, uint, long, double, float, DateTime)[]? values;
    }
    public class TestNestedClass
    {
        private class TestPrivateNestedClass { };
        private interface IPrivateNested { };
        protected interface IProtectedNested { };
        internal interface IInternalNested { };

    }
    public class TestProperties
    {
        public string PrivateSet { get; private set; }
        public string InternalGet { internal get; set; }
        public string ProtecteSet { get; protected set; }
        private string Private { get; }
        internal string Internal { get; set; }
        protected string Protected { get; set; }
        public TestProperties(string privateSet, string internalGet, string protectedSet)
        {
            PrivateSet = privateSet;
            InternalGet = internalGet;
            ProtecteSet = protectedSet;
            Private = PrivateSet;
            Internal = InternalGet;
            Protected = ProtecteSet;
        }
        public TestProperties(string privateSet, string internalGet)
        {
            PrivateSet = privateSet;
            InternalGet = internalGet;
            ProtecteSet = "";
            Private = PrivateSet;
            Internal = InternalGet;
            Protected = ProtecteSet;
        }
    }
    namespace TestNestedNamespace
    {
        internal class TestInternalClass
        {
            protected (char, string) TestTupleMethod(char c, string s)
            {
                return (c, s);
            }
            public void TestInRefOutMethod(in byte b, ref long l, out char c)
            {
                c = 'c';
            }
            public void TestGenericMethod<T, L>(T t, L l)
            {    }
        }
    }
}
