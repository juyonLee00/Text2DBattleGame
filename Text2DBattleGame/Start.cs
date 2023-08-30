using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Text2DBattleGame
{
    public class Start
    {

        public class JobFormat
        {
            public string JobName { get; set; }
            public int Atk { get; set; }
            public int Def { get; set; }
            public int MaxHp { get; set; }
            public int MaxMp { get; set; }

            public List<Skill> Skills { get; set; }


            internal void AddJob(string jobName)
            {
                if (jobName == "Wizard")
                {
                    this.JobName = "마법사";
                    this.Atk = 20;
                    this.Def = 3;
                    this.MaxHp = 60;
                    this.MaxMp = 50;
                    this.Skills = Program.skillManager.GetWizardSkills();
                }
                else if (jobName == "Warrior")
                {
                    this.JobName = "전사";
                    this.Atk = 10;
                    this.Def = 5;
                    this.MaxHp = 100;
                    this.MaxMp = 30;
                    this.Skills = Program.skillManager.GetWarriorSkills();
                }

                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public void GameDataSetting(ref Character player)
        {
            PlayerDataSetting(ref player);
            ItemDataSetting();
        }

        public void PlayerDataSetting(ref Character player)
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n");
            string playerName = "";
            do
            {
                Console.WriteLine("원하는 이름을 설정하세요.");
                playerName = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(playerName));



            Console.Clear();
            Console.WriteLine("원하는 직업을 선택해주세요\n");
            Console.WriteLine("1. 마법사");
            Console.WriteLine("2. 전사");
            Console.WriteLine("원하는 직업의 번호를 입력하세요");

            JobFormat jobData = new JobFormat();

            //코드 CheckValidInput 수정 필요
            int input = Program.CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    jobData.AddJob("Wizard");
                    break;
                case 2:
                    jobData.AddJob("Warrior");
                    break;
            }

            //코드 수정 필요
            player = new Character(playerName, jobData.JobName, 1, jobData.Atk, jobData.Def, jobData.MaxHp, jobData.MaxMp, 1500, jobData.Skills);
        }

        //추후 사라질 함수 - Item목록 Json으로 받아오는 작업용
        public void ItemDataSetting()
        {
            //Inventory Data
            /*
            itemList.Add(new Item("무쇠갑옷", 'd', 5, "무쇠로 만들어져 튼튼한 갑옷입니다.", true));
            itemList.Add(new Item("낡은 검", 'a', 2, "쉽게 볼 수 있는 낡은 검입니다.", false));
            itemList.Add(new Item("나무 몽둥이", 'a', 3, "주위에서 많이 보이는 몽둥이입니다.", true));
            itemList.Add(new Item("스파르타의 갑옷", 'd', 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false));
            itemList.Add(new Item("수련자 갑옷", 'd', 5, "수련에 도움을 주는 갑옷입니다.", 1000, false));
            itemList.Add(new Item("청동 도끼", 'a', 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500, false));
            itemList.Add(new Item("스파르타의 창", 'a', 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000, false));
            */

        }
    }

    
}
