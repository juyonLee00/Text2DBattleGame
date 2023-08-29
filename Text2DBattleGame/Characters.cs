using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    public interface ICharacter
    {
        string Name { get; }
        int Hp { get; set; }
        int Atk { get; }
        bool IsDead { get; }
        int Level { get; set; }
        int Exp { get; set; }
        int Gold { get; set; }

        void TakeDamage(int damage);
    }

    public class Character : ICharacter
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; }
        public bool IsDead => Hp <= 0;
        public int DungeonLevel { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }

        public Character() { }

        public List<Item> Inventory { get; set; }
        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            MaxHp = hp;
            Gold = gold;
            Exp = 0;
            DungeonLevel = 1;
            Inventory = new List<Item>();
        }
        public void TakeDamage(int damage)
        {
            Hp -= damage;
        }
    }

    public class Monster : ICharacter
    {
        public string Name { get; }
        public int Level { get; set; }
        public int Atk { get; }
        public int Hp { get; set; }
        public int Exp  { get; set; }
         public bool IsDead => Hp <= 0;
        public int Gold { get; set; }
        public Monster(string name, int level, int hp, int atk, int exp , int gold)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Hp = hp;
            Exp = exp;
            Gold =gold;
        }
        public void TakeDamage(int damage)
        {
            Hp -= damage;
        }
        static public Item Drop(List<Item> droptable)
        {
            int persent = 100; //드랍확률
            Random random = new Random();
            int basic = random.Next(1, 100);
            int i = random.Next(0, droptable.Count);
            if (persent >= basic)
            {
                Item item = droptable[i];
                return item;
            }
            return null;
        }
    
    }
    public class Minion : Monster
    {
        public Minion() : base("미니언", 2, 15, 5 , 1 ,5) { }


    }
    public class EmptinessBug : Monster
    {
        public EmptinessBug() : base("공허충", 3, 10, 5, 2, 10) { }
    }
    public class CanonMinion : Monster
    {
        public CanonMinion() : base("대포미니언", 5, 25, 8, 3, 15) { }
    }
    public class CorruptedSpider : Monster
    {
        public CorruptedSpider() : base("타락한 거미", 7, 25, 10, 5, 20) { }
    }
    public class Hiding : Monster
    {
        public Hiding() : base("그림자속무언가", 7, 5, 14, 5, 20) { }
    }
    public class CorruptedQueen : Monster
    {
        public CorruptedQueen() : base("타락한 거미여왕", 10, 40, 15, 10, 30) { }
    }
    public class WaterSnake : Monster
    {
        public WaterSnake() : base("수중뱀", 10, 25, 12, 7, 25) { }
    }
    public class WaterSerpent : Monster
    {
        public WaterSerpent() : base("수중서펀트", 10, 40, 16, 8, 27) { }
    }
    public class Drangon : Monster
    {
        public Drangon() : base("드래곤", 15, 60, 20, 20, 50) { }
    }

    public class CreateCharacter
    {
        public static Monster[] CreateRandomMonster(int dungeonlevel)
        {
            Monster monster = new Monster("zizon", 100, 1000, 99, 0, 0);

            Random random = new Random();

            int howManyMax = 4;
            int howManyMin = 1;
            if (dungeonlevel < 16)
            {
                if (dungeonlevel % 5 == 2 || dungeonlevel % 5 == 4)
                {
                    howManyMax = 5;
                }
                if (dungeonlevel % 5 == 0)
                {
                    howManyMin = 4;
                }
            }
            else
            {
                howManyMin = 1 + (dungeonlevel - 16);
                howManyMax = 4 + (dungeonlevel - 16);
            }

            int howMany = random.Next(howManyMin, howManyMax + 1);
            Monster[] battleMonsters = new Monster[howMany];

            for (int i = 0; i < howMany; i++)
            {
                int endNum = ((dungeonlevel - 1) / 5 + 1) * 3;
                int startNum = (dungeonlevel - 5 * ((dungeonlevel - 1) / 5) - 1) / 2 - 3 + endNum;

                if (dungeonlevel >= 16)
                {
                    startNum = 0;
                    endNum = 9;
                }

                int flag = random.Next(startNum, endNum);
                switch (flag)
                {
                    case 0:
                        monster = new Minion();
                        break;
                    case 1:
                        monster = new EmptinessBug();
                        break;
                    case 2:
                        monster = new CanonMinion();
                        break;
                    case 3:
                        monster = new CorruptedSpider();
                        break;
                    case 4:
                        monster = new Hiding();
                        break;
                    case 5:
                        monster = new CorruptedQueen();
                        break;
                    case 6:
                        monster = new WaterSnake();
                        break;
                    case 7:
                        monster = new WaterSerpent();
                        break;
                    case 8:
                        monster = new Drangon();
                        break;
                }

                battleMonsters[i] = monster;
            }

            return battleMonsters;
        }
    }
}
