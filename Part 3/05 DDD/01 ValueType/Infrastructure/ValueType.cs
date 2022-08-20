using System;
using System.Linq;
using System.Reflection;

namespace Ddd.Infrastructure
{
    /// <summary>
    /// Базовый класс для всех Value типов.
    /// </summary>
    public class ValueType<T> where T : class
    {
        private Type _Type => typeof(T);
        private PropertyInfo[] _PropertyInfo => typeof(T).GetProperties().Where(p => !p.GetGetMethod().IsStatic).OrderBy(p => p.Name).ToArray();

        public bool Equals(T obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj == null) return false;
            return obj.GetHashCode() == GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj == null) return false;
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode() =>
            unchecked(3 ^ (int)_PropertyInfo.Average(x => 123 * x.Name.GetHashCode() * (x.GetValue(this) ?? "").GetHashCode()));

        public override string ToString()
        {
            var props = _PropertyInfo.Select(property => $"{property.Name}: {property.GetValue(this)}").ToList();
            return $"{_Type.Name}({string.Join("; ", props)})";
        }
    }
}