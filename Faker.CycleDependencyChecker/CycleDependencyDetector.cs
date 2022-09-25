using System.Reflection;

namespace Faker.CycleDependencyChecker
{
    public class CycleDependencyDetector
    {
        private HashSet<Type> _dependences;
        private bool _hasCycleDependency;

        public CycleDependencyDetector(Type type)
        {
            _dependences = new HashSet<Type>();
            _hasCycleDependency = false;
            CheckForCycleDependency(type);
        }

        private void CheckForCycleDependency(Type type)
        {
            if (_dependences.Contains(type))
            {
                _hasCycleDependency = true;
                return;
            }
            _dependences.Add(type);
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType.IsClass && field.FieldType != typeof(string))
                    CheckForCycleDependency(field.FieldType);
            }
        }

        public bool HasCycleDependency { get => _hasCycleDependency; }
    }
}
