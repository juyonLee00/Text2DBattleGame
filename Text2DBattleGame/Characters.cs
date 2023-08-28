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
        public int Gold { get; }
        public int Exp { get; set; }

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

    public class CreateCharacter
    {
        public static Monster[] CreateRandomMonster()
        {
            Monster monster = new Monster("zizon", 100, 1000, 99, 0, 0);

            Random random = new Random();

            int howMany = random.Next(1, 5);
            Monster[] battleMonsters = new Monster[howMany];

            for (int i = 0; i < howMany; i++)
            {
                int flag = random.Next(0, 3);
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
                }

                battleMonsters[i] = monster;
            }

            return battleMonsters;
        }
    }
}
