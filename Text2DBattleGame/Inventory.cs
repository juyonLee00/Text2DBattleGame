using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    internal class Inventory
    {
            public static void DisplayInventory(Character player)
            {
                Console.Clear();

                Console.WriteLine("[인벤토리]");
                Console.WriteLine("");
                Console.WriteLine("아이템 목록");

                Console.WriteLine("0. 나가기");
                int i = 1;
                foreach (IItem item in player.Inventory) 
                {
                    Console.WriteLine($"{i}. {item.Name}");
                     ++i;
                }
                Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = Program.CheckValidInput(0,i);
                switch (input)
                {
                    case 0:
                    Program.scene= Scene.GameIntro;
                    break;

                    default:
                    // 사용 함수
                    Program.scene = Scene.GameIntro;
                    break;
                }
            }
                  
    }
}
