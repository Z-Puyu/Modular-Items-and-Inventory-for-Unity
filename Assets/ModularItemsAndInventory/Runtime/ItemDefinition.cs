using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ModularItemsAndInventory.Runtime {
    public abstract class ItemDefinition : ScriptableObject, IComparable<ItemDefinition> {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public ItemCategory Category { get; private set; }
        private string FullName { get; set; }

        public override string ToString() {
            return this.FullName;
        }

        private void OnValidate() {
            StringBuilder sb = new StringBuilder(this.Name);
            ItemCategory category = this.Category;
            while (category) {
                sb.Insert(0, $"{category.Name}.");
                category = category.Category;
            }

            this.FullName = sb.ToString();
        }

        public int CompareTo(ItemDefinition other) {
            return this == other ? 0 : string.Compare(this.FullName, other.FullName, StringComparison.Ordinal);
        }
    }
}
