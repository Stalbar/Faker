using Faker.Core.Interfaces;
using Faker.Core.Classes;

namespace Faker.Core
{
    public class Faker : IFaker
    {
        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type type)
        {
            if (Generator.GenerateMethods.ContainsKey(type))
                return Generator.GenerateMethods[type]();
            return null;
        }
    }
}
