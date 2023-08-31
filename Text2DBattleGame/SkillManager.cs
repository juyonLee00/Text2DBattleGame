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

        private void Warrior_AlphaStrike(Character player, Monster[] monsters, int useMp, List<IItem> getitem)
        {
            player.UsingMp(useMp);

            List<Monster> hitMobs = new List<Monster>();

            int monsterNum;
            int originalLeft = Console.CursorLeft; 
            int originalTop = Console.CursorTop;

            while (true)
            {
                monsterNum = Program.CheckValidInput(1, monsters.Length);

                if (monsters[monsterNum - 1].IsDead)
                {
                    Console.Write("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(originalLeft, originalTop);
                    Console.Write("                     ");
                    Console.SetCursorPosition(originalLeft, originalTop);
                }
                else
                {
                    hitMobs.Add(monsters[monsterNum - 1]);

                    break;
                }
            }
            DisplayBattle.SkillAttack(player, hitMobs, (int)(player.Atk * 2f), getitem);
        }

        private void Warrior_DoubleStrike(Character player, Monster[] monsters, int useMp, List<IItem> getitem)
        {
            /*
            player.UsingMp(useMp);

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
            DisplayBattle.SkillAttack(player, hitMobs, (int)(player.Atk * 1.5f), getitem);
            */

            player.UsingMp(useMp);

            List<Monster> hitMobs = GetAliveMonsters(monsters);

            int n;

            while (hitMobs.Count > 2)
            {
                n = new Random().Next(0, hitMobs.Count);

                hitMobs.Remove(monsters[n]);
            }

            DisplayBattle.SkillAttack(player, hitMobs, (int)(player.Atk * 1.5f), getitem);
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

        private void Wizard_FireBall(Character player, Monster[] monsters, int useMp, List<IItem> getitem)
        {
            player.UsingMp(useMp);

            List<Monster> hitMobs = new List<Monster>();

            int monsterNum;
            int originalLeft = Console.CursorLeft;
            int originalTop = Console.CursorTop;

            while (true)
            {
                monsterNum = Program.CheckValidInput(1, monsters.Length);

                if (monsters[monsterNum - 1].IsDead)
                {
                    Console.Write("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(originalLeft, originalTop);
                    Console.Write("                     ");
                    Console.SetCursorPosition(originalLeft, originalTop);
                }
                else
                {
                    hitMobs.Add(monsters[monsterNum - 1]);

                    break;
                }
            }
            DisplayBattle.SkillAttack(player, hitMobs, (int)(player.Atk * 3f), getitem);
        }

        private void Wizard_Meteor(Character player, Monster[] monsters, int useMp, List<IItem> getitem)
        {
            /*
            player.UsingMp(useMp);

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

            DisplayBattle.SkillAttack(player, hitMobs, (int)(player.Atk * 1.0f), getitem);
            */

            player.UsingMp(useMp);

            List<Monster> hitMobs = GetAliveMonsters(monsters);

            int n;

            while (hitMobs.Count > 3)
            {
                n = new Random().Next(0, hitMobs.Count);

                hitMobs.Remove(monsters[n]);
            }

            DisplayBattle.SkillAttack(player, hitMobs, (int)(player.Atk * 1.0f), getitem);
        }

        #endregion


        private List<Monster> GetAliveMonsters(Monster[] monsters)
        {
            List<Monster> aliveMonsters = new List<Monster>();

            for(int i = 0; i < monsters.Length; i++)
            {
                if (!monsters[i].IsDead)
                    aliveMonsters.Add(monsters[i]);
            }

            return aliveMonsters;
        }
    }
}
