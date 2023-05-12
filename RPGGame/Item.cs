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
        /// <summary>
        /// Место занимаемое в инвенторе
        /// </summary>
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
    public abstract class Healing : Item
    {
        protected Healing(string name, float size, Effects effects) : base(name, size, effects)
        {
        }
        abstract public float HealingAmount { get; }
    }
    public class PowerPendant : Item
    {
        /// <summary>
        /// Кулон силы
        /// </summary>
        public PowerPendant() : base("Кулон силы", 1, new Effects(0, 0, 3)) { }
    }
    public class MantleOfStealth : Item
    {
        /// <summary>
        /// Мантия скрытности
        /// </summary>
        public MantleOfStealth() : base("Мантия скрытности", 1, new Effects(0, 3, 0)) { }
    }
    public class RingOfSpeed : Item
    {
        /// <summary>
        /// Кольцо скорости
        /// </summary>
        public RingOfSpeed() : base("Кольцо скорости", 1, new Effects(3, 0, 0)) { }
    }
    public class Bandage : Healing
    {
        /// <summary>
        /// Бинты(хилл)
        /// </summary>
        public Bandage() : base("Бинт", 0.5f, new Effects(0, 0, 0))
        {
        }
        public override float HealingAmount { get; } = 1;
    }
    public class Potion : Healing
    {
        /// <summary>
        /// Зелье(хилл)
        /// </summary>
        public Potion() : base("Зелье здоровья", 0.5f, new Effects(0, 0, 0))
        {
        }

        public override float HealingAmount { get; } = 3;
    }
}