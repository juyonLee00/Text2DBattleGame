using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    class DisplayBattle
    {
        public static void Display(Character player)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();

            Monster[] battleMonsters = CreateCharacter.CreateRandomMonster();

            foreach (Monster monster in battleMonsters)
            {
                Console.WriteLine("Lv." + monster.Level + " " + monster.Name + "  HP " + monster.Hp);
            }

            Console.WriteLine("\n\n[내정보]");
            Console.WriteLine("Lv." + player.Level + " " + player.Name + " (" + player.Job + ")");
            Console.WriteLine("Hp " + player.Hp + "/" + player.MaxHp);

            Console.WriteLine("1. 공격");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.\n>>");

            Program.CheckValidInput(1, 1);

            int result = Battle(battleMonsters, player); //0이면 승리, 1이면 패배
            if (result == 3) Program.scene = Scene.GameIntro;

        }

        public static int Battle(Monster[] battleMonsters, Character player)
        {
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Battle!!\n");
                Console.ResetColor();

                int i = 1;
                foreach (Monster monster in battleMonsters)
                {
                    if (monster.IsDead)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(i++ + " Lv." + monster.Level + " " + monster.Name + "  Dead ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(i++ + " Lv." + monster.Level + " " + monster.Name + "  HP " + monster.Hp);
                    }
                }

                Console.WriteLine("\n\n[내정보]");
                Console.WriteLine("Lv." + player.Level + " " + player.Name + " (" + player.Job + ")");
                Console.WriteLine("Hp " + player.Hp + " " + player.Name + " (" + player.Job + ")\n");

                Console.WriteLine("0. 취소");
                Console.WriteLine();
                Console.Write("대상을 선택해주세요.\n>>");

                int monsterNum;
                while (true)
                {
                    monsterNum = Program.CheckValidInput(0, i - 1);
                    if (monsterNum == 0) return 2;
                    if (battleMonsters[monsterNum - 1].IsDead)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                    else
                    {
                        break;
                    }
                }

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Battle!!\n");
                Console.ResetColor();

                Attack(player, battleMonsters[monsterNum - 1]);

                int deadMonsternumber = 0;
                foreach (Monster monster in battleMonsters)
                {
                    if (monster.IsDead)
                    {
                        deadMonsternumber++;
                    }
                }
                if (deadMonsternumber == battleMonsters.Length) return 0;

                Console.WriteLine("\n0. 다음");
                Program.CheckValidInput(0, 0);

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Battle!!\n");
                Console.ResetColor();

                foreach (Monster monster in battleMonsters)
                {
                    if (!monster.IsDead)
                    {
                        Attack(monster, player);
                        if (player.IsDead) return 1;
                    }
                }

                Console.WriteLine("\n0. 다음");
                Program.CheckValidInput(0, 0);
            }
        }

        static int RandomDamage(int Atk)
        {
            int damage;
            int damageMin, damageMax;
            if ((Atk * 9 / 10) % 10 != 0) damageMin = Atk * 9 / 10 + 1;
            else damageMin = Atk * 9 / 10;
            if ((Atk * 11 / 10) % 10 != 0) damageMax = Atk * 11 / 10 + 1;
            else damageMax = Atk * 11 / 10;

            Random random = new Random();
            damage = random.Next(damageMin, damageMax + 1);

            return damage;
        }

        static void Attack(ICharacter attacker, ICharacter defender)
        {
            int damage = RandomDamage(attacker.Atk);

            Console.WriteLine("Lv." + attacker.Level + " " + attacker.Name + " 의 공격!");
            Console.WriteLine("Lv." + defender.Level + " " + defender.Name + "을(를) 맞췄습니다. [데미지 : " + damage + "]");

            Console.WriteLine("Lv." + defender.Level + " " + defender.Name);
            Console.Write("Hp " + defender.Hp + " -> ");
            defender.Hp = defender.Hp - damage;

            if (defender.IsDead)
            {
                Console.WriteLine("Dead");
            }
            else
            {
                Console.WriteLine(defender.Hp);
            }
        }
    }
}
