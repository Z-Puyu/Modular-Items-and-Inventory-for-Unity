using UnityEngine;

namespace ModularItemsAndInventory.Runtime {
    [CreateAssetMenu(fileName = "Item Type", menuName = "Modular Items and Inventory/Item Type")]
    public class ItemType : ItemDefinition {
        [field: SerializeField] public Sprite DefaultIcon { get; private set; }
    }
}
