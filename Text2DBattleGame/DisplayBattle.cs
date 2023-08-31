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
                if (flag == 0) //3은 스레기값 2면 다시 실행 0은 승리 
                {
                    player.DungeonLevel++;
                    break;
                }
                else if (flag == 2) continue;
                if (MonstersTurn(battleMonsters, player, getItem, console) == 1) break;  //3은 스레기값 1은 패배  
            }
        }

        public static int PlayersTurn(Monster[] battleMonsters, Character player, List<IItem> getItem, ConsoleManager console) 
        {
            bool endflag = false;
            int originalLeft = Console.CursorLeft; 
            int originalTop = Console.CursorTop; 

            console.PlayersTurnPausedDisplay(battleMonsters, player, false, "원하시는 행동을 입력해주세요. \n  1. 공격 2. 스킬");

            int input = Program.CheckValidInput(1, 2);
            if (input == 1) 
            {
                console.PlayersTurnPausedDisplay(battleMonsters, player, true, "대상을 선택해주세요  0. 취소");

                int monsterNum;

                originalLeft = Console.CursorLeft;
                originalTop = Console.CursorTop;

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
            }
            else //스킬 사용 시
            {
                string str = "";

                // 스킬 출력
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
                            if (skillNum == 1) console.PlayersTurnPausedDisplay(battleMonsters, player, true, "대상을 선택해주세요 ");
                            originalLeft = Console.CursorLeft;
                            originalTop = Console.CursorTop;
                            player.Skills[skillNum - 1].UsingSkill(player, battleMonsters, player.Skills[skillNum - 1].Mp, getItem);
                            console.PlayerSkillAttackMotionDisplay(battleMonsters, player);
                            ConsoleManager.DrawLine(battleMonsters.Length);
                            Console.SetCursorPosition(originalLeft, originalTop);
                            break;
                        }
                        else
                        {
                            originalLeft = Console.CursorLeft;
                            originalTop = Console.CursorTop;
                            Console.Write("플레이어의 MP가 부족합니다.");
                            Thread.Sleep(500);
                            Console.SetCursorPosition(originalLeft, originalTop);
                            Console.Write("                             ");
                            Console.SetCursorPosition(originalLeft, originalTop);
                        }
                    }
                }
            }

            int deadMonsternumber = 0;
            foreach (Monster monster in battleMonsters)
            {
                if (monster.IsDead) deadMonsternumber++;
            }
            if (deadMonsternumber == battleMonsters.Length) return 0;

            Console.Write("\n  0. 다음   "); 
            originalLeft = Console.CursorLeft;
            originalTop = Console.CursorTop;
            ConsoleManager.DrawLine(battleMonsters.Length); 
            Console.SetCursorPosition(originalLeft, originalTop);

            Program.CheckValidInput(0, 0);

            return 3;
        }

        public static int MonstersTurn(Monster[] battleMonsters, Character player, List<IItem> getItem, ConsoleManager console)
        {
            int originalLeft; 
            int originalTop; 

            console.PlayerDefenseMotionDisplay(battleMonsters, player);
            foreach (Monster monster in battleMonsters)
            {
                if (!monster.IsDead)
                {
                    originalLeft = Console.CursorLeft; 
                    originalTop = Console.CursorTop;
                    Attack(monster, player, getItem);
                    ConsoleManager.DrawLine(battleMonsters.Length);
                    Thread.Sleep(1500);
                    Console.SetCursorPosition(originalLeft, originalTop);
                    for (int i = 0; i < 3; i++) Console.WriteLine("                                                               ");
                    Console.SetCursorPosition(originalLeft, originalTop);

                    if (player.IsDead) return 1;
                }
            }
            originalLeft = Console.CursorLeft;
            originalTop = Console.CursorTop;
            Console.SetCursorPosition(originalLeft, originalTop);

            Console.Write("\n  0. 다음   "); 
            originalLeft = Console.CursorLeft;
            originalTop = Console.CursorTop;
            ConsoleManager.DrawLine(battleMonsters.Length); 
            Console.SetCursorPosition(originalLeft, originalTop);

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

            // 플레이어와 몬스터는 각각의 회피율을 가지게 설정했기 때문에 변경했습니다!
            /*
            if (attacker.GetType() == typeof(Character) && new Random().Next(1, 11) == 1) 
            {
                Console.WriteLine("Lv." + defender.Level + " " + defender.Name + "을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                return;
            }
            */
            if (new Random().Next(1, 101) <= defender.Avoidability)
            {
                Console.WriteLine("  Lv." + defender.Level + " " + defender.Name + "을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                return;
            }

                int damage = RandomDamage(attacker.Atk);

            if (new Random().Next(1, 101) <= attacker.CriticalRate)
            {
                // 임시 - damage는 float형으로 바뀌어야 할 것 같습니다.
                damage = (int)(damage * attacker.CriticalAtk);

                // 요구사항
                // damage *= 1.6f;

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
                Console.Write("Dead");
                //startTable = changeDropTabel(defender.Name);//디펜더=죽은 몬스터 = 죽은몬스터의 이름을받아 몬스터등급별 테이블로 이동
                getItem.Add(Monster.Drop(changeDropTabel(defender.Name)));
                attacker.Exp += defender.Level;//경험치추가
                attacker.Gold += defender.Gold;//골드추가
            }
            else
            {
                Console.Write(defender.Hp);
            }
        }

        public static void SkillAttack(Character attacker, List<Monster> defenders, int damage, List<IItem> getItem)
        {
            //반복문 안에서 계속 같은수뱉어서 빼냄
            Random rand = new Random();

            for (int i = 0; i < defenders.Count; i++)
            {
                bool isCritical = false;
                int criticalDamage = (int)(damage * attacker.CriticalAtk);

                int originalLeft = Console.CursorLeft;
                int originalTop = Console.CursorTop;

                Console.WriteLine("Lv." + attacker.Level + " " + attacker.Name + " 의 공격!");

                // 일정확률로 치명타공격
                if (rand.Next(1, 101) <= attacker.CriticalRate)
                {
                    isCritical = true;

                    Console.WriteLine("| Lv." + defenders[i].Level + " " + defenders[i].Name + "을(를) 맞췄습니다. [데미지 : " + criticalDamage + "] - 치명타 공격!!");
                }
                else
                {
                    isCritical = false;
                    Console.WriteLine("| Lv." + defenders[i].Level + " " + defenders[i].Name + "을(를) 맞췄습니다. [데미지 : " + damage + "]");
                } 

                Console.Write("| Lv." + defenders[i].Level + " " + defenders[i].Name + "  :  Hp " + defenders[i].Hp + " -> ");

                defenders[i].TakeDamage(isCritical ? criticalDamage : damage);

                if (defenders[i].IsDead)
                {
                    Console.WriteLine("Dead");
                    //Monster.Drop(itemTable1); 드랍은 아이템을 생성하는 함수
                    getItem.Add(Monster.Drop(changeDropTabel(defenders[i].Name)));//죽은몬스터의 이름을받아 몬스터등급별 테이블로 이동
                    attacker.Exp += defenders[i].Level;//경험치추가
                    attacker.Gold += defenders[i].Gold;//골드추가

                }
                else
                {
                    Console.WriteLine(defenders[i].Hp);
                }

                Thread.Sleep(1000);

                Console.SetCursorPosition(originalLeft, originalTop);
                for (int j = 0; j < 3; j++) Console.WriteLine("|                                                                    ");
                Console.SetCursorPosition(originalLeft, originalTop);

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
