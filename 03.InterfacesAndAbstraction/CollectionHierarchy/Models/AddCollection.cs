using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionHierarchy.Models.Interfaces;

namespace CollectionHierarchy.Models
{
    public class AddCollection : IAddCollection
    {
        private List<string> collection;

        public AddCollection()
        {
            collection = new List<string>();
        }
        public int Add(string item)
        {
            collection.Add(item);
            return collection.Count - 1;
        }
    }
}
