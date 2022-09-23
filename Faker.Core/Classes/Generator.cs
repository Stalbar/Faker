using System.Reflection;
using System.Runtime.CompilerServices;

namespace Faker.Core.Classes
{
    public abstract class Generator
    {
        public delegate object GenerateDelegate();
        public static Dictionary<Type, GenerateDelegate> GenerateMethods => _generateMethods;
        protected static Dictionary<Type, GenerateDelegate> _generateMethods = new();
        protected static readonly Random _random = new Random();

        static Generator()
        {
            RunSubClasesStaticConstructors();
        }

        protected static byte[] GenerateBytes(int bytesCountToGenerate)
        {
            byte[] result = new byte[bytesCountToGenerate];
            _random.NextBytes(result);
            return result;
        }

        private static void RunSubClasesStaticConstructors()
        {
            Type[] currentAssemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in currentAssemblyTypes)
            {
                if (type.IsAssignableTo(typeof(Generator)) && type != typeof(Generator))
                    RuntimeHelpers.RunClassConstructor(type.TypeHandle);
            }
        }
    }
}
