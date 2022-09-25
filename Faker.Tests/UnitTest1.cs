using Faker.CycleDependencyChecker;
using Faker.Core;
using System.Collections.Generic;
using Faker.Core.Interfaces;
using Faker.Tests.TestClasses;
namespace Faker.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IFaker _faker = new Faker.Core.Faker();

        [TestMethod]
        public void TestNumericValues()
        {
            Assert.AreEqual(typeof(short), _faker.Create<short>().GetType());
            Assert.AreEqual(typeof(ushort), _faker.Create<ushort>().GetType());
            Assert.AreEqual(typeof(int), _faker.Create<int>().GetType());
            Assert.AreEqual(typeof(uint), _faker.Create<uint>().GetType());
            Assert.AreEqual(typeof(long), _faker.Create<long>().GetType());
            Assert.AreEqual(typeof(ulong), _faker.Create<ulong>().GetType());
            Assert.AreEqual(typeof(float), _faker.Create<float>().GetType());
            Assert.AreEqual(typeof(double), _faker.Create<double>().GetType());
        }

        [TestMethod]
        public void TestStringValues()
        {
            Assert.AreEqual(typeof(string), _faker.Create<string>().GetType());
            Assert.AreEqual(typeof(char), _faker.Create<char>().GetType());
        }

        [TestMethod]
        public void TestDateTime()
        {
            Assert.AreEqual(typeof(DateTime), _faker.Create<DateTime>().GetType());
        }

        [TestMethod]
        public void TestClassWithFields()
        {
            var testClass = _faker.Create<ClassWithFields>();
            Assert.AreNotEqual(testClass, null);
            Assert.AreEqual(typeof(string), testClass.Name.GetType());
            Assert.AreEqual(typeof(int), testClass.Id.GetType());
            Assert.AreEqual(typeof(DateTime), testClass.CreationTime.GetType());
        }

        [TestMethod]
        public void TestClassWithProperties()
        {
            var testClass = _faker.Create<ClassWithProperties>();
            Assert.AreNotEqual(testClass, null);
            Assert.AreEqual(typeof(string), testClass.Name.GetType());
            Assert.AreEqual(typeof(int), testClass.Id.GetType());
            Assert.AreEqual(typeof(DateTime), testClass.CreationTime.GetType());
        }

        [TestMethod]
        public void TestClassWitnConstructors()
        {
            var testClass = _faker.Create<ClassWithConstructors>();
            Assert.AreNotEqual(testClass, null);
            Assert.AreEqual(typeof(string), testClass.Name.GetType());
            Assert.AreEqual(typeof(int), testClass.Id.GetType());
            Assert.AreEqual(typeof(DateTime), testClass.CreationTime.GetType());
        }

        [TestMethod]
        public void TestClassWithList()
        {
            var testClass = _faker.Create<ClassWithList>();
            Assert.AreNotEqual(testClass, null);
            Assert.AreNotEqual(testClass.Ids, null);
        }
    }
}