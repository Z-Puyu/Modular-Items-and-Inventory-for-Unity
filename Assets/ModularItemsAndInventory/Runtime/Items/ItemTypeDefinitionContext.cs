using System.Collections.Generic;
using System.Linq;
using SaintsField;
using UnityEngine;

namespace ModularItemsAndInventory.Runtime.Items {
    [CreateAssetMenu(fileName = "Item Type Definition Context",
        menuName = "Modular Items and Inventory/Item Type Definition Context")]
    public class ItemTypeDefinitionContext : ScriptableObject {
        [field: SerializeField]
        private List<ItemTypeDefinition> DefinedItemTypesAndCategories { get; set; } =
            new List<ItemTypeDefinition>();

        public bool Contains(ItemTypeDefinition definition) {
            return this.DefinedItemTypesAndCategories.Any(definition.BelongsTo);
        }
    }
}
