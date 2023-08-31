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
        public static void Display(Character player)
        {
            int gold = player.Gold;
            int savehp = player.Hp;
            int saveexp = player.Exp;
            int savemp = player.Mp;
            List<IItem> getItem = new List<IItem>();

            Monster[] battleMonsters = CreateCharacter.CreateRandomMonster(player.DungeonLevel);

            Battle(battleMonsters, player, getItem); 

            DungeonResult.Result(player, getItem, savehp, saveexp, gold, battleMonsters.Length,savemp);
            Program.scene = Scene.GameIntro;
        }

        public static void Battle(Monster[] battleMonsters, Character player, List<IItem> getItem)
        {
            ConsoleManager console = new ConsoleManager(player);
            while (true)
            {
                int flag = PlayersTurn(battleMonsters, player, getItem, console);

                //3은 스레기값 2면 다시 실행 0은 승리
                if (flag == 0)  
                {
                    player.DungeonLevel++;
                    break;
                }
                else if (flag == 2) continue;

                //3은 스레기값 1은 패배 
                if (MonstersTurn(battleMonsters, player, getItem, console) == 1) break;   
            }
        }

        public static int PlayersTurn(Monster[] battleMonsters, Character player, List<IItem> getItem, ConsoleManager console) 
        {
            bool endflag = false;

            console.PlayersTurnPausedDisplay(battleMonsters, player, false, "원하시는 행동을 입력해주세요. \n  1. 공격 2. 스킬");

            int input = Program.CheckValidInput(1, 2);
            if (input == 1) 
            {
                console.PlayersTurnPausedDisplay(battleMonsters, player, true, "대상을 선택해주세요  0. 취소");

                int monsterNum;

                int originalLeft = Console.CursorLeft;
                int originalTop = Console.CursorTop;

                while (true)
                {
                    monsterNum = Program.CheckValidInput(0, battleMonsters.Length);

                    if (monsterNum == 0)
                    {
                        endflag = true;
                        break;
                    }
                    if (battleMonsters[monsterNum - 1].IsDead) 
                    {
                        Console.Write("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        Console.SetCursorPosition(originalLeft, originalTop);
                        Console.Write("                     ");
                        Console.SetCursorPosition(originalLeft, originalTop);
                    }
                    else break;
                }
                if (endflag) return 2;

                console.PlayerAttackMotionDisplay(battleMonsters, player);

                Attack(player, battleMonsters[monsterNum - 1], getItem);

                originalLeft = Console.CursorLeft;
                originalTop = Console.CursorTop;
                ConsoleManager.DrawLine(); 
                Console.SetCursorPosition(originalLeft, originalTop);

            }
            //스킬 사용 시
            else
            {
                string str = "";

                for (int i = 0; i < player.Skills.Count; i++)
                {
                    Skill skill = player.Skills[i];

                    str += $"\n  {i + 1}. {skill.Name} - {skill.ShowExplanation(player)} (MP {skill.Mp})";
                }

                str = "사용할 스킬을 입력해주세요 0. 취소" + str;
                console.PlayersTurnPausedDisplay(battleMonsters, player, false, str);

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
                            if (skillNum == 1) console.PlayersTurnPausedDisplay(battleMonsters, player, true, "대상을 선택해주세요  0. 취소");
                            player.Skills[skillNum - 1].UsingSkill(player, battleMonsters, player.Skills[skillNum - 1].Mp, getItem);
                            int originalLeft = Console.CursorLeft;
                            int originalTop = Console.CursorTop;
                            console.PlayerSkillAttackMotionDisplay(battleMonsters, player);
                            ConsoleManager.DrawLine();
                            Console.SetCursorPosition(originalLeft, originalTop);
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

        public static int MonstersTurn(Monster[] battleMonsters, Character player, List<IItem> getItem, ConsoleManager console)
        {
            console.PlayerDefenseMotionDisplay(battleMonsters, player);
            foreach (Monster monster in battleMonsters)
            {
                if (!monster.IsDead)
                {
                    Attack(monster, player, getItem);

                    if (player.IsDead) return 1;
                }
            }
            int originalLeft = Console.CursorLeft;
            int originalTop = Console.CursorTop;
            ConsoleManager.DrawLine(); 
            Console.SetCursorPosition(originalLeft, originalTop);

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
            Console.WriteLine("  Lv." + attacker.Level + " " + attacker.Name + " 의 공격!");

            if (new Random().Next(1, 101) <= defender.Avoidability)
            {
                Console.WriteLine("  Lv." + defender.Level + " " + defender.Name + "을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                return;
            }

                int damage = RandomDamage(attacker.Atk);

            if (new Random().Next(1, 101) <= attacker.CriticalRate)
            {
                damage = (int)(damage * attacker.CriticalAtk);
            
                Console.WriteLine("  Lv." + defender.Level + " " + defender.Name + "을(를) 맞췄습니다. [데미지 : " + damage + "] - 치명타 공격!!");
            }
            else
            {
                Console.WriteLine("  Lv." + defender.Level + " " + defender.Name + "을(를) 맞췄습니다. [데미지 : " + damage + "]");
            }

            Console.Write("  Lv." + defender.Level + " " + defender.Name + "  :  Hp " + defender.Hp + " -> ");
            defender.TakeDamage(damage);

            if (defender.IsDead)
            {
                Console.WriteLine("Dead");
                getItem.Add(Monster.Drop(changeDropTabel(defender.Name)));
                attacker.Exp += defender.Level;
                attacker.Gold += defender.Gold;
            }
            else
            {
                Console.WriteLine(defender.Hp);
            }
        }

        public static void SkillAttack(Character attacker, List<Monster> defenders, int damage, List<IItem> getItem)
        {
            //반복문 안에서 계속 같은수 뱉어서 빼냄
            Random rand = new Random();

            for (int i = 0; i < defenders.Count; i++)
            {
                bool isCritical = false;
                int criticalDamage = (int)(damage * attacker.CriticalAtk);

                Console.WriteLine("  Lv." + attacker.Level + " " + attacker.Name + " 의 공격!");

                if (rand.Next(1, 101) <= attacker.CriticalRate)
                {
                    isCritical = true;

                    Console.WriteLine("  Lv." + defenders[i].Level + " " + defenders[i].Name + "을(를) 맞췄습니다. [데미지 : " + criticalDamage + "] - 치명타 공격!!");
                }
                else
                {
                    isCritical = false;
                    Console.WriteLine("  Lv." + defenders[i].Level + " " + defenders[i].Name + "을(를) 맞췄습니다. [데미지 : " + damage + "]");
                } 

                Console.Write("  Lv." + defenders[i].Level + " " + defenders[i].Name + "  :  Hp " + defenders[i].Hp + " -> ");

                defenders[i].TakeDamage(isCritical ? criticalDamage : damage);

                if (defenders[i].IsDead)
                {
                    Console.WriteLine("Dead");
                    getItem.Add(Monster.Drop(changeDropTabel(defenders[i].Name)));
                    attacker.Exp += defenders[i].Level;
                    attacker.Gold += defenders[i].Gold;

                }
                else
                {
                    Console.WriteLine(defenders[i].Hp);
                }

            }
        }
        public static List<IItem> changeDropTabel(string name) 
        {
            switch (name)
            {
                case "미니언":            
                case "공허충":
                case "대포미니언":
                    return Program.normalTable;

                case "타락한 거미":
                case "그림자속무언가":
                case "수중뱀":
                    return Program.rareTable;

                case "수중서펀트":
                case "드래곤":
                case "타락한 거미여왕":
                    return Program.UniqueTable;

                default:
                    return Program.errortable;
            }
        }
    }
}
