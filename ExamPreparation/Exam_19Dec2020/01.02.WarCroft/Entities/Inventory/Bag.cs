using System;
using System.Collections.Generic;
using System.Linq;
using WarCroft.Constants;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private readonly List<Item> items;
        protected Bag(int capacity)
        {
            Capacity = capacity;
            items = new List<Item>();
        }

        public int Capacity { get; set; }
        public int Load => Items.Sum(i => i.Weight);
        public IReadOnlyCollection<Item> Items => items;
        public void AddItem(Item item)
        {
            if (Load + item.Weight > Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.ExceedMaximumBagCapacity);
            }
            items.Add(item);
        }

        public Item GetItem(string name)
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }

            Item getItem = items.FirstOrDefault(i => i.GetType().Name == name);
            if (getItem == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotFoundInBag, name));
            }

            items.Remove(getItem);
            return getItem;
        }
    }
}
