using UnityEngine;

namespace ModularItemsAndInventory.Runtime.Items {
    [CreateAssetMenu(fileName = "Item Type", menuName = "Modular Items and Inventory/Item Type")]
    public class ItemType : ItemTypeDefinition {
        [field: SerializeField] public Sprite DefaultIcon { get; private set; }
    }
}
