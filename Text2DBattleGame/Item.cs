using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Text2DBattleGame
{
    public enum ItemType
    {
        Potion, Attack, Defense
    }
    public interface IItem
    {
        public string Name { get; }
        public string Description { get; }
        public bool IsEquip { get; set; }
        public ItemType ItemType { get; set; }
        public int Price { get; set; }

        public object CreateClone();
    }

    public class AttackItem : IItem
    {
        public string Name { get; }
        public string Description { get; }
        public bool IsEquip { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public ItemType ItemType { get; set; }
        public int Price { get; set; }

        public AttackItem() { }

        public AttackItem(string name, int atk, int def, int hp, int price)
        {
            Name = name;
            Atk = atk;
            Def = def;
            Hp = hp;
            ItemType = ItemType.Attack;
            Price = price;
        }

        public object CreateClone()
        {
            return MemberwiseClone();
        }

        [JsonConstructor]
        public AttackItem(string name, string description, bool isEquip, int atk, int def, int hp, int mp, ItemType itemType, int price)
        {
            this.Name = name;
            this.Description = description;
            this.IsEquip = isEquip;
            this.Atk = atk;
            this.Def = def;
            this.Hp = hp;
            this.Mp = mp;
            this.ItemType = itemType;
            this.Price = price;
        }
    }

    public class DefenseItem : IItem
    {   
        public string Name { get; set; }
        public string Description { get; }
        public bool IsEquip { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public ItemType ItemType { get; set; }
        public int Price { get; set; }

        public DefenseItem() { }

        public DefenseItem(string name, string description, bool isEquip, int atk, int def, int hp, int mp, int price)
        {
            this.Name = name;
            this.Description = description;
            this.IsEquip = isEquip;
            this.Atk = atk;
            this.Def = def;
            this.Hp = hp;
            this.Mp = mp;
            this.ItemType = ItemType.Defense;
            this.Price = price;
        }

        public object CreateClone()
        {
            return MemberwiseClone();
        }
    }

    public class PotionItem : IItem
    {
        public string Name { get; set; }
        public string Description { get; }
        public bool IsEquip { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public ItemType ItemType { get; set; }
        public int Price { get; set; }

        public PotionItem() { }

        public PotionItem(string name, string description, bool isEquip, int atk, int def, int hp, int mp, int price)
        {
            this.Name = name;
            this.Description = description;
            this.IsEquip = isEquip;
            this.Atk = atk;
            this.Def = def;
            this.Hp = hp;
            this.Mp = mp;
            this.ItemType = ItemType.Potion;
            this.Price = price;
        }

        public object CreateClone()
        {
            return MemberwiseClone();
        }
    }

}
