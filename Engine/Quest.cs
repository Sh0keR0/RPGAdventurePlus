namespace Engine;

public class Quest
{
    public int ID { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int RewardExperiencePoints { get; init; }
    public int RewardGold { get; init; }
    public Item? RewardItem { get; set; }
    public List<QuestCompletionItem> QuestCompletionItems { get; } = [];

    public Quest(int id, string name, string description, int rewardExperiencePoints, int rewardGold, Item? rewardItem = null)
    {
        ID = id;
        Name = name;
        Description = description;
        RewardExperiencePoints = rewardExperiencePoints;
        RewardGold = rewardGold;
        RewardItem = rewardItem;
    }
}
