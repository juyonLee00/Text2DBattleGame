using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    public class Item
    {   
        public string Name { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }

        public Item(string name , int atk, int def, int hp) 
        { 
            Name = name;
            Atk= atk;
            Def = def;
            Hp = hp;
        }
    }
}
