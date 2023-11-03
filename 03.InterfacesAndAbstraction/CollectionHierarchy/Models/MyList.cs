using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionHierarchy.Models.Interfaces;

namespace CollectionHierarchy.Models
{
    public class MyList : IMyList
    {
        private List<string> collection;

        public MyList()
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
                removed = collection[0];
                collection.RemoveAt(0);
            }

            return removed;
        }

        public int Used
        {
            get => collection.Count;
        }
    }
}
