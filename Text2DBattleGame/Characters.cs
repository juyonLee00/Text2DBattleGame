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
        float CriticalRate { get; set; }
        float CriticalAtk { get; set; }
        float Avoidability { get; set; }

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
        public int Mp { get; set; }
        public int MaxHp { get; }
        public int MaxMp { get; set; }
        public bool IsDead => Hp <= 0;
        public int DungeonLevel { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public float CriticalRate { get; set; }
        public float CriticalAtk { get; set; }
        public float Avoidability { get; set; }

        public Character() { }

        public List<IItem> Inventory { get; set; }
        public List<Skill> Skills { get; set; }
        public Character(string name, string job, int level, int atk, int def, int hp, int mp, int gold, List<Skill> skill, float criticalRate, float criticalAtk, float avoidability)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Mp = mp;
            MaxHp = hp;
            MaxMp = mp;
            Gold = gold;
            Exp = 0;
            DungeonLevel = 1;
            Inventory = new List<IItem>();
            Skills = skill;
            CriticalRate = criticalRate;
            CriticalAtk = criticalAtk;
            Avoidability = avoidability;
        }
        public void TakeDamage(int damage)
        {
            Hp -= damage;
        }

        public void UsingMp(int useMp)
        {
            Mp -= useMp;

            if (Mp < 0)
                Mp = 0;
        }

        public void EquipItem(int itemIdx)
        {
            IItem curItem = Inventory[itemIdx];
            char curItemType = curItem.ItemType;

            if(curItemType == 'p')
            {
                Console.WriteLine("포션은 장착할 수 없습니다!");
                Program.scene = Scene.Inventory;
            }

            else
            {
                if (!curItem.IsEquip)
                {
                    foreach (IItem item in Inventory)
                    {
                        if (item.ItemType == curItemType && item.IsEquip)
                        {
                            item.IsEquip = false;
                            Console.WriteLine($"아이템이 {item.Name}에서 {curItem.Name}으로 교체되었습니다.");
                            break;
                        }
                    }
                    curItem.IsEquip = true;

                }
                else
                {
                    Console.WriteLine("이미 착용한 아이템입니다.");
                }
            }
            Console.WriteLine("\n0. 나가기");

            int input = Program.CheckValidInput(0, 0);
            switch(input)
            {
                case 0:
                    Program.scene = Scene.GameIntro;
                    break;
            }

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
        public float CriticalRate { get; set; }
        public float CriticalAtk { get; set; }
        public float Avoidability { get; set; }
        public Monster(string name, int level, int hp, int atk, int exp , int gold, float criticalRate, float criticalAtk, float avoidability)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Hp = hp;
            Exp = exp;
            Gold =gold;
            CriticalRate = criticalRate;
            CriticalAtk = criticalAtk;
            Avoidability = avoidability;
        }
        public void TakeDamage(int damage)
        {
            Hp -= damage;
        }
        static public IItem Drop(List<IItem> droptable)
        {
            int persent = 100; //드랍확률
            Random random = new Random();
            int basic = random.Next(1, 100);
            int i = random.Next(0, droptable.Count);
            if (persent >= basic)
            {
                IItem item = droptable[i];
                return item;
            }
            return null;
        }
    
    }
    public class Minion : Monster
    {
        public Minion() : base("미니언", 2, 15, 5 , 1 ,5, 0f, 0f, 10f) { }


    }
    public class EmptinessBug : Monster
    {
        public EmptinessBug() : base("공허충", 3, 10, 5, 2, 10, 0f, 0f, 10f) { }
    }
    public class CanonMinion : Monster
    {
        public CanonMinion() : base("대포미니언", 5, 25, 8, 3, 15, 0f, 0f, 10f) { }
    }
    public class CorruptedSpider : Monster
    {
        public CorruptedSpider() : base("타락한 거미", 7, 25, 10, 5, 20, 0f, 0f, 10f) { }
    }
    public class Hiding : Monster
    {
        public Hiding() : base("그림자속무언가", 7, 5, 14, 5, 20, 0f, 0f, 10f) { }
    }
    public class CorruptedQueen : Monster
    {
        public CorruptedQueen() : base("타락한 거미여왕", 10, 40, 15, 10, 30, 0f, 0f, 10f) { }
    }
    public class WaterSnake : Monster
    {
        public WaterSnake() : base("수중뱀", 10, 25, 12, 7, 25, 0f, 0f, 10f) { }
    }
    public class WaterSerpent : Monster
    {
        public WaterSerpent() : base("수중서펀트", 10, 40, 16, 8, 27, 0f, 0f, 10f) { }
    }
    public class Drangon : Monster
    {
        public Drangon() : base("드래곤", 15, 60, 20, 20, 50, 0f, 0f, 10f) { }
    }

    public class CreateCharacter
    {
        public static Monster[] CreateRandomMonster(int dungeonlevel)
        {
            Monster monster = new Monster("zizon", 100, 1000, 99, 0, 0, 0f, 0f, 10f);

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
