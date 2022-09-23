using Faker.Core.Interfaces;

namespace Faker.Core
{
    public class Faker : IFaker
    {
        public T Create<T>()
        {
            throw new NotImplementedException();
        }
    }
}
