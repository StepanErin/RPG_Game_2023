using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static RPGGame.NotificationRaces;
using static System.Net.Mime.MediaTypeNames;

namespace RPGGame
{
    /// <summary>
    /// Требования к расам
    /// </summary>
    public interface IRacesItem
    {
        void Move();
        //void TakeDamage(float damage);
        void Hit(RacesItem person, PartBody siteOfInjury);
        void Hit(RacesItem person, PartBody[] siteOfInjury);
        void GetItem(Item item);
        //void GetWeapon(Weapon weapon);
        //void GetArmor(Armor armor);
    }
    internal interface INotificationRaces
    {
        /// <summary>
        /// Уведомление о смерти
        /// </summary>
        event NotificationRaces.DeathHandler Notify_Death;
        /// <summary>
        /// Уведомление о возраждении
        /// </summary>
        event NotificationRaces.ResurrectHandler Notify_Resurrect;

        /// <summary>
        /// Уведомление получении повреждении
        /// </summary>
        event NotificationRaces.GetDamageHandler Notify_GetDamage;
        /// <summary>
        /// Уведомление нанесении повреждении
        /// </summary>
        event NotificationRaces.SetDamageHandler Notify_SetDamage;
        /// <summary>
        /// Уведомление о использовании предмета
        /// </summary>
        event NotificationRaces.UseSubjectHandler Notify_UseSubject;

        event NotificationRaces.UseArmorHandler Notify_UseArmor;
        event NotificationRaces.UseWeaponHandler Notify_UseWeapon;

        /// <summary>
        /// Уведомление о недостатке пространства
        /// </summary>
        event NotificationRaces.GettingAnItem Notify_NotificationRaces;
    }
    public class NotificationRaces
    {
        #region Notify_Death
        /// <summary>
        /// Уведомление о смерти персонажа
        /// </summary>
        /// <param name="person_Name">Имя</param>
        public delegate void DeathHandler(string person_Name);
        public event DeathHandler Notify_Death;
        private protected void Notify_Death_Invoke(string name)
        {
            Notify_Death?.Invoke(name);
        }
        #endregion Notify_Death



        #region Notify_SetDamage
        /// <summary>
        /// Уведомление нанесении повреждении
        /// </summary>
        /// <param name="attacking_Name">Имя атакующего</param>
        /// <param name="defensive_Name">Имя защищающегося</param>
        /// <param name="weapon_Name">Название оружия</param>
        /// <param name="siteOfInjury">Место нанесённого урона</param>
        public delegate void SetDamageHandler(string attacking_Name, string defensive_Name, string weapon_Name, PartBody[] siteOfInjury);

        public event SetDamageHandler Notify_SetDamage;
        private protected void Notify_SetDamage_Invoke(string attacking_Name, string defensive_Name, string weapon_Name, PartBody[] siteOfInjury)
        {
            Notify_SetDamage?.Invoke(attacking_Name, defensive_Name, weapon_Name, siteOfInjury);
        }
        #endregion Notify_SetDamage



        #region Notify_GetDamage
        /// <summary>
        /// Уведомление получении повреждении
        /// </summary>
        /// <param name="defensive_Name">Имя</param>
        /// <param name="siteOfInjury">Место нанесённого урона</param>
        /// <param name="weapon_Name">Название оружия</param>
        /// <param name="damage">Урон</param>
        /// <param name="currentHealth">Текущее здоровье</param>
        public delegate void GetDamageHandler(string defensive_Name, PartBody[] siteOfInjury, string weapon_Name, float damage, float currentHealth);

        public event GetDamageHandler Notify_GetDamage;
        private protected void Notify_GetDamage_Invoke(string defensive_Name, PartBody[] siteOfInjury, string weapon_Name, float damage, float currentHealth)
        {
            Notify_GetDamage?.Invoke(defensive_Name, siteOfInjury, weapon_Name, damage, currentHealth);
        }
        #endregion Notify_GetDamage



        #region Notify_Resurrect
        /// <summary>
        /// Уведомление о воскрешении персонажа
        /// </summary>
        /// <param name="person_Name">Имя</param>
        /// <param name="maxResurrect">Количество оставшихся воскрешений</param>
        public delegate void ResurrectHandler(string person_Name, int maxResurrect);
        public event ResurrectHandler Notify_Resurrect;
        private protected void Notify_Resurrect_Invoke(string person_Name, int maxResurrect)
        {
            Notify_Resurrect?.Invoke(person_Name, maxResurrect);
        }
        #endregion Notify_Resurrect



