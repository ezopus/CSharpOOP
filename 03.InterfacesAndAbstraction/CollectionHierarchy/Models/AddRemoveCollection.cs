using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionHierarchy.Models.Interfaces;

namespace CollectionHierarchy.Models
{
    public class AddRemoveCollection : IAddRemoveCollection
    {
        private List<string> collection;
        public AddRemoveCollection()
        {
            collection = new List<string>();
        }
        public int Add(string item)
        {
            collection.Insert(0, item);
            return 0;
        }

        public string Remove()
        {
            string removed = null;
            if (collection.Count > 0)
            {
                removed = collection[collection.Count - 1];
                collection.RemoveAt(collection.Count - 1);
            }
            return removed;
        }
    }
}
