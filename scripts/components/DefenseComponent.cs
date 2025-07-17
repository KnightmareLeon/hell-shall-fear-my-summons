using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Components;

[GlobalClass, Icon("res://assets/icons/icon_shield.png")]  [Tool]
public partial class DefenseComponent : Component
{
    [ExportGroup("Defenses")]
    [Export]
    public int PhysicalDefense { get; set; } = 0;
    [Export]
    public int MagicalResistance { get; set; } = 0;
    [Export]
    public int HeatResistance { get; set; } = 0;
    [Export]
    public int ColdResistance { get; set; } = 0;
    [Export]
    public int CorrosiveResistance { get; set; } = 0;

    [ExportGroup("Immunities")]
    [Export]
    public bool PhysicalImmune = false;
    [Export]
    public bool MagicalImmune = false;
    [Export]
    public bool HeatImmune = false;
    [Export]
    public bool ColdImmune = false;
    [Export]
    public bool CorrosiveImmune = false;

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
            case DamageType.HEAT:
                defense = HeatResistance;
                immuneMultiplier = HeatImmune ? 0 : 1;
                break;
            case DamageType.COLD:
                defense = ColdResistance;
                immuneMultiplier = ColdImmune ? 0 : 1;
                break;
            case DamageType.CORROSIVE:
                defense = CorrosiveResistance;
                immuneMultiplier = CorrosiveImmune ? 0 : 1;
                break;
            case DamageType.PURE:
            default:
                break;

        }

        return (int)(damage * (1 - 0.06 * (defense - pierce))) * immuneMultiplier;
    }

}