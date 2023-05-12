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
            items.Sort();
            return res;
        }
        public void Remove(int itemIndex)
        {
            items.RemoveAt(itemIndex);
        }
        public string[] GetArrName()
        {
            List<string> arr = new List<string>();
            int iter = 0;
            Item oldItems = Items[0];
            foreach (var item in Items)
            {
                if (oldItems.Name == item.Name)
                    iter++;
                else
                {
                    arr.Add($"{iter}x [{oldItems.Size}]\t{oldItems.Name}");
                    iter = 1;
                }
                oldItems = item;
            }
            arr.Add($"{iter}x [{oldItems.Size}]\t{oldItems.Name}");
            return arr.ToArray();
        }
    }
}