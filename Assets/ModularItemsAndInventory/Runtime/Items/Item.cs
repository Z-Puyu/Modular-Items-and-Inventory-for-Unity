using System;
using ModularItemsAndInventory.Runtime.Items.Properties;

namespace ModularItemsAndInventory.Runtime.Items {
    public record Item(ItemType Type, string Name, ItemProperties Properties) : IComparable<Item> {
        public Item(ItemData data) : this(data.Type, data.Name, null) {
            this.Properties = ItemProperties.Of(this).With(data.Properties);
        }
        
        public int CompareTo(Item other) {
            int comparison = this.Type.CompareTo(other.Type);
            if (comparison != 0) {
                return comparison;
            }
            
            comparison = string.CompareOrdinal(this.Name, other.Name);
            return comparison != 0 ? comparison : this.Properties.CompareTo(other.Properties);
        }
    }
}
