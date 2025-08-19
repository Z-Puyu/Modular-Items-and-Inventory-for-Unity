using System;

namespace ModularItemsAndInventory.Runtime.Items {
    public interface IItemProperty : IEquatable<IItemProperty>, IComparable<IItemProperty> { }

    public interface IItemProperty<in T> : IItemProperty {
        public void Process(in Item item, T target);
    }
}
