using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    internal class DungeonResult
    {
        public static void Result(Character player, List<IItem> item, int savehp, int saveexp, int gold, int deadCount, int savemp)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Battle!! - Result");
            Console.ResetColor();
            Console.WriteLine("");
            if (!player.IsDead)
            {

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Victory");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine($"던전에서 몬스터 {deadCount} 마리를 잡았습니다.");
                Console.WriteLine("[캐릭터 정보]");
                LevelResult(player, savehp, saveexp,savemp);
                GetItem(player, item, gold); 

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Lose");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ");
                Console.WriteLine($"HP.{savehp} -> 0 ");
                Console.WriteLine("눈앞이 깜깜해졌다.");
                Console.WriteLine("소지금의 절반을 잃었다.");
                player.Hp = player.MaxHp / 4;
                player.Gold /= 2;
                Console.WriteLine($"{player.Gold} Gold");
            }
            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");
            Console.ReadLine();
            Program.scene = Scene.GameIntro;


        }

        public static void LevelResult(Character player, int savehp, int saveexp, int savemp)

        {
            float upAtk = 1f; 
            float upDef = 2f;
            bool levelup = false;
            if (player.Exp >= 100)
            {
                player.Level = 5;
                levelup = true;
            }
            else if (player.Exp >= 65)
            {
                player.Level = 4;
                player.Atk += (int)upAtk;
                player.Def += (int)upDef;
                levelup = true;
            }
            if (player.Exp >= 35)
            {
                player.Level = 3;
                player.Atk += (int)upAtk;
                player.Def += (int)upDef;
                levelup = true;
            }
            if (player.Exp >= 10)
            {
                player.Level = 2;
                player.Atk += (int)upAtk;
                player.Def += (int)upDef;
                levelup = true;
            }
            else
            {
                player.Level = 1;
            }
            if (levelup)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("L");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("e");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("v");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("e");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("l");
                Console.ResetColor();
                Console.Write("");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("U");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("p");
                Console.ResetColor();
                Console.WriteLine($"Lv.{player.Level - 1} {player.Name} -> Lv.{player.Level} {player.Name} ");
                Console.WriteLine($"HP.{savehp} -> {player.Hp} ");
                Console.WriteLine($"HP.{savemp} -> {player.Mp} ");
                Console.WriteLine($"Atk.{player.Atk-1f} -> {player.Atk} ");
                Console.WriteLine($"Def.{player.Def-2f} -> {player.Def} ");
                if (player.Level <= 4)
                    Console.WriteLine($"Exp.{saveexp} -> {player.Exp} ");

            }
            else
            {
                Console.WriteLine($"Lv.{player.Level} {player.Name} ");
                Console.WriteLine($"HP.{savehp} -> {player.Hp} ");
                Console.WriteLine($"MP.{savemp} -> {player.Mp} ");
                if (player.Level <= 4)
                    Console.WriteLine($"Exp.{saveexp} -> {player.Exp} ");

            }

        }

        public static void GetItem(Character player, List<IItem> list, int gold)
        {
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{player.Gold - gold} Gold");
            for (int i = 0; i < list.Count; ++i)
            {
                Console.WriteLine($"{list[i].Name} - 1");
                if (list[i].CanUse)
                {
                    string number = list[i].Name;

                    int num = player.Inventory.FindIndex(Item => Item.Name.Equals(number));
                    if (0 <= num)
                    {
                        if (player.Inventory[num] is PotionItem)
                        {
                            //만약 a가 형변환이 가능하다면 a에 오른쪽 값이 들어옴
                            PotionItem a = player.Inventory[num] as PotionItem;
                            a.Count++;
                        }


                    }
                    //그 아이템이 처음 추가 되는경우
                    else
                    {
                        player.Inventory.Add(list[i]);
                    }
                }
                else
                {
                    player.Inventory.Add(list[i]);
                }
            }
        }

    
    }
}