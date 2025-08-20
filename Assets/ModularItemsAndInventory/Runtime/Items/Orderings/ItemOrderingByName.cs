namespace ModularItemsAndInventory.Runtime.Items.Orderings {
    public class ItemOrderingByName : ItemOrdering {
        protected override int CompareExistingItems(Item x, Item y) {
            return string.CompareOrdinal(x.Name, y.Name);
        }
    }
}
