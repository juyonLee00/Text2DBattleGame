using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Text2DBattleGame
{
    //[Serializable]
    
    public class ItemGroup
    {
        [JsonConstructor]
        public ItemGroup(List<AttackItem> atkItems)
        {
            this.attackList = atkItems;
        }
        public List<AttackItem> attackList { get; set; } = new List<AttackItem>();
        public List<AttackItem> GetList()
        {
            return attackList;
        }
    }
}
