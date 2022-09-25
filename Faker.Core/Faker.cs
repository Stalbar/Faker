using Faker.Core.Interfaces;
using Faker.Core.Classes;
using Faker.CycleDependencyChecker;
using Faker.CycleDependencyChecker.Exceptions;
using System.Reflection;

namespace Faker.Core
{
    public class Faker : IFaker
    {
        public T Create<T>()
        {
            if (IsHasCycleDependency(typeof(T)))
                throw new CycleDependencyException("Cycle dependency is detected!");
            else
                return (T)Create(typeof(T));
        }
        private bool IsHasCycleDependency(Type type)
        {
            CycleDependencyDetector cycleDependencyDetector = new(type);
            return cycleDependencyDetector.HasCycleDependency;
        }

        private object Create(Type type)
        {
            if (Generator.GenerateMethods.ContainsKey(type))
                return Generator.GenerateMethods[type]();
            return CreateSingleObject(type);
        }

        private object CreateSingleObject(Type type)
        {
            var constructor = GetMaxParametersConstructor(type);
            var constructorParameters = constructor.GetParameters();
            var publicFields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var publicSetters = GetPublicSetters(type);
            object result = CreateObjectViaConstructor(constructor);
            foreach (var field in publicFields)
            {
                field.SetValue(result, Create(field.FieldType));
            }
            foreach (var property in publicSetters)
            {
                property.SetValue(result, Create(property.PropertyType));
            }
            return result;
        }

        private ConstructorInfo GetMaxParametersConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            var maxParametersConstructor = constructors[0];
            foreach (var constructor in constructors)
            {
                if (constructor.GetParameters().Length > maxParametersConstructor.GetParameters().Length)
                    maxParametersConstructor = constructor;
            }
            return maxParametersConstructor;
        }

        private PropertyInfo[] GetPublicSetters(Type type)
        {
            var properties = type.GetProperties();
            var publicSetters = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                if (property.CanWrite && property.SetMethod.IsPublic)
                    publicSetters.Add(property);
            }
            return publicSetters.ToArray(); 
        }

        private object CreateObjectViaConstructor(ConstructorInfo constructor)
        {
            List<Object> constructorParams = GetConstructorParameters(constructor);
            return constructor.Invoke(constructorParams.ToArray());
        }

        private List<Object> GetConstructorParameters(ConstructorInfo constructor)
        {
            List<object> constructorParams = new List<object>();
            foreach (var param in constructor.GetParameters())
            {
                constructorParams.Add(Create(param.ParameterType));
            }
            return constructorParams;
        }
    }
}
