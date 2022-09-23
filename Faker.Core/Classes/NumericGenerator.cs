using System.Reflection;

namespace Faker.Core.Classes
{
    public class NumericGenerator: Generator
    {
        static NumericGenerator()
        {
            var generationMethods = typeof(NumericGenerator).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            foreach (var method in generationMethods)
            {
                GenerateMethods[method.ReturnType] = () => method.Invoke(null, null);
            }
        }

        private static short GenerateShort() => BitConverter.ToInt16(GenerateBytes(sizeof(short)));
        private static ushort GenerateUShort() => BitConverter.ToUInt16(GenerateBytes(sizeof(ushort)));
        private static int GenerateInt() => BitConverter.ToInt32(GenerateBytes(sizeof(int)));
        private static uint GenerateUInt() => BitConverter.ToUInt32(GenerateBytes(sizeof(uint)));
        private static long GenerateLong() => BitConverter.ToInt64(GenerateBytes(sizeof(long)));
        private static ulong GenerateULong() => BitConverter.ToUInt64(GenerateBytes(sizeof(ulong)));
        private static float GenerateFloat() => BitConverter.ToSingle(GenerateBytes(sizeof(float)));
        private static double GenerateDouble() => BitConverter.ToDouble(GenerateBytes(sizeof(double)));

    }
}
