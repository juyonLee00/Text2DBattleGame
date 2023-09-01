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
            ItemDataSetting();
        }


        public void PlayerDataSetting(ref Character player)
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n");
            string playerName = "";
            do
            {
                try
                {
                    checked
                    {
                        Console.WriteLine("원하는 이름을 설정하세요.");
                        playerName = Console.ReadLine().Trim();
                    }
                }
                catch(OverflowException)
                {
                    Console.WriteLine("해당 이름은 사용하실 수 없습니다.");
                    Console.WriteLine("다른 이름을 입력하세요.");
                }

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

            player = new Character(playerName, jobData.JobName, 1, jobData.Atk, jobData.Def, jobData.MaxHp,
                jobData.MaxMp, 1500, jobData.CriticalRate, jobData.CriticalAtk, jobData.Avoidability);

            CharacterSkillSetting(player);
        }

        public void CharacterSkillSetting(Character player)
        {
            switch(player.Job)
            {
                case "마법사":
                    player.Skills = Program.skillManager.GetWizardSkills();
                    break;
                case "전사":
                    player.Skills = Program.skillManager.GetWarriorSkills();
                    break;
            }
        }

        public void ItemDataSetting()//ref List<IItem> itemList
        {
            var options3 = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
            };

            List<AttackItem> atkItemList = new List<AttackItem>();
            List<AttackItem> atkItemList2 = new List<AttackItem>();
            List<AttackItem> atkItemList3 = new List<AttackItem>();
            List<DefenseItem> defItemList = new List<DefenseItem>();
            List<DefenseItem> defItemList2 = new List<DefenseItem>();
            List<DefenseItem> defItemList3 = new List<DefenseItem>();
            List<PotionItem> potionItemList = new List<PotionItem>();
            List<PotionItem> potionItemList2 = new List<PotionItem>();
            List<PotionItem> potionItemList3 = new List<PotionItem>();
            string strJson = "";

            strJson = File.ReadAllText(@"../../AttackItemDataList.json");
            atkItemList = JsonSerializer.Deserialize<List<AttackItem>>(strJson, options3);

            strJson = File.ReadAllText(@"../../AttackItemDataList2.json");
            atkItemList2 = JsonSerializer.Deserialize<List<AttackItem>>(strJson, options3);

            strJson = File.ReadAllText(@"../../AttackItemDataList3.json");
            atkItemList3 = JsonSerializer.Deserialize<List<AttackItem>>(strJson, options3);


            strJson = File.ReadAllText(@"../../DefenseItemDataList.json");
            defItemList = JsonSerializer.Deserialize<List<DefenseItem>>(strJson, options3);
            strJson = File.ReadAllText(@"../../DefenseItemDataList2.json");
            defItemList2 = JsonSerializer.Deserialize<List<DefenseItem>>(strJson, options3);
            strJson = File.ReadAllText(@"../../DefenseItemDataList3.json");
            defItemList3 = JsonSerializer.Deserialize<List<DefenseItem>>(strJson, options3);


            strJson = File.ReadAllText(@"../../PotionItemDataList.json");
            List<PotionItem> potItemList = JsonSerializer.Deserialize<List<PotionItem>>(strJson, options3);
            strJson = File.ReadAllText(@"../../PotionItemDataList2.json");
            List<PotionItem> potItemList2 = JsonSerializer.Deserialize<List<PotionItem>>(strJson, options3);
            strJson = File.ReadAllText(@"../../PotionItemDataList3.json");
            List<PotionItem> potItemList3 = JsonSerializer.Deserialize<List<PotionItem>>(strJson, options3);

            Program.itemGroup = new ItemGroup(atkItemList, defItemList, potItemList);

            AddAllItemData(atkItemList, defItemList, potItemList, Program.normalTable);
            AddAllItemData(atkItemList2, defItemList2, potItemList2, Program.rareTable);
            AddAllItemData(atkItemList3, defItemList3, potItemList3, Program.UniqueTable);


        }

        public void AddAllItemData(List<AttackItem> atkList, List<DefenseItem> defList, List<PotionItem> potList, List<IItem> itemList)
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