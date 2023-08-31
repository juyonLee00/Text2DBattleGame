using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Text2DBattleGame
{
    public interface IItem
    {
        public string Name { get; }
        public string Description { get; }
        public bool IsEquip { get; set; }
        public char ItemType { get; set; }
        public bool CanUse { get; set; }
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
        public char ItemType { get; set; }
        public bool CanUse { get; set; }

        public AttackItem() { }

        public AttackItem(string name, int atk, int def, int hp)//드랍 테스트용으로 만듬 나중에 없어질듯함
        {
            Name = name;
            Atk = atk;
            Def = def;
            Hp = hp;
        }

        [JsonConstructor]
        public AttackItem(string name, string description, bool isEquip, int atk, int def, int hp, int mp, char itemType)
        {
            this.Name = name;
            this.Description = description;
            this.IsEquip = isEquip;
            this.Atk = atk;
            this.Def = def;
            this.Hp = hp;
            this.Mp = mp;
            this.ItemType = itemType;
            CanUse = false;
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
        public char ItemType { get; set; }
        public bool CanUse { get; set; }

        public DefenseItem() { }

        public DefenseItem(string name, string description, bool isEquip, int atk, int def, int hp, int mp, char itemType)
        {
            this.Name = name;
            this.Description = description;
            this.IsEquip = isEquip;
            this.Atk = atk;
            this.Def = def;
            this.Hp = hp;
            this.Mp = mp;
            this.ItemType = itemType;
            CanUse = false;
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
        public char ItemType { get; set; }
        public int Count { get; set; }
        public bool CanUse { get; set; }

        public PotionItem() { }

        public PotionItem(string name, string description, bool isEquip, int atk, int def, int hp, int mp, char itemType)
        {
            this.Name = name;
            this.Description = description;
            this.IsEquip = isEquip;
            this.Atk = atk;
            this.Def = def;
            this.Hp = hp;
            this.Mp = mp;
            this.ItemType = itemType;
            Count = 1;
            CanUse = true;
    }

        public void Use(Character player, int i)
        {
            player.Hp += Hp;
            player.Mp += Mp;
            player.Atk += Atk;
            player.Def += Def;
            Count--;
            if (Count <= 0)
                player.Inventory.RemoveAt(i - 1);

        }
    }

}
