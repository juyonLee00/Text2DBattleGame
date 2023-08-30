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

        public void GameDataSetting(ref Character player, ref List<IItem> itemList)
        {
            PlayerDataSetting(ref player);
            ItemDataSetting(ref itemList);
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

            player = new Character(playerName, jobData.JobName, 1, jobData.Atk, jobData.Def, jobData.MaxHp, jobData.MaxMp, 1500,
                jobData.Skills, jobData.CriticalRate, jobData.CriticalAtk, jobData.Avoidability);
        }

        public void ItemDataSetting(ref List<IItem> itemList)
        {
            var options3 = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
            };
           
            string strJson = File.ReadAllText(@"../../AttackItemDataList.json");
            List<AttackItem> atkItemList = JsonSerializer.Deserialize<List<AttackItem>>(strJson, options3);


            strJson = File.ReadAllText(@"../../DefenseItemDataList.json");
            List<DefenseItem> defItemList = JsonSerializer.Deserialize<List<DefenseItem>>(strJson, options3);


            strJson = File.ReadAllText(@"../../PotionItemDataList.json");
            List<PotionItem> potItemList = JsonSerializer.Deserialize<List<PotionItem>>(strJson, options3);


            ItemGroup itemGroup = new ItemGroup(atkItemList, defItemList, potItemList);

            AddAllItemData(atkItemList, defItemList, potItemList, ref itemList);

        }

        public void AddAllItemData(List<AttackItem> atkList, List<DefenseItem> defList, List<PotionItem> potList, ref List<IItem> itemList)
        {
            foreach(AttackItem atkItem in atkList)
            {
                itemList.Add(atkItem);
            }

            foreach(DefenseItem defItem in defList)
            {
                itemList.Add(defItem);
            }

            foreach(PotionItem potItem in potList)
            {
                itemList.Add(potItem);
            }
        }
    }

    
}