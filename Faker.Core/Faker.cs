using Faker.Core.Interfaces;
using Faker.Core.Classes;
using Faker.CycleDependencyChecker;
using Faker.CycleDependencyChecker.Exceptions;
using System.Reflection;
using System.Collections;

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
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                return CreateListOfObjects(type);
            return CreateSingleObject(type);
        }

        private object CreateListOfObjects(Type type)
        {
            int listCapacity = 7;
            var result = FillList(type, listCapacity);
            return result;
        }

        private IList FillList(Type type, int capacity)
        {
            var result = Activator.CreateInstance(type) as IList;
            for (int i = 0; i < capacity; i++)
            {
                result.Add(Create(type.GetGenericArguments()[0]));
            }
            return result;
        }

        private object CreateSingleObject(Type type)
        {
            var constructor = GetMaxParametersConstructor(type);
            var publicFields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var publicSetters = GetPublicSetters(type);
            object result = CreateObjectViaConstructor(constructor);
            SetValuesInObjectFields(result, publicFields);
            SetValuesInObjectProperties(result, publicSetters);
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

        private void SetValuesInObjectFields(object obj, FieldInfo[] fields)
        {
            foreach (var field in fields)
            {
                var fieldType = field.FieldType;
                field.SetValue(obj, Create(field.FieldType));
            }
        }

        private void SetValuesInObjectProperties(object obj, PropertyInfo[] properties)
        {
            foreach (var property in properties)
            {
                property.SetValue(obj, Create(property.PropertyType));
            }
        }
    }
}
