using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    public class Skill
    {
        public string Name { get; }

        // Atk, Mp 넣어놨지만 구조상 쓸모가 없어질듯합니다.
        public int Atk { get; }
        public int Mp { get; }

        public Func<Character, string> ShowExplanation { get; set; }

        public Action<Character, Monster[]> UsingSkill { get; set; }

        public Skill(string name, int atk, int mp)
        {
            Name = name;
            Atk = atk;
            Mp = mp;
        }
    }
}