        #region Notify_UseSubject
        /// <summary>
        /// Использование предмета
        /// </summary>
        /// <param name="person_Name">Имя персонажа</param>
        /// <param name="weapon_Name">Название предмета</param>
        public delegate void UseSubjectHandler(string person_Name, string weapon_Name, bool availability);
        public event UseSubjectHandler Notify_UseSubject;
        private protected void Notify_UseSubject_Invoke(string person_Name, string weapon_Name, bool availability)
        {
            Notify_UseSubject?.Invoke(person_Name, weapon_Name, availability);
        }
        #endregion Notify_UseSubject



        #region Notify_UseArmor
        /// <summary>
        /// Использование брони
        /// </summary>
        /// <param name="person_Name">Имя персонажа</param>
        /// <param name="armor_Name">Название брони</param>
        /// <param name="partBody">Место ношения брони</param>
        public delegate void UseArmorHandler(string person_Name, string armor_Name, PartBody partBody);
        public event UseArmorHandler Notify_UseArmor;
        private protected void Notify_UseArmor_Invoke(string person_Name, string armor_Name, PartBody partBody)
        {
            Notify_UseArmor?.Invoke(person_Name, armor_Name, partBody);
        }
        #endregion Notify_UseArmor



        #region Notify_UseWeapon
        /// <summary>
        /// Использование оружия
        /// </summary>
        /// <param name="person_Name">Имя персонажа</param>
        /// <param name="weapon_Name">Название оружия</param>
        public delegate void UseWeaponHandler(string person_Name, string weapon_Name);
        public event UseWeaponHandler Notify_UseWeapon;
        private protected void Notify_UseWeapon_Invoke(string person_Name, string weapon_Name)
        {
            Notify_UseWeapon?.Invoke(person_Name, weapon_Name);
        }
        #endregion Notify_UseWeapon


        #region Notify_NotificationRaces
        /// <summary>
        /// Недостаточно пространства
        /// </summary>
        /// <param name="person_Name">Имя персонажа</param>
        /// <param name="ItemName">Название предмета</param>
        public delegate void GettingAnItem(string person_Name, string ItemName, bool availability);
        public event GettingAnItem Notify_NotificationRaces;
        private protected void Notify_NotificationRaces_Invoke(string person_Name, string ItemName, bool availability)
        {
            Notify_NotificationRaces?.Invoke(person_Name, ItemName, availability);
        }
        #endregion Notify_NotificationRaces

    }


