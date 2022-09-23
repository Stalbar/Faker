using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Classes
{
    public abstract class Generator
    {
        protected static readonly Random _random = new Random();
        protected static byte[] GenerateBytes(int bytesCountToGenerate)
        {
            byte[] result = new byte[bytesCountToGenerate];
            _random.NextBytes(result);
            return result;
        }
    }
}
