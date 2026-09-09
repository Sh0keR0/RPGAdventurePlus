namespace Engine;

public record HealingPotion(int ID, string Name, string NamePlural, int AmountToHeal)
    : Item(ID, Name, NamePlural);
