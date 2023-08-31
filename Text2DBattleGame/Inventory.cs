﻿using System;
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

            AttackItem item1 = new AttackItem("스파르타의 창", "아주 오래된 검입니다.", false, 2, 0, 0, 0, 'a');
            DefenseItem item2 = new DefenseItem("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", false, 0, 5, 0, 0, 'd');
            AttackItem item3 = new AttackItem("청동 도끼", "아주 오래된 도끼입니다.", false, 7, 0, 0, 0, 'a');

            player.Inventory.Add(item1);
            player.Inventory.Add(item2);
            player.Inventory.Add(item3);

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
            
            int input = Program.CheckValidInput(0, i);

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
                    player.EquipItem(input-1, ref player);
                    }
                    break;
            }

        }
    }
}
