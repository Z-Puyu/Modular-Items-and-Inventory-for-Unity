using System;
using System.Collections.Generic;
using System.Linq;

namespace ModularItemsAndInventory.Runtime.Items {
    public sealed class ItemProperties : IEquatable<ItemProperties>, IComparable<ItemProperties> {
        private Item OwningItem { get; }

        private Dictionary<Type, IItemProperty> Properties { get; } =
            new Dictionary<Type, IItemProperty>();
        // private HashSet<IItemProperty> Properties { get; } =  new HashSet<IItemProperty>();
        
        public void Affect<T>(T target) {
            foreach (IItemProperty p in this.Properties.Values) {
                if (p is IItemProperty<T> property) {
                    property.Process(this.OwningItem, target);
                }
            }
        }

        public bool HasExactly<P>(out P property) where P : IItemProperty {
            if (this.Properties.TryGetValue(typeof(P), out IItemProperty p)) {
                property = (P)p;
                return true;
            }

            property = default;
            return false;
        }

        public bool HasExactly<P>() where P : IItemProperty {
            return this.Properties.ContainsKey(typeof(P));
        }

        public bool Has<P>() where P : IItemProperty {
            Type type = typeof(P);
            return this.Properties.Keys.Any(k => type.IsAssignableFrom(k));
        }

        public bool Has<P>(out IEnumerable<P> properties) where P : IItemProperty {
            properties = this.Properties.Values.OfType<P>();
            return properties.Any();
        }

        private ItemProperties(Item owningItem) {
            this.OwningItem = owningItem;
        }

        public static ItemProperties Of(Item item) {
            return new ItemProperties(item);
        }

        internal ItemProperties With(IItemProperty property) {
            this.Properties[property.GetType()] = property;
            return this;
        }
        
        public bool Equals(ItemProperties other) {
            return other != null && this.Properties.Values.ToHashSet().SetEquals(other.Properties.Values);
        }

        public int CompareTo(ItemProperties other) {
            int length = Math.Min(this.Properties.Count, other.Properties.Count);
            List<IItemProperty> otherProperties = other.Properties.Values.ToList();
            otherProperties.Sort();
            List<IItemProperty> thisProperties = this.Properties.Values.ToList();
            thisProperties.Sort();
            for (int i = 0; i < length; i += 1) {
                IItemProperty thisProperty = thisProperties[i];
                IItemProperty otherProperty = otherProperties[i];
                int comparison = thisProperty.CompareTo(otherProperty);
                if (comparison != 0) {
                    return comparison;
                }
            }
            
            return this.Properties.Count - otherProperties.Count;
        }

        public override int GetHashCode() {
            return HashSet<IItemProperty>.CreateSetComparer().GetHashCode(this.Properties.Values.ToHashSet());
        }
    }
}
