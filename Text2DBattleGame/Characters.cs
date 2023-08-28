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
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; set; }
        public bool IsDead => Hp <= 0;
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage}의 데미지를 받았습니다. 남은 체력: {Hp}");
        }
    }

    public class Monster : ICharacter
    {
        public string Name { get; }
        public int Level { get; set; }
        public int Atk { get; }
        public int Hp { get; set; }
        public bool IsDead => Hp <= 0;

        public Monster(string name, int level, int hp, int atk)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Hp = hp;
        }
        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage}의 데미지를 받았습니다. 남은 체력: {Hp}");
        }
    }
    public class Minion : Monster
    {
        public Minion() : base("미니언", 2, 15, 5) { }
    }
    public class EmptinessBug : Monster
    {
        public EmptinessBug() : base("공허충", 3, 10, 5) { }
    }
    public class CanonMinion : Monster
    {
        public CanonMinion() : base("대포미니언", 5, 25, 8) { }
    }
    //public class CreateCharacter
    //{
    //    public static Monster CreateRandomMonster()
    //    {
    //        Random random = new Random();
    //        int flag = random.Next()
    //    }
    //}
}
