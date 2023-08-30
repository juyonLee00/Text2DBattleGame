using System;
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
        public bool isEquip { get; set; }
    }

    public class AttackItem : IItem
    {
        public string Name { get; }
        public string Description { get; }
        public bool isEquip { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }

    }

    public class DefenseItem : IItem
    {   
        public string Name { get; set; }
        public string Description { get; }
        public bool isEquip { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        
    }

    public class PotionItem : IItem
    {
        public string Name { get; set; }
        public string Description { get; }
        public bool isEquip { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
    }

}
