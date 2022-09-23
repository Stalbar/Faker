using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Classes
{
    public class StringGenerator: Generator
    {
        private static string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789;:\"\'{}[]()-+=_?\\/<>,.!@#$%^&";

        private const int _minStringLength = 0;

        private const int _maxStringLength = 40;

        private static string GenerateString()
        {
            int length = _random.Next(_minStringLength, _maxStringLength);
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                char randomSymbol = symbols[_random.Next(symbols.Length)];
                sb.Append(randomSymbol);
            }
            return sb.ToString();
        }

        private static char GenerateChar() => symbols[_random.Next(symbols.Length)];
    }
}
