using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserTests
{
    public static class TestExtensionMethods
    {
        public static string GetFullname(this TestNameClass testName)
        {
            return testName.Surname + " " + testName.Name + " " + testName.Patronimic;
        }
        public static void TestMethod(this TestNameClass testName, int a)
        {

        }
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
