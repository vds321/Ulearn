using System;

namespace Inheritance.DataStructure
{
    public class Category : IComparable
    {
        public string Name { get; }
        public MessageType Incoming { get; }
        public MessageTopic Subscribe { get; }

        public Category(string name, MessageType incoming, MessageTopic subscribe)
        {
            Name = name;
            Incoming = incoming;
            Subscribe = subscribe;
        }

        public int CompareTo(object other)
        {
            if (!(other is Category obj)) return 0;
            var result = string.Compare(Name, obj.Name, StringComparison.CurrentCulture);
            if (result != 0) return result;
            var result2 = Incoming.CompareTo(obj.Incoming);
            return result2 != 0 ? result2 : Subscribe.CompareTo(obj.Subscribe);
        }

        public override string ToString()
        {
            return $"{Name}.{Incoming}.{Subscribe}";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Category category)) return false;
            return GetHashCode() == category.GetHashCode();
        }

        public override int GetHashCode() =>
            unchecked(3 ^ ((Name ?? "").GetHashCode() * Subscribe.ToString().GetHashCode() * Incoming.ToString().GetHashCode()));

        public static bool operator >(Category c1, Category c2) => c1.CompareTo(c2) > 0;
        public static bool operator <(Category c1, Category c2) => c1.CompareTo(c2) < 0;
        public static bool operator <=(Category c1, Category c2) => c1.CompareTo(c2) <= 0;
        public static bool operator >=(Category c1, Category c2) => c1.CompareTo(c2) >= 0;

    }
}
