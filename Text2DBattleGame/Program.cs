using System;
using System.Collections.Generic;

namespace Text2DBattleGame
{
    public enum Scene
    {
        None, GameIntro, MyInfo, Battle, Inventory, Shop, ShopBuyItem, ShopSellItem
    }

    internal class Program
    {
        public static Scene scene = Scene.None;

        private static Character player = new Character();

        private static List<IItem> itemList = new List<IItem>();

        public static ItemGroup itemGroup = new ItemGroup();

        public static SkillManager skillManager = new SkillManager();

        public static Shop shop = new Shop();
      
        public static List<IItem> errortable = new List<IItem>() { new PotionItem("에러포션", "체력,마나를 1회복하고 공격력 방어력이 1 상승한다", false,1,1,1,1,ItemType.Potion, 10) };

        public static List<IItem> normalTable = new List<IItem>() { new AttackItem("노말아이템", "test", true, 1, 1, 1, 1, ItemType.Attack, 0) };

        public static List<IItem> rareTable = new List<IItem>() { new AttackItem("레어아이템", "test", true, 1, 1, 1, 1, ItemType.Attack, 0) };

        public static List<IItem> UniqueTable = new List<IItem>() { new AttackItem("유니크아이템", "test", true, 1, 1, 1, 1, ItemType.Attack, 0) };

        static void Main(string[] args)
        {
            Start start = new Start();
            start.GameDataSetting(ref player, ref itemList);
            Update();
        }

        public static void Update()
        {
            while (true)
            {
                switch (scene)
                {
                    case Scene.None:
                        DisplayGameIntro();
                        break;
                    case Scene.GameIntro:
                        DisplayGameIntro();
                        break;
                    case Scene.MyInfo:
                        DisplayMyInfo();
                        break;
                    case Scene.Battle:
                        DisplayBattle.Display(player);
                        break;
                    case Scene.Inventory:
                        Inventory.DisplayInventory(ref player);
                        break;
                    case Scene.Shop:
                        Shop.DisplayShop(player);
                        break;
                    case Scene.ShopBuyItem:
                        Shop.DisplayShopBuyItem(player);
                        break;
                    case Scene.ShopSellItem:
                        Shop.DisplayShopSellItem(player);
                        break;
                }
            }
        }


        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 전투시작(현재 진행: " + player.DungeonLevel + "층)");
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 상점");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 4);
            switch (input)
            {
                case 1:
                    scene = Scene.MyInfo;
                    break;

                case 2:
                    scene = Scene.Battle;
                    break;

                case 3:
                    scene = Scene.Inventory;
                    break;
                case 4:
                    scene = Scene.Shop;
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk} {player.ShowAtkItemEffect(ref player, ref itemGroup)}");
            Console.WriteLine($"방어력 : {player.Def} {player.ShowDefItemEffect(ref player, ref itemGroup)}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    scene = Scene.GameIntro;
                    break;
            }
        }

        static public int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}