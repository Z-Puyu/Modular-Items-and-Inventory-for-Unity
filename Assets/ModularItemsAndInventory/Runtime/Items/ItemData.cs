using System.Collections.Generic;
using ModularItemsAndInventory.Runtime.Items.Properties;
using SaintsField;
using UnityEngine;

namespace ModularItemsAndInventory.Runtime.Items {
    [CreateAssetMenu(fileName = "Item Data", menuName = "Item/Data")]
    public class ItemData : ScriptableObject {
        [field: SerializeField] public ItemType Type { get; private set; } 
        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeReference, RichLabel(nameof(this.LabelProperty), true)]
        public List<IItemProperty> Properties { get; private set; } = new List<IItemProperty>();

        private string LabelProperty(object prop, int _) => prop.GetType().Name;
    }
}
