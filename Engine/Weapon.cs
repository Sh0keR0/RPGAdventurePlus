namespace Engine;

public record Weapon(int ID, string Name, string NamePlural, int MinimumDamage, int MaximumDamage)
    : Item(ID, Name, NamePlural);
