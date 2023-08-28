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
        public void Result(Character player, List<Item> item, int alldamege, int gold)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Battle!! - Result");
            Console.ResetColor();
            Console.WriteLine("");
            if (player.IsDead)
            {

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Victory");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine("던전에서 몬스터 n 마리를 잡았습니다.");
                Console.WriteLine("[캐릭터 정보]");
                LevelResult(player, alldamege);
                GetItem(item, gold); //

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Lose");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ");
                Console.WriteLine($"HP.{player.Hp + alldamege} -> {player.Hp} ");
            }
            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");

        }
        public void LevelResult(Character player, int alldamege)
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
                Console.WriteLine($"HP.{player.Hp + alldamege} -> {player.Hp} ");
                if (player.Level <= 4)
                    Console.WriteLine($"Exp.{player.Exp - alldamege} -> {player.Exp} ");
            }
            else
            {
                Console.WriteLine($"Lv.{player.Level} {player.Name} ");
                Console.WriteLine($"HP.{player.Hp + alldamege} -> {player.Hp} ");
                if (player.Level <= 4)
                    Console.WriteLine($"Exp.{player.Exp - alldamege} -> {player.Exp} ");
            }

        }

        public void GetItem(List<Item> list, int gold)
        {
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{gold} Gold");
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
