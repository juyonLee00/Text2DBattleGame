using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace Text2DBattleGame
{
    class ConsoleManager
    {
        string Paused { get; set; }
        string[] DefenseMotion { get; set; }
        string[] AttackMotion { get; set; }
        string[] SkillAttackMotion { get; set; }
        public ConsoleManager(Character player)
        {

            string strJson;

            switch (player.Job)
            {
                case "전사":
                    strJson = File.ReadAllText(@"../../warrior.json");
                    Paused = JsonSerializer.Deserialize<string>(strJson);

                    strJson = File.ReadAllText(@"../../warriorDefense.json");
                    DefenseMotion = JsonSerializer.Deserialize<string[]>(strJson);

                    strJson = File.ReadAllText(@"../../warriorAttack.json");
                    AttackMotion = JsonSerializer.Deserialize<string[]>(strJson);

                    strJson = File.ReadAllText(@"../../warriorSkillAttack.json");
                    SkillAttackMotion = JsonSerializer.Deserialize<string[]>(strJson);
                    break;
                case "마법사":
                    strJson = File.ReadAllText(@"../../wizzard.json");
                    Paused = JsonSerializer.Deserialize<string>(strJson);

                    strJson = File.ReadAllText(@"../../wizzardDefense.json");
                    DefenseMotion = JsonSerializer.Deserialize<string[]>(strJson);

                    strJson = File.ReadAllText(@"../../wizzardAttack.json");
                    AttackMotion = JsonSerializer.Deserialize<string[]>(strJson);

                    strJson = File.ReadAllText(@"../../wizzardSkillAttack.json");
                    SkillAttackMotion = JsonSerializer.Deserialize<string[]>(strJson);
                    break;
            }
        }

        public void PlayersTurnPausedDisplay(Monster[] battleMonsters, Character player, bool attack, string print)
        {
            Console.Clear();
            Console.SetCursorPosition(1, 1);
            Console.WriteLine(Paused);

            int originalTop = StatusDisplay(battleMonsters, player, attack, print);

            DrawLine();
            DrawCharacterStatLine();
            DrawMonsterStatLine();

            Console.SetCursorPosition(2, originalTop);
        }

        public void PlayerDefenseMotionDisplay(Monster[] battleMonsters, Character player)
        {
            Console.Clear();
            int originalTop = StatusDisplay(battleMonsters, player, false, "");

            foreach (string motion in DefenseMotion)
            {
                Console.CursorVisible = false;

                Console.SetCursorPosition(1, 1);
                Console.WriteLine(motion);

                DrawLine();
                DrawCharacterStatLine();
                DrawMonsterStatLine();

                Thread.Sleep(100);
            }

            Console.SetCursorPosition(0, originalTop);
            Console.CursorVisible = true;
        }

        public void PlayerAttackMotionDisplay(Monster[] battleMonsters, Character player)
        {
            Console.Clear();
            int originalTop = StatusDisplay(battleMonsters, player, false, "");

            foreach (string motion in AttackMotion)
            {
                Console.CursorVisible = false;

                Console.SetCursorPosition(1, 1);
                Console.WriteLine(motion);

                DrawLine();
                DrawCharacterStatLine();
                DrawMonsterStatLine();

                Thread.Sleep(100);
            }

            Console.SetCursorPosition(0, originalTop);
            Console.CursorVisible = true;
        }

        public void PlayerSkillAttackMotionDisplay(Monster[] battleMonsters, Character player)
        {
            Console.Clear();
            int originalTop = StatusDisplay(battleMonsters, player, false, "");

            foreach (string motion in SkillAttackMotion)
            {
                Console.CursorVisible = false;

                Console.SetCursorPosition(1, 1);
                Console.WriteLine(motion);

                DrawLine();
                DrawCharacterStatLine();
                DrawMonsterStatLine();

                Thread.Sleep(100);
            }

            Console.SetCursorPosition(0, originalTop);
            Console.CursorVisible = true;
        }

        static int StatusDisplay(Monster[] battleMonsters, Character player, bool attack, string print) ///////////////////
        {

            int cursor_y = 24;
            Console.SetCursorPosition(82, cursor_y++);
            Console.WriteLine("[내정보]");
            Console.SetCursorPosition(82, cursor_y++);
            Console.WriteLine("Lv." + player.Level + " " + player.Name + " (" + player.Job + ")");
            Console.SetCursorPosition(82, cursor_y++);
            Console.WriteLine("Hp " + player.Hp + "/" + player.MaxHp);
            Console.SetCursorPosition(82, cursor_y++);
            Console.WriteLine("MP " + player.Mp + "/" + player.MaxMp);

            cursor_y = 24;
            int i = 1;
            foreach (Monster monster in battleMonsters)
            {
                Console.SetCursorPosition(2, cursor_y++);
                if (monster.IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    if (attack) Console.Write(i++);
                    Console.WriteLine(" Lv." + monster.Level + " " + monster.Name + "  Dead ");
                    Console.ResetColor();
                }
                else
                {
                    if (attack) Console.Write(i++);
                    Console.WriteLine(" Lv." + monster.Level + " " + monster.Name + "  HP " + monster.Hp);
                }
            }

            cursor_y++;
            Console.SetCursorPosition(2, cursor_y);
            Console.Write("                                             ");
            Console.SetCursorPosition(2, cursor_y++);
            Console.WriteLine(print);

            return Console.CursorTop;
        }

        public static void DrawLine() //테두리 그리는 함수
        {
            for (int i = 0; i < 110; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("-");
                Console.SetCursorPosition(i, 34);
                Console.Write("-");
            }

            for (int i = 1; i < 34; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
                Console.SetCursorPosition(110, i);
                Console.Write("|");
            }
        }

        public static void DrawCharacterStatLine() //플레이어 정보 나타낼 테두리 그리는 함수
        {
            for (int i = 1; i < 34; i++)
            {
                Console.SetCursorPosition(80, i);
                Console.Write("|");
            }
            for (int i = 81; i < 109; i++)
            {
                Console.SetCursorPosition(i, 23);
                Console.Write("-");
            }
        }

        public static void DrawMonsterStatLine() //몬스터 정보 나타낼 테두리 그리는 함수
        {
            for (int i = 1; i < 80; i++)
            {
                Console.SetCursorPosition(i, 23);
                Console.Write("-");
            }
            Console.SetCursorPosition(80, 23);
            Console.Write("+");
        }

    }
}
