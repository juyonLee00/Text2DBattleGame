using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    public class Skill
    {
        public string Name { get; }
        public int Atk { get; }
        public int Mp { get; }

        public Func<Character, string> ShowExplanation { get; set; }

        public Action<Character, List<Monster>> UsingSkill { get; set; }

        public Skill(string name, int atk, int mp)
        {
            Name = name;
            Atk = atk;
            Mp = mp;
        }
    }

    // 임시저장용
    /*
    public List<Skill> GetWarriorSkills(Character player, List<Monster> monsters)
    {
        List<Skill> skills = new List<Skill>();

        Skill warriorSkill1 = new Skill("알파 스트라이크", 2, 10);
        warriorSkill1.ShowExplanation = () => $"{player.Atk * 2f}의 데미지로 하나의 적을 공격합니다.";
        // 충돌방지 임시 형변환
        warriorSkill1.UsingSkill = () => { monsters[new Random().Next(0, monsters.Count)].TakeDamage((int)(player.Atk * 2f)); };

        Skill warriorSkill2 = new Skill("더블 스트라이크", 3, 15);
        warriorSkill2.ShowExplanation = () => $"{player.Atk * 1.5f}의 데미지로 2명의 적을 랜덤으로 공격합니다";
        warriorSkill2.UsingSkill = () =>
        {
            List<int> randomNums = new List<int>();

            for (int i = 0; i < 2; i++)
            {
                int n;

                do
                {
                    n = new Random().Next(0, monsters.Count);
                }
                while (randomNums.Contains(n));

                randomNums.Add(n);
                // 충돌방지 임시 형변환
                monsters[randomNums[i]].TakeDamage((int)(player.Atk * 1.5f));
            }
        };

        skills.Add(warriorSkill1);
        skills.Add(warriorSkill2);


        return skills;
    }
    */
}
