using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModularItemsAndInventory.Runtime.Items;
using UnityEngine;

namespace ModularItemsAndInventory.Runtime.Inventory {
    [DisallowMultipleComponent]
    public class Inventory : MonoBehaviour, IEnumerable<KeyValuePair<Item, int>> {
        [field: SerializeField] private ItemTypeDefinitionContext DefinedItemTypes { get; set; }

        private Dictionary<ItemTypeDefinition, Dictionary<Item, int>> Items { get; set; } =
            new Dictionary<ItemTypeDefinition, Dictionary<Item, int>>();

        public IDictionary<Item, int> this[ItemTypeDefinition type] =>
                this.Items.TryGetValue(type, out Dictionary<Item, int> items)
                        ? items
                        : new Dictionary<Item, int>();
        
        public int Count(Item item) {
            return this.Items.TryGetValue(item.Type, out Dictionary<Item, int> record)
                    ? record.GetValueOrDefault(item, 0)
                    : 0;
        }

        public int Count(ItemTypeDefinition type) {
            return this.Items.TryGetValue(type, out Dictionary<Item, int> record) ? record.Values.Sum() : 0;
        }

        public int Count(Predicate<Item> predicate) {
            return this.Items.SelectMany(entry => entry.Value)
                       .Count(record => predicate(record.Key));
        }
        
        private bool CanStore(Item item) {
            return item is not null && this.DefinedItemTypes &&
                   this.DefinedItemTypes.Contains(item.Type);
        }

        public bool Add(Item item) {
            return this.Add(1, item);
        }

        public bool Add(int quantity, Item item) {
            if (item is null) {
                Debug.LogError("Cannot add null item to inventory.", this);
                return false;
            }
            
            if (quantity < 1) {
                Debug.LogWarning("Minimally should add one copy of an item.", this);
                return false;
            }

            if (!this.CanStore(item)) {
                return false;
            }
            
            ItemTypeDefinition type = item.Type;
            if (this.Items.TryGetValue(type, out Dictionary<Item, int> record)) {
                record[item] = record.GetValueOrDefault(item, 0) + quantity;
            } else {
                this.Items.Add(type, new Dictionary<Item, int> { { item, quantity } });
            }

            return true;
        }

        public void RemoveAll(Item item) {
            if (item is null) {
                Debug.LogError("Cannot remove null item to inventory.", this);
                return;
            }

            if (this.Items.TryGetValue(item.Type, out Dictionary<Item, int> record)) {
                record.Remove(item);
            }
        }

        public bool Remove(Item item) {
            return this.Remove(1, item);
        }
        
        public bool Remove(int quantity, Item item) {
            if (item is null) {
                Debug.LogError("Cannot remove null item to inventory.", this);
                return false;
            }
            
            if (quantity < 1) {
                Debug.LogWarning("Minimally should remove one copy of an item.", this);
                return false;
            }
            
            ItemTypeDefinition type = item.Type;
            if (!this.Items.TryGetValue(type, out Dictionary<Item, int> record)) {
                Debug.LogWarning($"Does not have any {item} to remove.", this);
                return false;
            }

            if (!record.TryGetValue(item, out int count)) {
                Debug.LogWarning($"Does not have any {item} to remove.", this);
                return false;
            }

            if (count < quantity) {
                Debug.LogWarning($"Trying to remove {quantity} copies of {item} but only has {count}.", this);
            } 
            
            int remaining = record[item] = count - quantity;
            if (remaining <= 0) {
                record.Remove(item);
            }

            return true;
        }

        public bool ContainsAtLeast(int quantity, Item item) {
            if (!this.Items.TryGetValue(item.Type, out Dictionary<Item, int> record)) {
                return false;
            }
            
            return record.TryGetValue(item, out int count) && count >= quantity;
        }

        public IEnumerator<KeyValuePair<Item, int>> GetEnumerator() {
            return this.Items.SelectMany(record => record.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }
}
