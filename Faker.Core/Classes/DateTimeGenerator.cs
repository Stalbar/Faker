using System.Reflection;
namespace Faker.Core.Classes
{
    public class DateTimeGenerator: Generator
    {
        static DateTimeGenerator()
        {
            var generationMethods = typeof(DateTimeGenerator).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            foreach (var method in generationMethods)
            {
                GenerateMethods[method.ReturnType] = () => method.Invoke(null, null);
            }
        }
        private static DateTime GenerateDateTime() => new DateTime(_random.NextInt64(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks));
    }
}
