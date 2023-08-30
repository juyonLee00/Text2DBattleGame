using System;
using System.Collections.Generic;

namespace Text2DBattleGame
{
    public enum Scene
    {
        None, GameIntro, MyInfo, Battle,Inventory
    }

    internal class Program
    {
        public static Scene scene = Scene.None;

        private static Character player = new Character();

        //private static List<Item> itemList = new List<Item>();

        public static SkillManager skillManager = new SkillManager();

        static void Main(string[] args)
        {
            Start start = new Start();
            start.GameDataSetting(ref player);
            //Update();
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
                        Inventory.DisplayInventory(player);
                        break;
                }
            }
        }


        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 전투시작(현재 진행: " + player.DungeonLevel + "층)");
            Console.WriteLine("3. 인벤토리");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 3);
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

            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보르 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
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