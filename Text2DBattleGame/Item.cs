﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    public interface IItem
    {
        public string Name { get; }
        public string Description { get; }
        public bool IsEquip { get; set; }
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
        public AttackItem(string name, int atk, int def, int hp) 
        { 
           Name=name; 
           Atk=atk;
           Def=def;
           Hp=hp;
        }

        public AttackItem(string name, string description, bool isEquip, int atk, int def, int hp, int mp)
        {
            this.Name = name;
            this.Description = description;
            this.IsEquip = isEquip;
            this.Atk = atk;
            this.Def = def;
            this.Hp = hp;
            this.Mp = mp;
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

        public DefenseItem(string name, string description, bool isEquip, int atk, int def, int hp, int mp)
        {
            this.Name = name;
            this.Description = description;
            this.IsEquip = isEquip;
            this.Atk = atk;
            this.Def = def;
            this.Hp = hp;
            this.Mp = mp;
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

        public PotionItem(string name, string description, bool isEquip, int atk, int def, int hp, int mp)
        {
            this.Name = name;
            this.Description = description;
            this.IsEquip = isEquip;
            this.Atk = atk;
            this.Def = def;
            this.Hp = hp;
            this.Mp = mp;
        }
    }

}
