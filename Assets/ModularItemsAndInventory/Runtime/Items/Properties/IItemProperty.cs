using System;

namespace ModularItemsAndInventory.Runtime.Items.Properties {
    public interface IItemProperty : IEquatable<IItemProperty>, IComparable<IItemProperty> {
        public IItemProperty Instantiate();
    }

    public interface IItemProperty<in T> : IItemProperty {
        public void Process(in Item item, T target);
    }
}