    #region Params
    public class Parameter
    {
        public Parameter(float currentValue, float maxValue, float limitValue)
        {
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            this.limitValue = limitValue;
        }
        protected private float currentValue = 0;
        protected private float maxValue = 0;
        protected private float limitValue = 0;
        public float CurrentValue { get { return currentValue; } set { currentValue = value; } }
        public float MaxValue { get { return maxValue; } }
        public float LimitValue { get { return limitValue; } }
    }
    public class Health : Parameter
    {
        public Health(float maxValue, float limitValue)
            : base(maxValue, maxValue, limitValue) { }
        public void Heals(float heals)
        {
            if (currentValue + heals >= maxValue)
                currentValue = maxValue;
            else
                currentValue += heals;
        }
        internal float TakeDamage(Weapon weapon, ArmorSet armor, PartBody[] siteOfInjury)
        {
            float damage = CalculateDamage(weapon, armor, siteOfInjury);
            if (currentValue - damage <= 0)
                currentValue = 0;
            else
                currentValue -= damage;
            return damage;
        }
        private float CalculateDamage(Weapon weapon, ArmorSet armor, PartBody[] siteOfInjury)
        {
            float summ = 0f;
            foreach (var item_W in siteOfInjury)
            {
                switch (item_W)
                {
                    case PartBody.Head:
                        Subtract(armor.ThisHelmet, 1.8f);
                        break;
                    case PartBody.Breast:
                        Subtract(armor.ThisBreastplate, 1.5f);
                        break;
                    case PartBody.Legs:
                        Subtract(armor.ThisLeggings, 0.8f);
                        break;
                    case PartBody.Feet:
                        Subtract(armor.ThisBoots, 0.8f);
                        break;
                    case PartBody.Hands:
                        Subtract(armor.ThisBracers, 0.5f);
                        break;
                }
            }
            return summ;

            void Subtract(Armor armor_, float x)
            {
                if (armor_ != null)
                {
                    T(weapon.Damage_.Chopping, armor_.Protection.Chopping, x);
                    T(weapon.Damage_.Crushing, armor_.Protection.Crushing, x);
                    T(weapon.Damage_.Stabbing, armor_.Protection.Stabbing, x);
                }
                else
                {
                    T(weapon.Damage_.Chopping, 0, x);
                    T(weapon.Damage_.Crushing, 0, x);
                    T(weapon.Damage_.Stabbing, 0, x);
                }
            }
            //           weapon         armor
            void T(float weapon_, float armor_, float x)
            {
                var temp = weapon_ - armor_;
                temp = (float)Math.Round((double)temp * x, 1);
                if (temp > 0)
                    summ += temp;
            }

        }
    }
    public class Speed : Parameter
    {
        public void T(Weapon weapon)
        {

        }
        public Speed(float maxValue, float limitValue)
            : base(maxValue, maxValue, limitValue) { }
    }
    public class Stealth : Parameter
    {
        public Stealth(float maxValue, float limitValue)
            : base(maxValue, maxValue, limitValue) { }
    }
    public class Force : Parameter
    {
        public Force(float maxValue, float limitValue)
            : base(maxValue, maxValue, limitValue) { }
    }
    #endregion Params
    public interface IParam
    {
        Parameter Health { get; }
        Parameter Speed { get; }
        Parameter Stealth { get; }
        Parameter Force { get; }
    }
    /// <summary>
    /// Характеристики
    /// </summary> 
    public class Characteristics : NotificationRaces, IParam
    {
        internal string Name { get; private protected set; }
        internal int MaxResurrect { get; private protected set; }
        internal bool Alive { get; private set; }
        private protected void Die()
        {
            Alive = false;
        }
        private protected void ResurrectBase()
        {
            if (MaxResurrect > 0)
            {
                MaxResurrect--;
                Alive = true;
            }
        }

        internal Inventory Inventory { get; private protected set; } = new Inventory();

        #region Param
        private protected Health health;
        public Parameter Health
        {
            get
            {
                return health;
            }
        }



        private protected Speed speed;
        public Parameter Speed
        {
            get
            {
                return speed;
            }
        }



        private protected Stealth stealth;
        public Parameter Stealth
        {
            get
            {
                return stealth;
            }
        }



        private protected Force force;
        public Parameter Force
        {
            get
            {
                return force;
            }
        }
        #endregion Param

        /// <summary>
        /// Проверить можно ли применить эфект
        /// </summary>
        /// <param name="requirement">эффекты</param>
        /// <returns>результат</returns>
        private protected bool CheckAvailability_Effects(Effects requirement)
        {
            return
            stealth.CurrentValue + requirement.Stealth > 0 &&
            speed.CurrentValue + requirement.Speed > 0 &&
            force.CurrentValue + requirement.Force > 0;
            /*
            return
            stealth.CurrentValue > Math.Abs(requirement.Stealth) &&
            speed.CurrentValue > Math.Abs(requirement.Speed) &&
            force.CurrentValue > Math.Abs(requirement.Force);*/
        }

        private protected void Apply_Effects(Effects requirement)
        {
            stealth.CurrentValue += requirement.Stealth;
            speed.CurrentValue += requirement.Speed;
            force.CurrentValue += requirement.Force;
        }
        private protected void Cancel_Effects(Effects requirement)
        {
            stealth.CurrentValue -= requirement.Stealth;
            speed.CurrentValue -= requirement.Speed;
            force.CurrentValue -= requirement.Force;
        }





        #region Equipment Экипировка
        public Weapon ThisWeapon { get; private protected set; }
        internal ArmorSet ThisArmor
        {
            get
            {
                return thisArmor;
            }
        }
        private protected ArmorSet thisArmor;
        #endregion Equipment Экипировка



