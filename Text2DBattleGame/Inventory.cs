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

                if (player.AtkEquipList.Exists(x => x.Name == item.Name) || player.DefEquipList.Exists(x => x.Name == item.Name))
                {
                    Console.Write("[E] ");
                }

                Console.Write($"{item.Name}");
                for(int j = 0; j < 10-item.Name.Length; j++)
                {
                    Console.Write(" ");
                }

                Console.Write($"| 공격 : {item.Atk}");
                Console.Write($"| 방어 : {item.Def}");
                Console.Write($"| 체력 : {item.Hp}");
                Console.Write($"| 마나 : {item.Mp}");

                int showItemCount = item.Count;
                if(item.IsEquip == true && item.Count >= 1)
                {
                    showItemCount = item.Count - 1;
                }
                Console.Write($"| {showItemCount} 개");


                Console.WriteLine();

                ++i;
            }
            
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            
            int input = Program.CheckValidInput(0, player.Inventory.Count);
            switch (input)
            {
                case 0:
                    Program.scene = Scene.GameIntro;
                    break;

                default:
                    if (player.Inventory[input - 1].CanUse)
                    {
                        player.Use(ref player, input);
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
