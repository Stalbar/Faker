using System.Text;
using System.Reflection;

namespace Faker.Core.Classes
{
    public class StringGenerator: Generator
    {
        private static string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789;:\"\'{}[]()-+=_?\\/<>,.!@#$%^&";

        private const int _minStringLength = 1;

        private const int _maxStringLength = 40;

        static StringGenerator()
        {
            var generationMethods = typeof(StringGenerator).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            foreach (var method in generationMethods)
            {
                GenerateMethods[method.ReturnType] = () => method.Invoke(null, null);
            }
        }

        private static string GenerateString()
        {
            int length = _random.Next(_minStringLength, _maxStringLength);
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                char randomSymbol = GenerateChar();
                sb.Append(randomSymbol);
            }
            return sb.ToString();
        }

        private static char GenerateChar() => symbols[_random.Next(symbols.Length)];
    }
}
