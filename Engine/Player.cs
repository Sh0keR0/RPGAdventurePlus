namespace Engine;

public class Player : LivingCreature
{
    private int _gold, _experiencePoints;

    public int Gold
    {
        get => _gold;
        set { _gold = value; OnPropertyChanged(nameof(Gold)); }
    }

    public int ExperiencePoints
    {
        get => _experiencePoints;
        set { _experiencePoints = value; OnPropertyChanged(nameof(ExperiencePoints)); }
    }

    public BindingList<InventoryItem> Inventory { get; set; } = new();
    public BindingList<PlayerQuest> Quests { get; set; } = new();
    public Location? CurrentLocation { get; set; }
    public Weapon? CurrentWeapon { get; set; }
    public HealingPotion? CurrentPotion { get; set; }

    public List<Weapon> Weapons =>
        Inventory.Where(x => x.Details is Weapon).Select(x => (Weapon)x.Details).ToList();

    public List<HealingPotion> Potions =>
        Inventory.Where(x => x.Details is HealingPotion).Select(x => (HealingPotion)x.Details).ToList();

    public Player(int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints,
        int level, int strength, int dexterity, int intelligent, int currentMana, int maximumMana,
        Race race, Armour? armourUsed = null)
        : base(currentHitPoints, maximumHitPoints, level, strength, dexterity, intelligent,
               currentMana, maximumMana, race, armourUsed)
    {
        Gold = gold;
        ExperiencePoints = experiencePoints;
    }

    public static Player LoadFromXml(string xmlPlayerData)
    {
        try
        {
            var doc = XDocument.Parse(xmlPlayerData);
            var stats = doc.Root!.Element("Stats")!;

            int currentHP    = (int)stats.Element("CurrentHitPoints")!;
            int maxHP        = (int)stats.Element("MaximumHitPoints")!;
            int gold         = (int)stats.Element("Gold")!;
            int xp           = (int)stats.Element("ExperiencePoints")!;
            int level        = (int)stats.Element("Level")!;
            int strength     = (int)stats.Element("Strength")!;
            int dexterity    = (int)stats.Element("Dexterity")!;
            int intelligent  = (int)stats.Element("Intelligent")!;
            int currentMana  = (int)stats.Element("CurrentMana")!;
            int maxMana      = (int)stats.Element("MaximumMana")!;
            Race race        = World.RaceByID((int)stats.Element("Race")!)!;
            int armourId     = (int)stats.Element("ArmourUsed")!;
            Armour? armour   = armourId != 0 ? (Armour)World.ItemByID(armourId)! : null;

            var player = new Player(currentHP, maxHP, gold, xp, level, strength, dexterity,
                                    intelligent, currentMana, maxMana, race, armour);

            player.CurrentLocation = World.LocationByID((int)stats.Element("CurrentLocation")!);

            int weaponId = (int)stats.Element("CurrentWeapon")!;
            player.CurrentWeapon = weaponId != 0 ? (Weapon)World.ItemByID(weaponId)! : null;

            int potionId = (int)stats.Element("CurrentPotion")!;
            player.CurrentPotion = potionId != 0 ? (HealingPotion)World.ItemByID(potionId)! : null;

            foreach (var node in doc.Root.Element("InventoryItems")!.Elements("InventoryItem"))
                player.AddItem(World.ItemByID((int)node.Attribute("ID")!)!, (int)node.Attribute("Quantity")!);

            foreach (var node in doc.Root.Element("PlayerQuests")!.Elements("PlayerQuest"))
            {
                var quest = new PlayerQuest(World.QuestByID((int)node.Attribute("ID")!)!);
                quest.IsCompleted = (bool)node.Attribute("IsCompleted")!;
                player.Quests.Add(quest);
            }

            return player;
        }
        catch
        {
            return new Player(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, World.RaceByID(World.RACE_ID_HUNMAN)!);
        }
    }

    public string ToXmlString()
    {
        var doc = new XDocument(
            new XElement("Player",
                new XElement("Stats",
                    new XElement("CurrentHitPoints", CurrentHitPoints),
                    new XElement("MaximumHitPoints", MaximumHitPoints),
                    new XElement("Gold", Gold),
                    new XElement("ExperiencePoints", ExperiencePoints),
                    new XElement("CurrentLocation", CurrentLocation?.ID ?? 0),
                    new XElement("Level", Level),
                    new XElement("Strength", Strength),
                    new XElement("Dexterity", Dexterity),
                    new XElement("Intelligent", Intelligent),
                    new XElement("CurrentMana", CurrentMana),
                    new XElement("MaximumMana", MaximumMana),
                    new XElement("Race", CreatureRace.ID),
                    new XElement("ArmourUsed", ArmourUsed?.ID ?? 0),
                    new XElement("CurrentWeapon", CurrentWeapon?.ID ?? 0),
                    new XElement("CurrentPotion", CurrentPotion?.ID ?? 0)
                ),
                new XElement("InventoryItems",
                    Inventory.Select(item => new XElement("InventoryItem",
                        new XAttribute("ID", item.Details.ID),
                        new XAttribute("Quantity", item.Quantity)))
                ),
                new XElement("PlayerQuests",
                    Quests.Select(quest => new XElement("PlayerQuest",
                        new XAttribute("ID", quest.Details.ID),
                        new XAttribute("IsCompleted", quest.IsCompleted)))
                )
            )
        );

        return doc.ToString();
    }

    private void CallInventoryChangedEvent(Item? item)
    {
        if (item is Weapon)
            OnPropertyChanged(nameof(Weapons));
        if (item is HealingPotion)
            OnPropertyChanged(nameof(Potions));
    }

    public void AddItem(Item item, int quantity)
    {
        var existing = Inventory.SingleOrDefault(ii => ii.Details.ID == item.ID);
        if (existing is not null)
            existing.Quantity += quantity;
        else
            Inventory.Add(new InventoryItem(item, quantity));
        CallInventoryChangedEvent(item);
    }

    public bool HasItem(Item item, int quantity) =>
        Inventory.Any(ii => ii.Details.ID == item.ID);

    public void Heal(int health)
    {
        CurrentHitPoints += health;
        if (CurrentHitPoints > MaximumHitPoints)
            CurrentHitPoints = MaximumHitPoints;
    }

    public bool HasOngoingQuest(Quest quest) =>
        Quests.Any(ii => ii.Details.ID == quest.ID);

    public bool IsQuestCompleted(Quest quest) =>
        Quests.Any(pq => pq.IsCompleted && pq.Details.ID == quest.ID);

    public void RemoveItem(InventoryItem item, int quantity)
    {
        item.Quantity -= quantity;
        if (item.Quantity <= 0)
        {
            Inventory.Remove(item);
            CallInventoryChangedEvent(item.Details);
        }
    }

    public void LevelUp()
    {
        Level++;
        MaximumHitPoints += 10;
        Heal(MaximumHitPoints);
    }

    public void RewardGold(int amount) => Gold += amount;
}
