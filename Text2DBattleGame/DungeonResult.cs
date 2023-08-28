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
        //DungeonResult(player, getitemlist, savehp, saveexp, gold);
        public static void Result(Character player, List<Item> item, int savehp, int saveexp, int gold)
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
                Console.WriteLine("던전에서 몬스터 n 마리를 잡았습니다.");
                Console.WriteLine("[캐릭터 정보]");
                LevelResult(player, savehp, saveexp);
                GetItem(player,item, gold); //

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
                Console.WriteLine($"{player.Gold} Gold");
                player.Gold /= 2;
            }
            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");
            Console.ReadLine();

        }
        public static void LevelResult(Character player, int savehp, int saveexp)
        {
            float upAtk = 1f; // 발제 에서는 0.5 였으나 기본 설정이 int 이기에 우선 1로 설정함
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
                Console.WriteLine($"Lv.{player.Level - 1} {player.Name} -> Lv.{player.Level} {player.Name} ");
                Console.WriteLine($"HP.{savehp} -> {player.Hp} ");
                if (player.Level <= 4)
                    Console.WriteLine($"Exp.{saveexp} -> {player.Exp} ");//@@@@@@@exp 생각하고 수정 해야할듯
            }
            else
            {
                Console.WriteLine($"Lv.{player.Level} {player.Name} ");
                Console.WriteLine($"HP.{savehp} -> {player.Hp} ");
                if (player.Level <= 4)
                    Console.WriteLine($"Exp.{saveexp} -> {player.Exp} ");
            }

        }

        public static void GetItem(Character player,List<Item> list, int gold)
        {
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{player.Gold - gold} Gold");
            for (int i = 0; i < list.Count; ++i)
            {
                Console.WriteLine($"{list[i].Name} - 1");
            }

            //foreach (Item item in list)배열
            //{
            //    Console.WriteLine($"{item.Name} - 1");
            //}


        }
    }
}