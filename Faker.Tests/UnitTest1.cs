using Faker.CycleDependencyChecker;
using Faker.Core;
using System.Collections.Generic;

namespace Faker.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var faker = new Faker.Core.Faker();
            var randomUser = faker.Create<List<int>>();
        }
    }

    public class User
    {
        public int Id;
        public string Name;
    }
}