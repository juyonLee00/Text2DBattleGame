using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    class DisplayBattle
    {
      static List<IItem> itemTable1 = new List<IItem>() { new AttackItem("테스트 아이템", 0, 0, 0) };
        public static void Display(Character player)
        {
            int gold = player.Gold;
            int savehp = player.Hp;
            int saveexp = player.Exp;
            List<IItem> getItem = new List<IItem>();

            Console.Clear();
            //테스트
            //if (player.Inventory != null) 
            //{ 
            //    foreach (IItem item in player.Inventory) 
            //    {
            //     Console.WriteLine(item.Name);
            //    }
            //}
            //테스트

            Monster[] battleMonsters = CreateCharacter.CreateRandomMonster(player.DungeonLevel);

            Battle(battleMonsters, player, getItem); 

            DungeonResult.Result(player, getItem, savehp, saveexp, gold, battleMonsters.Length);
            Program.scene = Scene.GameIntro;
        }

        public static void Battle(Monster[] battleMonsters, Character player, List<IItem> getItem)
        {
            while (true)
            {
                int flag = PlayersTurn(battleMonsters, player, getItem);
                if (flag == 0) //3은 스레기값 2면 다시 실행 0은 승리 
                {
                    player.DungeonLevel++;
                    break;
                }
                else if (flag == 2) continue;
                if (MonstersTurn(battleMonsters, player, getItem) == 1) break;  //3은 스레기값 1은 패배  
            }
        }

        public static int PlayersTurn(Monster[] battleMonsters, Character player, List<IItem> getItem) 
        {
            bool endflag = false;

            WriteBattle();
            DisplayPresentStatus(battleMonsters, player, false);

            Console.WriteLine("1. 공격\n2. 스킬\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>>");

            int input = Program.CheckValidInput(1, 2);
            if (input == 1) 
            {
                WriteBattle();
                DisplayPresentStatus(battleMonsters, player, true);

                Console.WriteLine("0. 취소\n");
                Console.Write("대상을 선택해주세요.\n>>");

                int monsterNum;
                while (true)
                {
                    monsterNum = Program.CheckValidInput(0, battleMonsters.Length);

                    if (monsterNum == 0)
                    {
                        endflag = true;
                        break;
                    }
                    if (battleMonsters[monsterNum - 1].IsDead) Console.WriteLine("잘못된 입력입니다.");
                    else break;
                }
                if (endflag) return 2;

                WriteBattle();

                Attack(player, battleMonsters[monsterNum - 1], getItem);

            }
            else //스킬 사용 시
            {
                WriteBattle();

                for(int i = 0; i < battleMonsters.Length; i++)
                {
                    Monster monster = battleMonsters[i];

                    if (monster.IsDead)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Lv." + monster.Level + " " + monster.Name + "  Dead ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Lv." + monster.Level + " " + monster.Name + "  HP " + monster.Hp);
                    }
                }

                Console.WriteLine("\n\n[내정보]");
                Console.WriteLine("Lv." + player.Level + " " + player.Name + " (" + player.Job + ")");
                Console.WriteLine("Hp " + player.Hp + "/" + player.MaxHp);
                Console.WriteLine("MP " + player.Mp + "/" + player.MaxMp);

                // 스킬 출력
                for (int i = 0; i < player.Skills.Count; i++)
                {
                    Skill skill = player.Skills[i];

                    Console.WriteLine($"{i + 1}. {skill.Name} - MP {skill.Mp}");
                    Console.WriteLine($"    {skill.ShowExplanation(player)}");
                }

                Console.WriteLine();
                Console.WriteLine("0. 취소");

                while (true)
                {
                    int skillNum = Program.CheckValidInput(0, player.Skills.Count);

                    if (skillNum == 0)
                    {
                        return 2;
                    }
                    else
                    {
                        if (player.Mp >= player.Skills[skillNum - 1].Mp)
                        {
                            player.Skills[skillNum - 1].UsingSkill(player, battleMonsters, player.Skills[skillNum - 1].Mp);
                            break;
                        }
                        else
                            Console.WriteLine("플레이어의 MP가 부족합니다.");
                    }
                }
            }

            int deadMonsternumber = 0;
            foreach (Monster monster in battleMonsters)
            {
                if (monster.IsDead) deadMonsternumber++;
            }
            if (deadMonsternumber == battleMonsters.Length) return 0;

            Console.WriteLine("\n0. 다음");
            Program.CheckValidInput(0, 0);

            return 3;
        }

        public static int MonstersTurn(Monster[] battleMonsters, Character player, List<IItem> getItem)
        {
            WriteBattle();

            foreach (Monster monster in battleMonsters)
            {
                if (!monster.IsDead)
                {
                    Attack(monster, player, getItem);

                    if (player.IsDead) return 1;
                }
            }

            Console.WriteLine("\n0. 다음");
            Program.CheckValidInput(0, 0);

            return 3;
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

        public static void Attack(ICharacter attacker, ICharacter defender, List<IItem> getItem)
        {
            Console.WriteLine("Lv." + attacker.Level + " " + attacker.Name + " 의 공격!");

            if (attacker.GetType() == typeof(Character) && new Random().Next(1, 11) == 1) 
            {
                Console.WriteLine("Lv." + defender.Level + " " + defender.Name + "을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                return;
            }

            int damage = RandomDamage(attacker.Atk);

            Console.WriteLine("Lv." + defender.Level + " " + defender.Name + "을(를) 맞췄습니다. [데미지 : " + damage + "]");

            Console.WriteLine("Lv." + defender.Level + " " + defender.Name);
            Console.Write("Hp " + defender.Hp + " -> ");
            defender.TakeDamage(damage);

            if (defender.IsDead)
            {
                Console.WriteLine("Dead");
                getItem.Add(Monster.Drop(itemTable1));
                attacker.Exp += defender.Level;//경험치추가
                attacker.Gold += defender.Gold;//골드추가
            }
            else
            {
                Console.WriteLine(defender.Hp);
            }
        }

        public static void SkillAttack(Character attacker, List<Monster> defenders, int damage)
        {
            for(int i = 0; i < defenders.Count; i++)
            {
                Console.WriteLine("Lv." + attacker.Level + " " + attacker.Name + " 의 공격!");
                Console.WriteLine("Lv." + defenders[i].Level + " " + defenders[i].Name + "을(를) 맞췄습니다. [데미지 : " + damage + "]");

                Console.WriteLine("Lv." + defenders[i].Level + " " + defenders[i].Name);
                Console.Write("Hp " + defenders[i].Hp + " -> ");

                defenders[i].TakeDamage(damage);

                if (defenders[i].IsDead)
                {
                    Console.WriteLine("Dead");
                    Monster.Drop(itemTable1);
                    attacker.Exp += defenders[i].Level;//경험치추가
                    attacker.Gold += defenders[i].Gold;//골드추가

                }
                else
                {
                    Console.WriteLine(defenders[i].Hp);
                }

                Console.WriteLine();
            }
        }

        public static void WriteBattle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();
        }
        static void DisplayPresentStatus(Monster[] battleMonsters, Character player, bool attack) 
        {
            int i = 1;
            foreach (Monster monster in battleMonsters)
            {
                if (monster.IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    if (attack) Console.Write(i++);
                    Console.WriteLine(" Lv." + monster.Level + " " + monster.Name + "  Dead ");
                    Console.ResetColor();
                }
                else
                {
                    if (attack) Console.Write(i++);
                    Console.WriteLine(" Lv." + monster.Level + " " + monster.Name + "  HP " + monster.Hp);
                }
            }

            Console.WriteLine("\n\n[내정보]");
            Console.WriteLine("Lv." + player.Level + " " + player.Name + " (" + player.Job + ")");
            Console.WriteLine("Hp " + player.Hp + "/" + player.MaxHp);
            Console.WriteLine("MP " + player.Mp + "/" + player.MaxMp);
        }
    }
}
