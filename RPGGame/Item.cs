using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPGGame
{
    interface I_Item
    {
        string Name { get; }
        float Size { get; }
        //Test
    }
    public class Item : I_Item, IComparable
    {
        public string Name { get; private protected set; }
        public float Size { get; private protected set; }
        /// <summary>
        /// Эффекты
        /// </summary>
        public Effects Effects_ { get; private protected set; }
        public Item(string name, float size, Effects effects)
        {
            Name = name;
            Size = size;
            Effects_ = effects;
        }

        public int CompareTo(object obj)
        {
            return string.Compare(((Item)obj).Name, Name);
        }
    }
    public class PowerPendant : Item
    {
        public PowerPendant() : base("Кулон силы", 1, new Effects(0, 0, 3)) { }
    }
    public class MantleOfStealth : Item
    {
        public MantleOfStealth() : base("Мантия скрытности", 1, new Effects(0, 3, 0)) { }
    }
}