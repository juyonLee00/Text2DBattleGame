using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text2DBattleGame
{
    public interface ICharacter
    {
        string Name { get; }
        int Hp { get; set; }
        int Atk { get; }
        bool IsDead { get; }
        int Level { get; set; }
        int Exp { get; set; }
        int Gold { get; set; }
        float CriticalRate { get; set; }
        float CriticalAtk { get; set; }
        float Avoidability { get; set; }

        void TakeDamage(int damage);
    }

    public class Character : ICharacter
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int MaxHp { get; }
        public int MaxMp { get; set; }
        public bool IsDead => Hp <= 0;
        public int DungeonLevel { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public float CriticalRate { get; set; }
        public float CriticalAtk { get; set; }
        public float Avoidability { get; set; }

        public Character() { }

        public List<IItem> Inventory { get; set; }

        public List<IItem> AtkEquipList { get; set; }
        public List<IItem> DefEquipList { get; set; }
        public List<Skill> Skills { get; set; }
        public Character(string name, string job, int level, int atk, int def, int hp, int mp, int gold, float criticalRate, float criticalAtk, float avoidability)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Mp = mp;
            MaxHp = hp;
            MaxMp = mp;
            Gold = gold;
            Exp = 0;
            DungeonLevel = 1;
            Inventory = new List<IItem>();
            AtkEquipList = new List<IItem>();
            DefEquipList = new List<IItem>();
            Skills = new List<Skill>();
            CriticalRate = criticalRate;
            CriticalAtk = criticalAtk;
            Avoidability = avoidability;
        }
        public void TakeDamage(int damage)
        {
            Hp -= damage;
        }

        public void UsingMp(int useMp)
        {
            Mp -= useMp;

            if (Mp < 0)
                Mp = 0;
        }

        public string ShowItemEffect(ref Character player, ref ItemGroup itemGroup, ItemType itemType)
        {
            int value;
            string answer = "";
            bool isAnsGet = false;

            switch (itemType)
            {
                case ItemType.Attack:
                    if (player.AtkEquipList.Count != 0)
                    {
                        foreach (IItem item in player.AtkEquipList)
                        {
                            value = item.Atk;
                            answer = "(+" + value.ToString() + ")";
                            isAnsGet = true;
                            if (isAnsGet)
                                break;
                        }
                    }
                    break;
                case ItemType.Defense:
                    if (player.DefEquipList.Count != 0)
                    {
                        foreach (IItem item in player.DefEquipList)
                        {
                            value = item.Def;
                            answer = "(+" + value.ToString() + ")";
                            isAnsGet = true;
                            break;
                        }
                        if (isAnsGet)
                            break;
                    }
                    break;
            }
            return answer;
        }

        public void CheckInventoryItem(int itemIdx, ref Character player)
        {
            Console.Clear();
            IItem curItem = Inventory[itemIdx];
            ItemType curItemType = curItem.ItemType;
            int input;
            bool haveSameTypeItem = false;

            
            if (curItemType == ItemType.Potion)
            {
                Console.WriteLine($"{curItem.Name}를 사용했습니다.");

                player.Atk += curItem.Atk;
                player.Def += curItem.Def;
                player.Hp += curItem.Hp;
                player.Mp += curItem.Mp;

                curItem.Count -= 1;
            }

            else
            {
                if (!curItem.IsEquip)
                {
                    foreach (IItem EquipedItem in Inventory)
                    {
                        if (EquipedItem.ItemType == curItemType && EquipedItem.IsEquip)
                        {
                            haveSameTypeItem = true;
                            IItem equipedItem = EquipedItem;
                            Console.WriteLine($"이미 동일한 속성의 아이템 {EquipedItem.Name}을(를) 장착하고 있습니다.");
                            Console.WriteLine("아이템을 교체하시겠습니까?\n");

                            Console.WriteLine("1. 예");
                            Console.WriteLine("0. 아니오");

                            input = Program.CheckValidInput(0, 1);
                            switch (input)
                            {
                                case 0:
                                    Program.scene = Scene.Inventory;
                                    break;
                                case 1:
                                    EquipedItem.IsEquip = false;
                                    curItem.IsEquip = true;

                                    if (curItem.ItemType == ItemType.Attack)
                                    {
                                        RemoveEquipListItem(ref player, ref equipedItem, ItemType.Attack);
                                        EquipCurItem(ref player, ref curItem, ItemType.Attack);
                                    }

                                    else
                                    {
                                        RemoveEquipListItem(ref player, ref equipedItem, ItemType.Defense);
                                        EquipCurItem(ref player, ref curItem, ItemType.Defense);
                                    }

                                    Console.WriteLine($"\n아이템이 {EquipedItem.Name}에서 {curItem.Name}(으)로 교체되었습니다.");
                                    Console.WriteLine($"{curItem.Name}을(를) 장착 성공했습니다.");
                                    break;
                            }

                            if (input == 1)
                                break;
                        }
                    }

                    if (!haveSameTypeItem)
                    {
                        curItem.IsEquip = true;

                        if (curItem.ItemType == ItemType.Attack)
                        {
                            EquipCurItem(ref player, ref curItem, ItemType.Attack);
                        }

                        else
                        {
                            EquipCurItem(ref player, ref curItem, ItemType.Defense);
                        }
                        Console.WriteLine($"{curItem.Name}을(를) 장착 성공했습니다.");
                    }

                }
                else
                {
                    Console.WriteLine("이미 착용한 아이템입니다.");
                    Console.WriteLine("아이템 착용을 해제하시겠습니까?\n");

                    Console.WriteLine("1. 예");
                    Console.WriteLine("0. 아니오");

                    input = Program.CheckValidInput(0, 1);
                    switch (input)
                    {
                        case 0:
                            Program.scene = Scene.Inventory;
                            break;
                        case 1:
                            if (curItem.ItemType == ItemType.Attack)
                            {
                                RemoveEquipListItem(ref player, ref curItem, ItemType.Attack);
                            }
                            else if (curItem.ItemType == ItemType.Defense)
                            {
                                RemoveEquipListItem(ref player, ref curItem, ItemType.Defense);
                            }
                            Console.WriteLine($"{curItem.Name} 아이템 장착을 해제했습니다!");
                            break;

                    }
                }
            }

            Console.WriteLine("\n1. 인벤토리");
            Console.WriteLine("0. 나가기");

            input = Program.CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    Program.scene = Scene.GameIntro;
                    break;
                case 1:
                    Program.scene = Scene.Inventory;
                    break;
            }

        }

        public void EquipCurItem(ref Character player, ref IItem curItem, ItemType itemType)
        {
            curItem.IsEquip = true;
            player.Atk += curItem.Atk;
            player.Def += curItem.Def;
            player.Hp += curItem.Hp;
            player.Mp += curItem.Mp;

            if(curItem.Count > 1)
            {
                curItem.Count -= 1;
            }

            switch (itemType)
            {
                case ItemType.Attack:
                    player.AtkEquipList.Add(curItem);
                    
                    break;
                case ItemType.Defense:
                    player.DefEquipList.Add(curItem);
                    break;
            }
        }


        public void RemoveEquipListItem(ref Character player, ref IItem curItem, ItemType itemType)
        {
            Console.WriteLine("Remove 들어옴");
            curItem.IsEquip = false;
            player.Atk -= curItem.Atk;
            player.Def -= curItem.Def;
            player.Atk -= curItem.Atk;
            player.Def -= curItem.Def;

            if (curItem.Count > 1)
            {
                curItem.Count += 1;
            }

            switch (itemType)
            {
                case ItemType.Attack:
                    player.AtkEquipList.Remove(curItem);
                    break;
                case ItemType.Defense:
                    player.DefEquipList.Remove(curItem);
                    break;
            }
        }

        public void Use(ref Character player, int i)
        {
            PotionItem item = player.Inventory[i - 1] as PotionItem;
            Console.Clear();
            if (item.Hp != 0)
            {
                if (player.Hp + item.Hp > player.MaxHp)
                {
                    Console.WriteLine($"체력을 {player.MaxHp - player.Hp}회복 하였습니다.");
                    Console.WriteLine($"{player.Hp} -> {player.MaxHp}");

                    player.Hp = player.MaxHp;
                }
                else
                {
                    Console.WriteLine($"체력을 {item.Hp}회복 하였습니다.");
                    Console.WriteLine($"{player.Hp} -> {player.Hp + item.Hp}");

                    player.Hp += item.Hp;
                }
            }
            if (item.Mp != 0)
            {
                if (player.Mp + item.Mp > player.MaxMp)
                {
                    Console.WriteLine($"마나를 {player.MaxMp - player.Mp}회복 하였습니다.");
                    Console.WriteLine($"{player.Mp} -> {player.MaxMp}");

                    player.Mp += player.MaxMp;
                }
                else
                {
                    Console.WriteLine($"마나를 {item.Mp}회복 하였습니다.");
                    Console.WriteLine($"{player.Mp} -> {player.Mp + item.Mp}");

                    player.Mp += item.Mp;
                }
            }
            if (item.Atk != 0)
            {
                player.Atk += item.Atk;
                Console.WriteLine($"공격력이 {Atk}증가 하였습니다.");
                Console.WriteLine($"{player.Atk - item.Atk} -> {player.Atk}");
            }
            if (item.Def != 0)
            {
                player.Def += item.Def;
                Console.WriteLine($"방어력이 {Def}증가 하였습니다.");
                Console.WriteLine($"{player.Def - item.Def} -> {player.Def}");
            }
            item.Count--;
            if (item.Count <= 0)
                player.Inventory.RemoveAt(i - 1);
            Console.WriteLine();
            Console.WriteLine("0. 인벤토리");

            int input = Program.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    Program.scene = Scene.Inventory;
                    break;
            }

        }
    }

    public class Monster : ICharacter
    {
        public string Name { get; }
        public int Level { get; set; }
        public int Atk { get; }
        public int Hp { get; set; }
        public int Exp  { get; set; }
         public bool IsDead => Hp <= 0;
        public int Gold { get; set; }
        public float CriticalRate { get; set; }
        public float CriticalAtk { get; set; }
        public float Avoidability { get; set; }
        public Monster(string name, int level, int hp, int atk, int exp , int gold, float criticalRate, float criticalAtk, float avoidability)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Hp = hp;
            Exp = exp;
            Gold =gold;
            CriticalRate = criticalRate;
            CriticalAtk = criticalAtk;
            Avoidability = avoidability;
        }
        public void TakeDamage(int damage)
        {
            Hp -= damage;
        }
        static public IItem Drop(List<IItem> droptable1)
        {
            int persent = 100;
            Random random = new Random();
            int basic = random.Next(1, 100);
            int i = random.Next(0, droptable1.Count);
            
                if (persent >= basic)
                {
                    IItem item = droptable1[i];
                    return item;
                }
            
            
            return null;
        }

    }
    public class Minion : Monster
    {
        public Minion() : base("미니언", 2, 15, 5 , 1 ,5, 0f, 0f, 10f) { }


    }
    public class EmptinessBug : Monster
    {
        public EmptinessBug() : base("공허충", 3, 10, 5, 2, 10, 0f, 0f, 10f) { }
    }
    public class CanonMinion : Monster
    {
        public CanonMinion() : base("대포미니언", 5, 25, 8, 3, 15, 0f, 0f, 10f) { }
    }
    public class CorruptedSpider : Monster
    {
        public CorruptedSpider() : base("타락한 거미", 7, 25, 10, 5, 20, 0f, 0f, 10f) { }
    }
    public class Hiding : Monster
    {
        public Hiding() : base("그림자속무언가", 7, 5, 14, 5, 20, 0f, 0f, 10f) { }
    }
    public class CorruptedQueen : Monster
    {
        public CorruptedQueen() : base("타락한 거미여왕", 10, 40, 15, 10, 30, 0f, 0f, 10f) { }
    }
    public class WaterSnake : Monster
    {
        public WaterSnake() : base("수중뱀", 10, 25, 12, 7, 25, 0f, 0f, 10f) { }
    }
    public class WaterSerpent : Monster
    {
        public WaterSerpent() : base("수중서펀트", 10, 40, 16, 8, 27, 0f, 0f, 10f) { }
    }
    public class Drangon : Monster
    {
        public Drangon() : base("드래곤", 15, 60, 20, 20, 50, 0f, 0f, 10f) { }
    }

    public class CreateCharacter
    {
        public static Monster[] CreateRandomMonster(int dungeonlevel)
        {
            Monster monster = new Monster("zizon", 100, 1000, 99, 0, 0, 0f, 0f, 10f);

            Random random = new Random();

            int howManyMax = 4;
            int howManyMin = 1;
            if (dungeonlevel < 16)
            {
                if (dungeonlevel % 5 == 2 || dungeonlevel % 5 == 4)
                {
                    howManyMax = 5;
                }
                if (dungeonlevel % 5 == 0)
                {
                    howManyMin = 4;
                }
            }
            else
            {
                howManyMin = (dungeonlevel > 35) ? 20 : 1 + (dungeonlevel - 16);
                howManyMax = (dungeonlevel > 35) ? 23 : 4 + (dungeonlevel - 16);
            }

            int howMany = random.Next(howManyMin, howManyMax + 1);
            Monster[] battleMonsters = new Monster[howMany];

            for (int i = 0; i < howMany; i++)
            {
                int endNum = ((dungeonlevel - 1) / 5 + 1) * 3;
                int startNum = (dungeonlevel - 5 * ((dungeonlevel - 1) / 5) - 1) / 2 - 3 + endNum;

                if (dungeonlevel >= 16)
                {
                    startNum = 0;
                    endNum = 9;
                }

                int flag = random.Next(startNum, endNum);
                switch (flag)
                {
                    case 0:
                        monster = new Minion();
                        break;
                    case 1:
                        monster = new EmptinessBug();
                        break;
                    case 2:
                        monster = new CanonMinion();
                        break;
                    case 3:
                        monster = new CorruptedSpider();
                        break;
                    case 4:
                        monster = new Hiding();
                        break;
                    case 5:
                        monster = new CorruptedQueen();
                        break;
                    case 6:
                        monster = new WaterSnake();
                        break;
                    case 7:
                        monster = new WaterSerpent();
                        break;
                    case 8:
                        monster = new Drangon();
                        break;
                }

                battleMonsters[i] = monster;
            }

            return battleMonsters;
        }
    }
}
