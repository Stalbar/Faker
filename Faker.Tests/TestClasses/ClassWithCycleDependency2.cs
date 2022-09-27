using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Tests.TestClasses
{
    public class ClassWithCycleDependency2
    {
        public string Name;
        public ClassWithCycleDependency1 InnerClass;
    }
}
