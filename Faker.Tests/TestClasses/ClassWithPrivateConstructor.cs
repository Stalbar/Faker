using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Tests.TestClasses
{
    public class ClassWithPrivateConstructor
    {
        public int Id;
        public string Name;
        public DateTime CreationTime;

        private ClassWithPrivateConstructor(int id, string name, DateTime creationTime)
        {
            Id = id;
            Name = name;
            CreationTime = creationTime;
        }

        public ClassWithPrivateConstructor(int id, string name)
        {
            throw new Exception();
            Id = id;
            Name = name;
        }

        public ClassWithPrivateConstructor(int id)
        {
            Id = id;
        }
    }
}