        private protected void Heals(float heals)
        {
            if (!Alive) return;
            health.Heals(heals);
        }
        private protected void TakeDamage(Weapon weapon, PartBody[] siteOfInjury)
        {
            if (!Alive) return;
            var damage = health.TakeDamage(weapon, thisArmor, siteOfInjury);
            Notify_GetDamage_Invoke(Name, siteOfInjury, weapon.Name, damage, Health.CurrentValue);
            if (Health.CurrentValue == 0)
            {
                Notify_Death_Invoke(Name);
                Die();
            }
        }
    }
    public class UsingItems : Characteristics
    {
        private protected void UseHealing(Healing healing)
        {
            Heals(healing.HealingAmount);
        }
        private protected void UseWeapon(Weapon weapon)
        {
            ThisWeapon = weapon;
            Notify_UseWeapon_Invoke(Name, weapon.Name);
        }
        private protected void UseArmor(Armor armor)
        {
            switch (armor.PartOfBody)
            {
                case PartBody.Head:
                    thisArmor.ThisHelmet = (Helmet)armor;
                    break;
                case PartBody.Breast:
                    thisArmor.ThisBreastplate = (Breastplate)armor;
                    break;
                case PartBody.Legs:
                    thisArmor.ThisLeggings = (Leggings)armor;
                    break;
                case PartBody.Feet:
                    thisArmor.ThisBoots = (Boots)armor;
                    break;
                case PartBody.Hands:
                    thisArmor.ThisBracers = (Bracers)armor;
                    break;
            }
            Notify_UseArmor_Invoke(Name, armor.Name, armor.PartOfBody);
        }
    }
    public class RacesItem : UsingItems, IRacesItem, INotificationRaces
    {
        /// <summary>
        /// Начальные характеристики
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="health">Здоровье</param>
        /// <param name="speed">Скорость</param>
        /// <param name="stealth">Стелс</param>
        /// <param name="force">Сила</param>
        public RacesItem(string name, Health health, Speed speed, Stealth stealth, Force force)
        {
            Name = name;
            ThisWeapon = new Hand();

            thisArmor = new ArmorSet();

            this.health = health;
            this.speed = speed;
            this.stealth = stealth;
            this.force = force;


            MaxResurrect = 2;
            ResurrectBase();
        }



        #region Actions
        public void Move()
        {
            if (!Alive) return;
            Console.WriteLine($"Идет (скорость: {speed.CurrentValue})");
        }
        public void Hit(RacesItem person, PartBody siteOfInjury)
        {
            if (!Alive) return;
            Notify_SetDamage_Invoke(Name, person.Name, ThisWeapon.Name, new PartBody[] { siteOfInjury });
            person.TakeDamage(ThisWeapon, new PartBody[] { siteOfInjury });
        }
        public void Hit(RacesItem person, PartBody[] siteOfInjury)
        {
            if (!Alive) return;
            Notify_SetDamage_Invoke(Name, person.Name, ThisWeapon.Name, siteOfInjury);
            person.TakeDamage(ThisWeapon, siteOfInjury);
        }
        public void GetItem(Item item)
        {
            Notify_NotificationRaces_Invoke(Name, item.Name, Inventory.Add(item));
        }
        public void UseItem(int indexInInventory)
        {
            if (!Alive) return;

            Item item = Inventory.Items[indexInInventory];
            if (!CheckAvailability_Effects(item.Effects_))
            {
                Notify_UseSubject_Invoke(Name, item.Name, false);
                return;
            }

            Apply_Effects(item.Effects_);
            if (item is Weapon) UseWeapon((Weapon)item);
            else if (item is Armor) UseArmor((Armor)item);
            else if (item is Healing) UseHealing((Healing)item);
            else Notify_UseSubject_Invoke(Name, item.Name, true);
        }
        internal void Resurrect()
        {
            if (!Alive)
            {
                ResurrectBase();
                Notify_Resurrect_Invoke(Name, MaxResurrect);
            }
        }
        #endregion Actions
    }
    #region Races
    class Human : RacesItem
    {
        public Human(string name)
            : base(name,
                  new Health(3, 10),
                  new Speed(3, 10),
                  new Stealth(3, 10),
                  new Force(3, 10))
        { }
    }
    class Orc : RacesItem
    {
        public Orc(string name)
            : base(name,
                  new Health(5, 10),
                  new Speed(4, 10),
                  new Stealth(0, 10),
                  new Force(6, 10))
        { }
    }
    class Undead : RacesItem
    {
        public Undead(string name)
            : base(name,
                  new Health(1, 1),
                  new Speed(5, 10),
                  new Stealth(5, 10),
                  new Force(3, 10))
        { }
    }
    #endregion Races
}