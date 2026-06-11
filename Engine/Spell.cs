namespace Engine;

public record Spell(int ID, string Name, int ManaCost, int EffectID, int EffectAmount, bool CombatSpell);
