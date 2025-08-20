namespace ModularItemsAndInventory.Runtime.Items.Orderings {
    public class ItemOrderingByType : ItemOrdering {
        protected override int CompareExistingItems(Item x, Item y) {
            return x.Type.CompareTo(y.Type);
        }
    }
}
