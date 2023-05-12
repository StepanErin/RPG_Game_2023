using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RPGGame
{
    public interface IHarm
    {
        /// <summary>
        /// Рубящее
        /// </summary>
        float Chopping { get; }
        /// <summary>
        /// Колющее
        /// </summary>
        float Stabbing { get; }
        /// <summary>
        /// Дробящее
        /// </summary>
        float Crushing { get; }
    }
    public struct Harm
    {
        /// <summary>
        /// Установить домаг
        /// </summary>
        /// <param name="chopping">Рубящее</param>
        /// <param name="stabbing">Колющее</param>
        /// <param name="crushing">Дробящее</param>
        public Harm(float chopping, float stabbing, float crushing)
        {
            Chopping = chopping;
            Stabbing = stabbing;
            Crushing = crushing;
        }
        /// <summary>
        /// Рубящее
        /// </summary>
        public float Chopping { get; private set; }
        /// <summary>
        /// Колющее
        /// </summary>
        public float Stabbing { get; private set; }
        /// <summary>
        /// Дробящее
        /// </summary>
        public float Crushing { get; private set; }
        public new string ToString()
        {
            return $"[Рубящее-{Chopping}; Дробящее-{Crushing}; Колющее-{Stabbing}]";
        }
    }
    public class Weapon : Item
    {
        public Weapon(string name, int distance, Harm damage, Effects requirement, float size)
            : base(name, size, requirement)
        {
            Distance = distance;
            Damage_ = damage;
        }
        public float Distance { get; private set; }
        /// <summary>
        /// Все типы домага
        /// </summary>
        public Harm Damage_ { get; private set; }
    }

    #region Weapons
    /// <summary>
    /// Меч одноручный	
    /// </summary>
    internal class SwordOneHanded : Weapon
    {
        public SwordOneHanded() : base("Меч одноручный", 0, new Harm(4, 3, 0), new Effects(-1, -1, -1), 1) { }
    }
    /// <summary>
    /// Меч двуручный	
    /// </summary>
    internal class TwoHandedSword : Weapon
    {
        public TwoHandedSword() : base("Меч двуручный", 0, new Harm(8, 4, 0), new Effects(-1, -1, -1), 1) { }
    }
    /// <summary>
    /// Копье одноручное	
    /// </summary>
    internal class OneHandedSpear : Weapon
    {
        public OneHandedSpear() : base("Копье одноручное", 0, new Harm(0, 0, 0), new Effects(-1, -1, -1), 1) { }
    }
    /// <summary>
    /// Копье двуручное	
    /// </summary>
    internal class TwoHandedSpear : Weapon
    {
        public TwoHandedSpear() : base("Копье двуручное", 0, new Harm(0, 6, 0), new Effects(-1, -1, -1), 1) { }
    }
    /// <summary>
    /// Булава	
    /// </summary>
    internal class Mace : Weapon
    {
        public Mace() : base("Булава", 0, new Harm(0, 0, 2), new Effects(-1, -1, -1), 1) { }
    }
    /// <summary>
    /// Молот	
    /// </summary>
    internal class Hammer : Weapon
    {
        public Hammer() : base("Молот", 0, new Harm(0, 0, 5), new Effects(-1, -1, -1), 1) { }
    }
    /// <summary>
    /// Лук	
    /// </summary>
    internal class Bow : Weapon
    {
        public Bow() : base("Лук", 5, new Harm(0, 3, 0), new Effects(-1, -1, -1), 1) { }
    }
    /// <summary>
    /// Арбалет	
    /// </summary>
    internal class Crossbow : Weapon
    {
        public Crossbow() : base("Арбалет", 0, new Harm(0, 6, 0), new Effects(-1, -1, -1), 1) { }
    }
    /// <summary>
    /// Метательный топор	
    /// </summary>
    internal class ThrowingAx : Weapon
    {
        public ThrowingAx() : base("Метательный топор", 0, new Harm(0, 5, 0), new Effects(-1, -1, -1), 1) { }
    }
    internal class Hand : Weapon
    {
        public Hand() : base("Рука", 0, new Harm(0, 0, 1), new Effects(-1, -1, -1), 1) { }
    }
    #endregion Weapons
}