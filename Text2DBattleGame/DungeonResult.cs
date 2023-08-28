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
        public void Result(Character player) 
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Battle!! - Result");
            Console.ResetColor();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Victory");
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("던전에서 몬스터 n 마리를 잡았습니다.");
            Console.WriteLine("[캐릭터 정보]");
            Status(player);
            Console.WriteLine("[획득 아이템]");
            GetItem(player);
            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");

        }
        public void Status(Character player) 
        {
            int getdamege = 0;//체력 데미지값 받기 
            int getexp = 0; // 경험치값 받아서 수정
            float upAtk = 1f; // 발제 에서는 0.5 였으나 기본 설정이 int 이기에 우선 1로 설정함
            float upDef = 2f;
            bool levelup = false;
            if (player.Exp >= 100)
            {
                player.Level = 5;
                levelup= true;
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
                Console.WriteLine($"HP.{player.Hp + getdamege} -> {player.Hp} ");
                if (player.Level <= 4)
                    Console.WriteLine($"Exp.{player.Exp - getexp} -> {player.Exp} ");
            }
            else 
            {
                Console.WriteLine($"Lv.{player.Level} {player.Name} ");
                Console.WriteLine($"HP.{player.Hp + getdamege} -> {player.Hp} ");
                if (player.Level <= 4)
                    Console.WriteLine($"Exp.{player.Exp - getexp} -> {player.Exp} ");
            }

        }

        public void GetItem(Character player) 
        {
            Console.WriteLine($"{player.Gold} Gold");
            Console.WriteLine($"아마 얻은 아이템 리스트.name");
            Console.WriteLine($"");
        }

        //public void StageClear(ICharacter character)
        //{
        //    if (character is Monster)
        //    {
        //        Console.WriteLine($"스테이지 클리어! {character.Name}을 물리쳤습니다!");

        //        if (itemList != null)
        //        {
        //            Console.WriteLine("아래의 보상 아이템 중 하나를 선택하여 사용할 수 있습니다:");
        //            foreach (var item in itemList)
        //            {
        //                Console.WriteLine(item.Name);
        //            }

        //            Console.WriteLine("사용할 아이템 이름을 입력하세요:");
        //            string input = Console.ReadLine();
        //            IItem selectedItem = itemList.Find(item => item.Name == input);
        //            if (selectedItem != null)
        //            {
        //                selectedItem.Use((Warrior)player);
        //            }
        //        }
        //        player.Health = 100;
        //    }
        //    else
        //    {
        //        Console.WriteLine("게임오버");
        //    }
        //}

    }
}
