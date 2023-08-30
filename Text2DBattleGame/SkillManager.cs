using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    internal class SkillManager
    {
        #region Warrior
        public List<Skill> GetWarriorSkills()
        {
            List<Skill> skills = new List<Skill>();

            Skill warriorSkill1 = new Skill("알파 스트라이크", 2, 10);
            warriorSkill1.ShowExplanation = (Character player) => $"{player.Atk * 2f}의 데미지로 하나의 적을 공격합니다.";
            warriorSkill1.UsingSkill = Warrior_AlphaStrike;

            Skill warriorSkill2 = new Skill("더블 스트라이크", 3, 15);
            warriorSkill2.ShowExplanation = (Character player) => $"{player.Atk * 1.5f}의 데미지로 2명의 적을 랜덤으로 공격합니다";
            warriorSkill2.UsingSkill = Warrior_DoubleStrike;

            skills.Add(warriorSkill1);
            skills.Add(warriorSkill2);


            return skills;
        }

        private void Warrior_AlphaStrike(Character player, Monster[] monsters)
        {
            //원본
            //monsters[new Random().Next(0, monsters.Length)].TakeDamage((int)(player.Atk * 2f));

            DisplayBattle.WriteBattle();

            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine((i+1) + " Lv." + monsters[i].Level + " " + monsters[i].Name + "  Dead ");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine((i + 1) + " Lv." + monsters[i].Level + " " + monsters[i].Name + "  HP " + monsters[i].Hp);
                }
            }

            Console.WriteLine("\n\n[내정보]");
            Console.WriteLine("Lv." + player.Level + " " + player.Name + " (" + player.Job + ")");
            Console.WriteLine("Hp " + player.Hp + "/" + player.MaxHp);
            Console.WriteLine("MP " + player.Mp + "/" + player.MaxMp);

            Console.WriteLine();
            Console.Write("대상을 선택해주세요.\n>>");


            List<Monster> hitMobs = new List<Monster>();

            int monsterNum;

            while (true)
            {
                monsterNum = Program.CheckValidInput(1, monsters.Length);

                if (monsters[monsterNum - 1].IsDead)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                else
                {
                    hitMobs.Add(monsters[monsterNum - 1]);

                    break;
                }
            }

            DisplayBattle.WriteBattle();

            DisplayBattle.SkillAttack(player, hitMobs, (int)(player.Atk * 2f));


            // 구조상 차라리 Display Battle을 매니저처럼 쓰는게 좋을것 같아 위처럼 했습니다.
        }

        private void Warrior_DoubleStrike(Character player, Monster[] monsters)
        {
            /*
            List<int> randomNums = new List<int>();

            int num = (monsters.Length >= 3) ? 2 : monsters.Length;

            for (int i = 0; i < num; i++)
            {
                int n;

                do
                {
                    n = new Random().Next(0, monsters.Length);
                }
                while (randomNums.Contains(n));

                randomNums.Add(n);

                monsters[randomNums[i]].TakeDamage((int)(player.Atk * 1.5f));
            }
            */

            List<Monster> hitMobs = new List<Monster>();


            // 랜덤 2마리 뽑기
            List<int> randomNums = new List<int>();

            int num = (monsters.Length >= 3) ? 2 : monsters.Length;

            for (int i = 0; i < num; i++)
            {
                int n;

                do
                {
                    n = new Random().Next(0, monsters.Length);
                }
                while (randomNums.Contains(n));

                randomNums.Add(n);
                hitMobs.Add(monsters[n]);
            }


            DisplayBattle.WriteBattle();

            DisplayBattle.SkillAttack(player, hitMobs, (int)(player.Atk * 1.5f));
        }

        #endregion

        #region Wizard

        public List<Skill> GetWizardSkills()
        {
            List<Skill> skills = new List<Skill>();

            Skill wizardSkill1 = new Skill("파이어볼", 2, 10);
            wizardSkill1.ShowExplanation = (Character player) => $"{player.Atk * 3f}의 데미지로 하나의 적을 공격합니다.";
            wizardSkill1.UsingSkill = Wizard_FireBall;

            Skill wizardSkill2 = new Skill("메테오", 3, 15);
            wizardSkill2.ShowExplanation = (Character player) => $"{player.Atk * 1.0f}의 데미지로 3명의 적을 랜덤으로 공격합니다";
            wizardSkill2.UsingSkill = Wizard_Meteor;

            skills.Add(wizardSkill1);
            skills.Add(wizardSkill2);


            return skills;
        }

        private void Wizard_FireBall(Character player, Monster[] monsters)
        {
            DisplayBattle.WriteBattle();

            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine((i + 1) + " Lv." + monsters[i].Level + " " + monsters[i].Name + "  Dead ");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine((i + 1) + " Lv." + monsters[i].Level + " " + monsters[i].Name + "  HP " + monsters[i].Hp);
                }
            }

            Console.WriteLine("\n\n[내정보]");
            Console.WriteLine("Lv." + player.Level + " " + player.Name + " (" + player.Job + ")");
            Console.WriteLine("Hp " + player.Hp + "/" + player.MaxHp);
            Console.WriteLine("MP " + player.Mp + "/" + player.MaxMp);

            Console.WriteLine();
            Console.Write("대상을 선택해주세요.\n>>");


            List<Monster> hitMobs = new List<Monster>();

            int monsterNum;

            while (true)
            {
                monsterNum = Program.CheckValidInput(1, monsters.Length);

                if (monsters[monsterNum - 1].IsDead)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                else
                {
                    hitMobs.Add(monsters[monsterNum - 1]);

                    break;
                }
            }

            DisplayBattle.WriteBattle();

            DisplayBattle.SkillAttack(player, hitMobs, (int)(player.Atk * 3f));
        }

        private void Wizard_Meteor(Character player, Monster[] monsters)
        {
            List<Monster> hitMobs = new List<Monster>();


            // 랜덤 3마리 뽑기
            List<int> randomNums = new List<int>();

            int num = (monsters.Length >= 3) ? 3 : monsters.Length;

            for (int i = 0; i < num; i++)
            {
                int n;

                do
                {
                    n = new Random().Next(0, monsters.Length);
                }
                while (randomNums.Contains(n));

                randomNums.Add(n);
                hitMobs.Add(monsters[n]);
            }


            DisplayBattle.WriteBattle();

            DisplayBattle.SkillAttack(player, hitMobs, (int)(player.Atk * 1.0f));
        }

        #endregion

    }
}
