using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Components;

[GlobalClass]
public partial class DefenseComponent : Component
{

    [Export]
    public int PhysicalDefense { get; set; } = 0;
    [Export]
    public int MagicalResistance { get; set; } = 0;
    [Export]
    public bool PhysicalImmune = false;
    [Export]
    public bool MagicalImmune = false;

    public int ReduceDamage(int damage, DamageType attackType, int pierce)
    {
        int defense = 0;
        int immuneMultiplier = 1;
        switch (attackType)
        {
            case DamageType.PHYSICAL:
                defense = PhysicalDefense;
                immuneMultiplier = PhysicalImmune ? 0 : 1;
                break;
            case DamageType.MAGICAL:
                defense = MagicalResistance;
                immuneMultiplier = MagicalImmune ? 0 : 1;
                break;
            case DamageType.PURE:
            default:
                break;

        }

        return (int)(damage * (1 - 0.06 * (defense - pierce))) * immuneMultiplier;
    }

}