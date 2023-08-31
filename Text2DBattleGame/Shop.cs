using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    public class Shop
    {
        public static List<IItem> ShopItemList { get; set; }
        public Shop()
        {
            ShopItemList = new List<IItem>();
            TestShopData();
        }

        private static void TestShopData()
        {
            AttackItem test1 = new AttackItem("테스트용 샵무기", "날카로운 날", false, 5000, 10, 10, 100, ItemType.Attack, 100);
            DefenseItem test2 = new DefenseItem("테스트용 샵갑빠", "튼튼한 강철갑빠", false, 5, 10000, 100, 100, ItemType.Defense, 10);
            PotionItem test3 = new PotionItem("테스트용 포-션", "맛은없다.", false, 0, 0, 1000, 1000, ItemType.Potion, 1);


            ShopItemList.Add(test1);
            ShopItemList.Add(test2);
            ShopItemList.Add(test3);
        }

        public static void BuyItem(Character player, int itemNum)
        {
            IItem item = (IItem)ShopItemList[itemNum].CreateClone();

            if (player.Gold < item.Price)
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
            else
            {
                if (!player.Inventory.Exists(x => x.Name == item.Name))
                {
                    player.Inventory.Add(item);
                    player.Gold -= item.Price;
                    Console.WriteLine("구매를 완료했습니다.");
                }
                else
                    Console.WriteLine("이미 구입한 아이템입니다.");
            }
        }

        public static void SellItem(Character player, int itemNum)
        {
            IItem item = player.Inventory[itemNum];

            if (player.Inventory.Exists(x => x.Name == item.Name))
            {
                // 장착중이라면 해제
                //player.ChangeEquipment(item);

                player.Inventory.Remove(item);
                player.Gold += (int)(item.Price * 0.85f);
                Console.WriteLine("판매를 완료했습니다.");
            }
            else
                Console.WriteLine("판매할 수 없는 아이템입니다.");
        }

        public static void DisplayShop(Character player)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            // 문자열길이를 내림차순 정렬, 같으면 사전 순
            ShopItemList.Sort((a, b) => {
                if (a.Name.Length > b.Name.Length)
                    return -1;
                else if (a.Name.Length < b.Name.Length)
                    return 1;
                else
                    return String.Compare(a.Name, b.Name);
            });

            for (int i = 0; i < ShopItemList.Count; i++)
            {
                IItem item = ShopItemList[i];
                string itemName = item.Name.PadRight(20, ' ');
                string itemPrice = (player.Inventory.Find(x => x.Name == item.Name) != null) ? "구매완료" : $"{item.Price} G";

                Console.WriteLine(string.Concat($"- {itemName} | {item.Description}".PadRight(50, ' '), " | ", itemPrice));
            }

            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = Program.CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    Program.scene = Scene.GameIntro;
                    break;
                case 1:
                    Program.scene = Scene.ShopBuyItem;
                    break;
                case 2:
                    Program.scene = Scene.ShopSellItem;
                    break;
            }
        }
        public static void DisplayShopBuyItem(Character player)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            ShopItemList.Sort((a, b) => {
                if (a.Name.Length > b.Name.Length)
                    return -1;
                else if (a.Name.Length < b.Name.Length)
                    return 1;
                else
                    return String.Compare(a.Name, b.Name);
            });

            for (int i = 0; i < ShopItemList.Count; i++)
            {
                IItem item = ShopItemList[i];
                string itemName = item.Name.PadRight(20, ' ');
                string itemPrice = (player.Inventory.Find(x => x.Name == item.Name) != null) ? "구매완료" : $"{item.Price}";

                Console.WriteLine(string.Concat($"- {i + 1} {itemName} | {item.Description}".PadRight(30, ' '), " | ", itemPrice));
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = Program.CheckValidInput(0, ShopItemList.Count);

                if (input == 0)
                {
                    Program.scene = Scene.Shop;

                    break;
                }
                else
                {
                    BuyItem(player, input - 1);

                    Thread.Sleep(1000);

                    break;
                }
            }
        }
        public static void DisplayShopSellItem(Character player)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 아이템 판매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            player.Inventory.Sort((a, b) => {
                if (a.Name.Length > b.Name.Length)
                    return -1;
                else if (a.Name.Length < b.Name.Length)
                    return 1;
                else
                    return String.Compare(a.Name, b.Name);
            });

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                IItem item = player.Inventory[i];
                string itemName = item.Name.PadRight(20, ' ');

                Console.WriteLine(string.Concat($"- {i + 1} {itemName} | {item.Description}".PadRight(30, ' '), " | ", item.Price));
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = Program.CheckValidInput(0, player.Inventory.Count);

                if (input == 0)
                {
                    Program.scene = Scene.Shop;

                    break;
                }
                else
                {
                    SellItem(player, input - 1);

                    Thread.Sleep(1000);

                    break;
                }
            }
        }
    }
}
