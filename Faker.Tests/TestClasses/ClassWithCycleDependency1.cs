using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Tests.TestClasses
{
    public class ClassWithCycleDependency1
    {
        public int Id;
        public ClassWithCycleDependency2 InnerClass;
    }
}
