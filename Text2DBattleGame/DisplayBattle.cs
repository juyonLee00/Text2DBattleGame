using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    class DisplayBattle
    {
      static List<Item> itemTable1 = new List<Item>() { new Item("테스트 아이템", 0, 0, 0) };
        public static void Display(Character player)
        {
            int gold = player.Gold;
            int savehp = player.Hp;
            int saveexp = player.Exp;
            List<Item> getItem = new List<Item>();

            Console.Clear();
            //테스트
            //if (player.Inventory != null) 
            //{ 
            //    foreach (Item item in player.Inventory) 
            //    {
            //     Console.WriteLine(item.Name);
            //    }
            //}
            //테스트

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();

            Monster[] battleMonsters = CreateCharacter.CreateRandomMonster(player.DungeonLevel);

            Battle(battleMonsters, player, getItem); 

            DungeonResult.Result(player, getItem, savehp, saveexp, gold, battleMonsters.Length);
            Program.scene = Scene.GameIntro;
        }

        public static void Battle(Monster[] battleMonsters, Character player, List<Item> getItem)
        {
            while (true)
            {
                if (PlayersTurn(battleMonsters, player, getItem) == 0) //3은 스레기값 0은 승리 1은 패배
                {
                    player.DungeonLevel++;
                    break;
                }
                if (MonstersTurn(battleMonsters, player, getItem) == 1) break;
            }
        }

        public static int PlayersTurn(Monster[] battleMonsters, Character player, List<Item> getItem) 
        {
            bool endflag = false;
            
            Console.Clear();

            foreach (Monster monster in battleMonsters)
            {
                if (monster.IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(" Lv." + monster.Level + " " + monster.Name + "  Dead ");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(" Lv." + monster.Level + " " + monster.Name + "  HP " + monster.Hp);
                }
            }

            Console.WriteLine("\n\n[내정보]");
            Console.WriteLine("Lv." + player.Level + " " + player.Name + " (" + player.Job + ")");
            Console.WriteLine("Hp " + player.Hp + "/" + player.MaxHp);
            Console.WriteLine("MP " + player.Mp + "/" + player.MaxMp);

            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.\n>>");

            int input = Program.CheckValidInput(1, 2);
            if (input == 1) 
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
                Console.WriteLine("Mp " + player.Mp + " " + player.Name + " (" + player.Job + ")\n");

                Console.WriteLine("0. 취소");
                Console.WriteLine();
                Console.Write("대상을 선택해주세요.\n>>");

                int monsterNum;
                while (true)
                {
                    monsterNum = Program.CheckValidInput(0, i - 1);

                    if (monsterNum == 0)
                    {
                        Console.Clear();
                        PlayersTurn(battleMonsters, player, getItem);
                        endflag = true;
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
                if (endflag == true) 
                {
                    return 3;
                }
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Battle!!\n");
                Console.ResetColor();

                Attack(player, battleMonsters[monsterNum - 1], getItem);

            }
            else //스킬 사용 시
            {
                ////////////////////////////////////////////////////////////////////////////채워넣을 부분
            }

            int deadMonsternumber = 0;
            foreach (Monster monster in battleMonsters)
            {
                if (monster.IsDead)
                {
                    deadMonsternumber++;
                }
            }

            if (deadMonsternumber == battleMonsters.Length)

            {
                return 0;
            }

            Console.WriteLine("\n0. 다음");
            Program.CheckValidInput(0, 0);

            return 3;
        }

        public static int MonstersTurn(Monster[] battleMonsters, Character player, List<Item> getItem)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();

            foreach (Monster monster in battleMonsters)
            {
                if (!monster.IsDead)
                {
                    Attack(monster, player, getItem);

                    if (player.IsDead)
                    {
                        return 1;
                    }

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

        static void Attack(ICharacter attacker, ICharacter defender, List<Item> getItem)
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
                getItem.Add(Monster.Drop(itemTable1));
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
