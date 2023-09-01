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

        public int Mp { get; }

        public Func<Character, string> ShowExplanation { get; set; }

        public Action<Character, Monster[], int, List<IItem>> UsingSkill { get; set; }

        public Skill(string name, int mp)
        {
            Name = name;
            Mp = mp;
        }
    }
}
