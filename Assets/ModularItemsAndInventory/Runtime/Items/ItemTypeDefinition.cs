﻿using System;
using System.Text;
using UnityEngine;

namespace ModularItemsAndInventory.Runtime.Items {
    public abstract class ItemTypeDefinition : ScriptableObject, IComparable<ItemTypeDefinition> {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public ItemCategory Category { get; private set; }
        private string FullName { get; set; }

        public override string ToString() {
            return this.FullName;
        }

        public bool BelongsTo(ItemTypeDefinition type) {
            ItemTypeDefinition curr = this;
            while (curr) {
                if (curr == type) {
                    return true;
                }
                
                curr = curr.Category;
            }

            return false;
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

        public int CompareTo(ItemTypeDefinition other) {
            return this == other ? 0 : string.Compare(this.FullName, other.FullName, StringComparison.Ordinal);
        }
    }
}
