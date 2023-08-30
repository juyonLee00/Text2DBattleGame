using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

using System.Text.Encodings.Web;
using System.Text.Unicode;


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
            public float CriticalRate { get; set; }
            public float CriticalAtk { get; set; }
            public float Avoidability { get; set; }

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
                    this.CriticalRate = 15f;
                    this.CriticalAtk = 1.6f;
                    this.Avoidability = 10f;
                    this.Skills = Program.skillManager.GetWizardSkills();
                }
                else if (jobName == "Warrior")
                {
                    this.JobName = "전사";
                    this.Atk = 10;
                    this.Def = 5;
                    this.MaxHp = 100;
                    this.MaxMp = 30;
                    this.CriticalRate = 15f;
                    this.CriticalAtk = 1.6f;
                    this.Avoidability = 10f;
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
            player = new Character(playerName, jobData.JobName, 1, jobData.Atk, jobData.Def, jobData.MaxHp, jobData.MaxMp, 1500,
                jobData.Skills, jobData.CriticalRate, jobData.CriticalAtk, jobData.Avoidability);
        }

        public void ItemDataSetting()
        {
            var options3 = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                //WriteIndented = true,
                PropertyNameCaseInsensitive = true,
            };
            //string filePath = "../../AttackItemDataList.json";


            

            //itemGroup.attkList = new List<AttackItem>();
            //itemGroup.atkList.Add(new AttackItem("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", false, 2, 0, 0, 0));
            
            List<AttackItem> atkItemList = new List<AttackItem>();
            atkItemList.Add(new AttackItem("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", false, 2, 0, 0, 0));
            atkItemList.Add(new AttackItem("나무 몽둥이", "주위에서 많이 보이는 몽둥이입니다.", false, 3, 0, 0, 0));
            atkItemList.Add(new AttackItem("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", false, 5, 0, 0, 0));
            atkItemList.Add(new AttackItem("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", false, 7, 0, 0, 0));

            ItemGroup itemGroup = new ItemGroup(atkItemList);

            //itemGroup.attackList = atkItemList;

            var strJson = JsonSerializer.Serialize(itemGroup.GetList(), options3);

            Console.WriteLine(strJson);

            var atkItemList1 = JsonSerializer.Deserialize<List<AttackItem>>(strJson, options3);

            /*foreach (AttackItem atkItem in atkItemList)
            {
                Console.WriteLine($"{atkItem.Name} {atkItem.Description}");
            }
            */

            //File.WriteAllText(filePath, strJson);

            /*
            filePath = "../../DefenseItemDataList.json";
            List<DefenseItem> defItemList = new List<DefenseItem>();
            defItemList.Add(new DefenseItem("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", false, 0, 5, 0, 0));
            defItemList.Add(new DefenseItem("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", false, 0, 15, 0, 0));
            defItemList.Add(new DefenseItem("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", false, 0, 5, 0, 0));

            strJson = JsonSerializer.Serialize<List<DefenseItem>>(defItemList, options3);
            File.WriteAllText(filePath, strJson);


            filePath = "../../PotionItemDataList.json";
            List<PotionItem> potionItemList = new List<PotionItem>();
            potionItemList.Add(new PotionItem("HP 회복 포션(소)", "소량의 HP를 회복할 수 있습니다.", false, 0, 0, 10, 0));
            potionItemList.Add(new PotionItem("MP 회복 포션(소)", "소량의 MP를 회복할 수 있습니다.", false, 0, 0, 0, 10));

            strJson = JsonSerializer.Serialize<List<PotionItem>>(potionItemList, options3);
            File.WriteAllText(filePath, strJson);
            */

            //string jsonString = File.ReadAllText(@"../../AttackItemDataList.json");


            //Console.WriteLine(jsonString);


        }
    }

    
}