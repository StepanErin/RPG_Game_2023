using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPGGame
{
    internal class Inventory
    {
        public int Size { get; private set; } = 10;
        private List<Item> items = new List<Item>();
        public List<Item> Items { get { return items; } }
        public bool Add(Item item)
        {
            bool res = items.Count + item.Size <= Size;
            if (res) items.Add(item);
            return res;
        }
        public void Remove(int itemIndex)
        {
            items.RemoveAt(itemIndex);
        }
        public string[] GetArrName()
        {
            List<string> arr = new List<string>();
            foreach (var item in Items) arr.Add($"[{item.Size}]\t{item.Name}");
            return arr.ToArray();
        }
    }
}