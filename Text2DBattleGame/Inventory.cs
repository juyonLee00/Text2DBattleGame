using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    internal class Inventory
    {
        public static void DisplayInventory(ref Character player)
        {
            Console.Clear();

            Console.WriteLine("[인벤토리]");
            Console.WriteLine("");
            Console.WriteLine("아이템 목록");
            int i = 0;

            foreach (IItem item in player.Inventory)
            {
                Console.Write($"{i+1}. ");

                if (item.IsEquip == true)
                {
                    Console.Write("[E] ");
                }

                Console.Write($"{item.Name}");
                if (item.CanUse) 
                {
                    PotionItem a = item as PotionItem;
                    Console.Write($" - {a.Count} 개");
                }
                Console.WriteLine();

                ++i;
            }
            
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            
            // 1입력되는 버그 수정
            int input = Program.CheckValidInput(0, player.Inventory.Count);
            switch (input)
            {
                case 0:
                    Program.scene = Scene.GameIntro;
                    break;

                default:
                    if (player.Inventory[input - 1].CanUse)//캔유즈라면 소모 아니라면 장착
                    {
                        player.Use(player, input);
                    }
                    else
                    {
                    player.CheckInventoryItem(input-1, ref player);
                    }
                    break;
            }

        }
    }
}
