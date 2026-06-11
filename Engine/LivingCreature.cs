namespace Engine;

public class LivingCreature : INotifyPropertyChanged
{
    private int _currentHitPoints, _level, _currentMana;

    public int CurrentHitPoints
    {
        get => _currentHitPoints;
        set { _currentHitPoints = value; OnPropertyChanged(nameof(CurrentHitPoints)); }
    }

    public int MaximumHitPoints { get; set; }

    public int Level
    {
        get => _level;
        set { _level = value; OnPropertyChanged(nameof(Level)); }
    }

    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Intelligent { get; set; }

    public int CurrentMana
    {
        get => _currentMana;
        set { _currentMana = value; OnPropertyChanged(nameof(CurrentMana)); }
    }

    public int MaximumMana { get; set; }
    public Armour? ArmourUsed { get; set; }
    public List<SpellList> Spells { get; set; } = [];
    public List<BuffsList> Buffs { get; set; } = [];
    public int TemporaryMaximumMana { get; set; }
    public Race CreatureRace { get; set; } = null!;

    public LivingCreature(int currentHitPoints, int maximumHitPoints, int level, int strength,
        int dexterity, int intelligent, int currentMana, int maximumMana, Race race, Armour? armourUsed = null)
    {
        CurrentHitPoints = currentHitPoints;
        MaximumHitPoints = maximumHitPoints;
        Level = level;
        Strength = strength;
        Dexterity = dexterity;
        Intelligent = intelligent;
        CurrentMana = currentMana;
        MaximumMana = maximumMana;
        ArmourUsed = armourUsed;
        CreatureRace = race;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public void DealDamage(int amount) => CurrentHitPoints -= amount;

    public bool HasSpell(Spell spell) => Spells.Any(sp => sp.Details.ID == spell.ID);

    public void AddSpell(Spell spell)
    {
        if (!HasSpell(spell))
            Spells.Add(new SpellList(spell));
        OnPropertyChanged(nameof(Spells));
    }

    public void RemoveSpell(Spell spell)
    {
        var sp = Spells.FirstOrDefault(s => s.Details.ID == spell.ID);
        if (sp is not null)
        {
            Spells.Remove(sp);
            OnPropertyChanged(nameof(Spells));
        }
    }

    public void DrainMana(int amount) => CurrentMana -= amount;

    public void RestoreManaToFull() => CurrentMana = MaximumMana;

    public void RestoreMana(int amount) =>
        CurrentMana = Math.Min(CurrentMana + amount, MaximumMana);

    public void RemoveBuff(Buff buff)
    {
        var bl = Buffs.FirstOrDefault(b => b.Details.ID == buff.ID);
        if (bl is not null)
            Buffs.Remove(bl);
    }

    public void RemoveAllBuffs() => Buffs.Clear();
}
