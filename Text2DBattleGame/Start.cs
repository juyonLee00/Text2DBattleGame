using System;
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


            internal void AddJob(string jobName)
            {
                if (jobName == "Wizard")
                {
                    this.JobName = "마법사";
                    this.Atk = 20;
                    this.Def = 3;
                    this.MaxHp = 60;
                }
                else if (jobName == "Warrior")
                {
                    this.JobName = "전사";
                    this.Atk = 10;
                    this.Def = 5;
                    this.MaxHp = 100;
                }

                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public void GameDataSetting(Character player)
        {
            PlayerDataSetting(player);
            ItemDataSetting();
        }

        public void PlayerDataSetting(Character player)
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

            //코드 수정 필z
            player = new Character(playerName, jobData.JobName, 1, jobData.Atk, jobData.Def, jobData.MaxHp, 1500);
        }

        public void ItemDataSetting()
        {
            //Inventory Data
        }
    }

    
}
