namespace Engine;

public class Monster : LivingCreature
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int MaximumDamage { get; set; }
    public int RewardExperiencePoints { get; set; }
    public int RewardGold { get; set; }
    public List<LootItem> LootTable { get; set; } = [];

    public Monster(int id, string name, int maximumDamage, int rewardExperience, int rewardGold,
        int currentHitPoints, int maximumHitPoints, int level, int strength, int dexterity,
        int intelligent, int currentMana, int maximumMana, Race race, Armour? armourUsed = null)
        : base(currentHitPoints, maximumHitPoints, level, strength, dexterity, intelligent,
               currentMana, maximumMana, race, armourUsed)
    {
        ID = id;
        Name = name;
        MaximumDamage = maximumDamage;
        RewardExperiencePoints = rewardExperience;
        RewardGold = rewardGold;
    }
}
