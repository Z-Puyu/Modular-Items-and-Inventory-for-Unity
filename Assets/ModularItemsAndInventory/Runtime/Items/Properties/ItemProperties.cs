using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModularItemsAndInventory.Runtime.Items.Properties {
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

        public bool HaveExactly<P>(out P property) where P : IItemProperty {
            if (this.Properties.TryGetValue(typeof(P), out IItemProperty p)) {
                property = (P)p;
                return true;
            }

            property = default;
            return false;
        }

        public bool HaveExactly<P>() where P : IItemProperty {
            return this.Properties.ContainsKey(typeof(P));
        }

        public bool Have<P>() where P : IItemProperty {
            Type type = typeof(P);
            return this.Properties.Keys.Any(k => type.IsAssignableFrom(k));
        }

        public bool Have<P>(out IEnumerable<P> properties) where P : IItemProperty {
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
            Type type = property.GetType();
            if (this.Properties.ContainsKey(type)) {
                Debug.LogWarning($"Duplicate property {property.GetType()} will be ignored.");
            } else {
                this.Properties.Add(type, property.Instantiate());
            }

            return this;
        }

        internal ItemProperties With(IEnumerable<IItemProperty> props) {
            foreach (IItemProperty p in props) {
                Type type = p.GetType();
                if (this.Properties.ContainsKey(type)) {
                    Debug.LogWarning($"Duplicate property {p.GetType()} will be ignored.");
                } else {
                    this.Properties.Add(type, p.Instantiate());
                }
            }
            
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
