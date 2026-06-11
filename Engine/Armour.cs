namespace Engine;

public record Armour(int ID, string Name, string NamePlural, int Resistance, int MagicResistance)
    : Item(ID, Name, NamePlural);
