using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.CycleDependencyChecker.Exceptions
{
    public class CycleDependencyException: Exception
    {
        public CycleDependencyException()
        {

        }

        public CycleDependencyException(string message)
            : base(message)
        {

        }
    }
}
