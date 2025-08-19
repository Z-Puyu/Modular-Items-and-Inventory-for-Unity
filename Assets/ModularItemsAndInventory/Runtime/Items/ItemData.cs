using System.Collections.Generic;
using SaintsField;
using UnityEngine;

namespace ModularItemsAndInventory.Runtime.Items {
    [CreateAssetMenu(fileName = "Item Data", menuName = "Item/Data")]
    public class ItemData : ScriptableObject {
        [field: SerializeField] public ItemType Type { get; private set; } 
        [field: SerializeField] public string Name { get; private set; }
        /*[field: SerializeReference, RichLabel(nameof(this.Label), true)] 
        public List<IItemPropertyData> ItemProperties { get; private set; } = [];

        private string Label(object obj, int _) => obj.GetType().Name;*/
    }
}
