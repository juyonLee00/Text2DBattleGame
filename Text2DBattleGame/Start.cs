using System;
namespace Text2DBattleGame
{
    public class Start
    {
        public Start(){}

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
                throw new NotImplementedException();
            }
        }

        public void GameDataSetting(Character player)
        {
            // 캐릭터 정보 세팅
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
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("원하는 직업의 번호를 입력하세요");

            string input = Console.ReadLine();
            JobFormat jobData = new JobFormat();

            switch (input)
            {
                case "2":
                    jobData.AddJob("Warrior");
                    break;
                case "1":
                default:
                    jobData.AddJob("Wizard");
                    break;
            }

            player = new Character(playerName, jobData.JobName, 1, jobData.Atk, jobData.Def, jobData.MaxHp, 1500);

        }
    }

    
}
