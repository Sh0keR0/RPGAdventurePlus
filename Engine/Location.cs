namespace Engine;

public class Location
{
    public int ID { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public Item? ItemRequiredToEnter { get; init; }
    public Quest? QuestAvailableHere { get; set; }
    public Monster? MonsterLivingHere { get; set; }
    public Location? LocationToNorth { get; set; }
    public Location? LocationToWest { get; set; }
    public Location? LocationToSouth { get; set; }
    public Location? LocationToEast { get; set; }
    public int LevelRequirement { get; init; }

    public Location(int id, string name, string description, Item? itemRequiredToEnter = null,
        Quest? questAvailableHere = null, Monster? monsterLivingHere = null, int levelRequirement = 1)
    {
        ID = id;
        Name = name;
        Description = description;
        ItemRequiredToEnter = itemRequiredToEnter;
        QuestAvailableHere = questAvailableHere;
        MonsterLivingHere = monsterLivingHere;
        LevelRequirement = levelRequirement;
    }

    public bool HasRequirementToEnter() => ItemRequiredToEnter is not null;
    public bool HasQuestAvailable() => QuestAvailableHere is not null;
    public bool HasLivingMonster() => MonsterLivingHere is not null;
}
