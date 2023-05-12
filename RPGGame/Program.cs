using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPGGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RacesItem human = new Human("Челолюдь");
            RacesItem okr = new Orc("ОКР");
            HangEvents(human);
            HangEvents(okr);

            human.GetItem(new Helmet());        //0
            human.GetItem(new Leggings());      //1
            human.GetItem(new Bracers());       //2
            human.GetItem(new Breastplate());   //3
            human.GetItem(new Bow());           //4
            human.GetItem(new PowerPendant());  //5
            human.GetItem(new PowerPendant());  //6
            human.GetItem(new PowerPendant());  //7
            human.GetItem(new Boots());         //8

            human.UseItem(1);
            human.UseItem(5);
            ShowArmor(human);
            ShowInventory(human);
            ShowWeapon(human);


            human.Hit(okr, PartBody.Head);
            okr.Hit(human, new PartBody[] { PartBody.Breast, PartBody.Breast, PartBody.Breast });
            okr.Hit(human, PartBody.Legs);
            human.Resurrect();

            Console.ReadLine();


            /*
            RacesItem human = new Human("Челолюдь");
            //RacesItem orc = new Orc("ОКР");
            RacesItem undead = new Undead("Няжить");


            HangEvents(undead);
            HangEvents(human);
            human.GetWeapon(new Bow());
            human.GetArmor(new Breastplate());




            undead.GetWeapon(new SwordOneHanded());
            undead.GetArmor(new Helmet());
            undead.GetArmor(new Breastplate());
            undead.GetArmor(new Leggings());
            undead.GetArmor(new Boots());
            undead.GetArmor(new Bracers());



            ShowArmor(human);
            ShowWeapon(human);

            ShowArmor(undead);
            ShowWeapon(undead);



            human.Hit(undead, PartBody.Helmet);
            undead.Resurrect();
            undead.Hit(human, PartBody.Helmet);
            */
        }

        private static void HangEvents(RacesItem racesItem)
        {
            racesItem.Notify_Death += RacesItem_Notify_Death;
            racesItem.Notify_SetDamage += RacesItem_Notify_SetDamage;
            racesItem.Notify_GetDamage += RacesItem_Notify_GetDamage;
            racesItem.Notify_UseArmor += RacesItem_Notify_UseArmor;
            racesItem.Notify_UseWeapon += RacesItem_Notify_UseWeapon; ;
            racesItem.Notify_Resurrect += RacesItem_Notify_Resurrect;
            racesItem.Notify_NotificationRaces += RacesItem_Notify_NotificationRaces;
            racesItem.Notify_UseSubject += RacesItem_Notify_UseSubject;
        }

        private static void RacesItem_Notify_UseWeapon(string person_Name, string weapon_Name)
        {
            Console.WriteLine($"<{person_Name}> взял в руки большой <{weapon_Name}>");
        }

        private static void RacesItem_Notify_UseSubject(string person_Name, string weapon_Name, bool availability)
        {
            if (availability)
                Console.WriteLine($"<{person_Name}> использует предмет <{weapon_Name}>");
            else
                Console.WriteLine($"<{person_Name}> не смог использвать предмет <{weapon_Name}>, так как у него не хватило сил");
        }

        private static void ShowArmor(RacesItem racesItem)
        {
            Console.WriteLine($"Броня персонажа <{racesItem.Name}>:");
            T(racesItem.ThisArmor.ThisHelmet);
            T(racesItem.ThisArmor.ThisBreastplate);
            T(racesItem.ThisArmor.ThisLeggings);
            T(racesItem.ThisArmor.ThisBoots);
            T(racesItem.ThisArmor.ThisBracers);
            void T(Armor armor)
            {
                if (armor != null) Console.WriteLine($"   -{armor.Name}({armor.PartOfBody}, {armor.Protection.ToString()}, требование к выносливости - {armor.Effects_.ToString()})");
            }
        }
        private static void ShowWeapon(RacesItem racesItem)
        {
            Console.WriteLine($"Оружие персонажа <{racesItem.Name}>:");
            T(racesItem.ThisWeapon);
            void T(Weapon weapon)
            {
                if (weapon != null) Console.WriteLine($"   -{weapon.Name}({weapon.Damage_.ToString()}, дистанция отаки-{weapon.Distance}, требование к выносливости - {weapon.Effects_.ToString()})");
            }
        }

        private static void ShowInventory(RacesItem racesItem)
        {
            Console.WriteLine($"В инвентаре <{racesItem.Name}> есть:");
            foreach (var item in racesItem.Inventory.GetArrName()) Console.WriteLine($"\t{item}");
            Console.WriteLine("__________________________________________________");
        }

        private static void RacesItem_Notify_Resurrect(string person_Name, int maxResurrect)
        {
            Console.WriteLine($"{person_Name} не понравилось у Андрея и он решил возрадиться, оставшиеся количество возраждений: {maxResurrect}");
        }

        private static void RacesItem_Notify_UseArmor(string person_Name, string armor_Name, PartBody partBody)
        {
            string temp = "";
            switch (partBody)
            {
                case PartBody.Head:
                    temp = "Голова";
                    break;
                case PartBody.Breast:
                    temp = "Грудь";
                    break;
                case PartBody.Legs:
                    temp = "Ноги";
                    break;
                case PartBody.Feet:
                    temp = "Стопы";
                    break;
                case PartBody.Hands:
                    temp = "Руки";
                    break;
            }
            Console.WriteLine($"<{person_Name}> надел броню: <{armor_Name}>({temp})");
        }

        private static void RacesItem_Notify_GetDamage(string defensive_Name, PartBody[] siteOfInjury, string weapon_Name, float damage, float currentHealth)
        {
            string temp = "";
            foreach (var item in siteOfInjury) temp += $", <{PartOfTheBody(item)}>";
            temp = temp.Remove(0, 2);
            Console.WriteLine($"<{defensive_Name}> получил по {temp} оружием <{weapon_Name}> (урон: {damage}), здоровье: {currentHealth}");
            string PartOfTheBody(PartBody partBody)
            {
                switch (partBody)
                {
                    case PartBody.Head:
                        return "Голове";
                    case PartBody.Breast:
                        return "Груди";
                    case PartBody.Legs:
                        return "Ногам";
                    case PartBody.Feet:
                        return "Ногам";
                    case PartBody.Hands:
                        return "Рукам";
                    default:
                        return "";
                }
            }
        }

        private static void RacesItem_Notify_SetDamage(string attacking_Name, string defensive_Name, string weapon_Name, PartBody[] siteOfInjury)
        {
            Console.WriteLine($"<{attacking_Name}> нанёс урон <{defensive_Name}> оружеем <{weapon_Name}>");
        }

        private static void RacesItem_Notify_Death(string name)
        {
            Console.WriteLine($"<{name}> помер и отправился к Андрею в гости");
        }

        private static void RacesItem_Notify_NotificationRaces(string person_Name, string ItemName, bool availability)
        {
            if (availability)
                Console.WriteLine($"<{person_Name}> гдето нашёл <{ItemName}> и запихнул в карман");
            else
                Console.WriteLine($"У <{person_Name}> кончилось место в карманах и он не смог запихнуть <{ItemName}> в них");
        }

    }
}

class Animal // base, instance
{
    public virtual void Move() { }
}
class Cat : Animal // derived
{
    public override void Move() { } // ~~
}
abstract class Shape // base, no instance
{
    public abstract void Draw();
}
class Circle : Shape // derived
{
    public override void Draw() // !!!
    {

    }
}

public interface IShape
{
    void Draw();
}
class Rectangle : IShape
{
    public void Draw()
    {
        Console.WriteLine("Rect Draw");
    }
}
