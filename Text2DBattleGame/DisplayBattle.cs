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
            int gold = player.Gold;
            int savehp = player.Hp;
            int saveexp = player.Exp;
            List<Item> getlist= new List<Item> { };
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
            Console.WriteLine("0. 돌아가기");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.\n>>");

            int input = Program.CheckValidInput(0, 1);
            int result;
            
            if (input == 1) result = Battle(battleMonsters, player); //0이면 승리, 1이면 패배

            Program.scene = Scene.GameIntro;

            int result = Battle(battleMonsters, player); //0이면 승리, 1이면 패배
            DungeonResult.Result(player, getlist, savehp, saveexp, gold);
            //
            if (result == 0) Program.scene = Scene.GameIntro;
            else Program.scene = Scene.GameIntro;

        }

        public static int Battle(Monster[] battleMonsters, Character player)
        {
            int result = 0;
            while (true)
            {
                int monsterNum;
                Console.Clear();
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
                // 여기서 취소시 몹 리스트로 돌아가는게 아니라 몹 리스트가 새로워짐
                Console.WriteLine();
                Console.Write("대상을 선택해주세요.\n>>");

                while (true)
                {
                    monsterNum = Program.CheckValidInput(0, i - 1);
                    if (monsterNum == 0) 
                    {
                        result = 2;
                        break;
                    }
                    if (battleMonsters[monsterNum - 1].IsDead)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                    else
                    {
                        break;
                    }
                }
                if (result == 2) break;

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
                if (deadMonsternumber == battleMonsters.Length)                      return 0;


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
                        if (player.IsDead) 
                        {
                            result = 1;
                            break;
                        } 
                    }
                }

                Console.WriteLine("\n0. 다음");
                Program.CheckValidInput(0, 0);
            }
            return result;
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
                attacker.Exp += defender.Level;//경험치추가
                attacker.Gold += defender.Gold;//골드추가
            }
            else
            {
                Console.WriteLine(defender.Hp);
            }
        }
    }
}
