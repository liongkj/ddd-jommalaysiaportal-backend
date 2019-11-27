using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JomMalaysia.Core.Domain.Enums
{
    public abstract class EnumerationBase : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected EnumerationBase(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name.ToLower();

        public static T Parse<T>(string name) where T : EnumerationBase
        {
            var enums = GetAll<T>();
            foreach (var e in enums)
            {
                if (e.Name == name.ToLower())
                    return e;
            }
            return null;
        }
        public static T Parse<T>(int id) where T : EnumerationBase
        {
            var enums = GetAll<T>();
            foreach (var e in enums)
            {
                if (e.Id == id)
                    return e;
            }
            return null;
        }
        public static IEnumerable<T> GetAll<T>() where T : EnumerationBase
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as EnumerationBase;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((EnumerationBase)other).Id);

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Id);
        }

        // Other utility methods ... 
    }
}
