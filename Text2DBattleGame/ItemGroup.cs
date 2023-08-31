using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Text2DBattleGame
{
    public class ItemGroup
    {
        public ItemGroup() { }

        [JsonConstructor]
        public ItemGroup(List<AttackItem> atkItems, List<DefenseItem> defItems, List<PotionItem> potItems)
        {
            this.attackList = atkItems;
            this.defenseList = defItems;
            this.potionList = potItems;
        }
        public List<AttackItem> attackList { get; set; } = new List<AttackItem>();
        public List<DefenseItem> defenseList { get; set; } = new List<DefenseItem>();
        public List<PotionItem> potionList { get; set; } = new List<PotionItem>();
        public List<IItem> itemAllList { get; set; } = new List<IItem>();

        public List<AttackItem> GetAtkList()
        {
            return attackList;
        }

        public List<DefenseItem> GetDefList()
        {
            return defenseList;
        }

        public List<PotionItem> GetPotList()
        {
            return potionList;
        }

        public List<IItem> GetIItemList()
        {
            return itemAllList;
        }
    }
}
