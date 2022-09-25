using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Tests.TestClasses
{
    public class ClassWithConstructors
    {
        public int Id;
        public DateTime CreationTime;
        public string Name;
        public long Count { get; set; }

        public ClassWithConstructors(int id)
        {
            Id = id;
        }

        public ClassWithConstructors(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
