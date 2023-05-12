using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    public class Armor : Item
    {
        /// <summary>
        /// Новоя броня
        /// </summary>
        /// <param name="partBody">Место защиты</param>
        /// <param name="name">Название</param>
        /// <param name="protection">Защита</param>
        /// <param name="requirement">Требование к умениям</param>
        public Armor(PartBody partBody, string name, Harm protection, Effects requirement, float size)
            : base(name, size, requirement)
        {
            Protection = protection;
            PartOfBody = partBody;
        }
        /// <summary>
        /// Все типы защиты
        /// </summary>
        internal Harm Protection { get; private set; }
        /// <summary>
        /// защита части тела
        /// </summary>
        public PartBody PartOfBody { get; private set; }
    }
    public enum PartBody
    {
        /// <summary>
        /// Голова
        /// </summary>
        Head,
        /// <summary>
        /// Грудь
        /// </summary>
        Breast,
        /// <summary>
        /// Руки
        /// </summary>
        Hands,
        /// <summary>
        /// Ноги
        /// </summary>
        Legs,
        /// <summary>
        /// Стопы
        /// </summary>
        Feet,
    }

    internal struct ArmorSet
    {
        public Helmet ThisHelmet;
        public Breastplate ThisBreastplate;
        public Leggings ThisLeggings;
        public Boots ThisBoots;
        public Bracers ThisBracers;
    }

    /// <summary>
    /// Требования к умениям
    /// </summary>
    public struct Effects
    {
        /// <summary>
        /// Задать значения требований
        /// </summary>
        /// <param name="speed">Скорость</param>
        /// <param name="stealth">Скрытность</param>
        /// <param name="forse">Сила</param>
        public Effects(float speed, float stealth, float forse)
        {
            Speed = speed;
            Stealth = stealth;
            Force = forse;
        }
        internal float Speed { get; private set; }
        internal float Stealth { get; private set; }
        internal float Force { get; private set; }

        public new string ToString()
        {
            return $"[speed = {Speed}; stealth = {Stealth}; force = {Force}]";
        }
    }
    #region Armor
    /// <summary>
    /// Шлем
    /// </summary>
    class Helmet : Armor
    {
        /// <summary>
        /// Шлем
        /// </summary>
        public Helmet() : base(PartBody.Head, "Шлем", new Harm(1, 1, 1), new Effects(-1, -1, 0), 1) { }
    }

    /// <summary>
    /// Нагрудник
    /// </summary>
    class Breastplate : Armor
    {
        /// <summary>
        /// Нагрудник
        /// </summary>
        public Breastplate() : base(PartBody.Breast, "Нагрудник", new Harm(1, 1, 1), new Effects(-1, -1, 0), 1) { }
    }

    /// <summary>
    /// Поножи
    /// </summary>
    class Leggings : Armor
    {
        /// <summary>
        /// Поножи
        /// </summary>
        public Leggings() : base(PartBody.Legs, "Поножи", new Harm(1, 1, 1), new Effects(-1, -1, 0), 1) { }
    }

    /// <summary>
    /// Ботинки
    /// </summary>
    class Boots : Armor
    {
        /// <summary>
        /// Ботинки
        /// </summary>
        public Boots() : base(PartBody.Feet, "Ботинки", new Harm(1, 1, 1), new Effects(-1, -1, 0), 1) { }
    }
    /// <summary>
    /// Наручни
    /// </summary>
    class Bracers : Armor
    {
        /// <summary>
        /// Наручни
        /// </summary>
        public Bracers() : base(PartBody.Hands, "Наручни", new Harm(1, 1, 1), new Effects(-1, -1, 0), 1) { }
    }
    #endregion Armor
}
