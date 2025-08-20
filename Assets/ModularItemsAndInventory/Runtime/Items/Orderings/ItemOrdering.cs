using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ModularItemsAndInventory.Runtime.Items.Orderings {
    public abstract class ItemOrdering : IComparer<Item> {
        public static ItemOrdering Default = new DefaultItemOrdering();
        
        public int Compare(Item x, Item y) {
            if (object.ReferenceEquals(x, y)) {
                return 0;
            }

            if (x is null) {
                return -1;
            }

            if (y is null) {
                return 1;
            }

            return this.CompareExistingItems(x, y);
        }

        protected abstract int CompareExistingItems([NotNull] Item x, [NotNull] Item y);
    }
}
