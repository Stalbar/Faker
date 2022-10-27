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
            var constructors = type.GetConstructors().OrderByDescending(x => x.GetParameters().Length).ToArray();
            var publicFields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var publicSetters = GetPublicSetters(type);
            object result = null;
            int i = 0;
            while (result == null && i < constructors.Length)
            {
                result = CreateObjectViaConstructor(constructors[i]);
                i++;
            }
            if (result == null)
            {
                throw new Exception("Can not be created via constructor");
            }
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
            object result = null;
            try
            {
                result = constructor.Invoke(constructorParams.ToArray());
            }
            catch
            {
                result = null;
            }
            return result;
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
                if (!IsFieldSetInConstructor(obj, field))
                    field.SetValue(obj, Create(field.FieldType));
            }
        }

        private void SetValuesInObjectProperties(object obj, PropertyInfo[] properties)
        {
            foreach (var property in properties)
            {
                if (!IsPropertySetInConsturctor(obj, property))
                    property.SetValue(obj, Create(property.PropertyType));
            }
        }

        private bool IsFieldSetInConstructor(object obj, FieldInfo fieldInfo)
        {
            var fieldType = fieldInfo.FieldType;
            if (fieldType.IsValueType && fieldInfo.GetValue(obj).Equals(Activator.CreateInstance(fieldType)))
                return false;
            else if (fieldType.IsClass && fieldInfo.GetValue(obj) == null)
                return false;
            else
                return true;
                
        }

        private bool IsPropertySetInConsturctor(object obj, PropertyInfo propertyInfo)
        {
            var propertyType = propertyInfo.PropertyType;
            if (propertyType.IsValueType && propertyInfo.GetValue(obj).Equals(Activator.CreateInstance(propertyType)))
                return false;
            else if (propertyType.IsClass && propertyInfo.GetValue(obj) == null)
                return false;
            else
                return true;
        }
    }
}
