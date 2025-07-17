using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Components;

[GlobalClass, Icon("res://assets/icons/icon_sword.png")] [Tool]
public partial class DamageComponent : Component
{
    private int _baseDamage = 1;
    private int _pierce = 0;
    public int DamageFlatModifier { get; set; } = 0;
    public double DamagePercentageModifier { get; set; } = 0;
    public int Damage => (int)((_baseDamage + DamageFlatModifier) * (1 + DamagePercentageModifier));
    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int BaseDamage
    {
        get => _baseDamage;
        set => _baseDamage = Mathf.Clamp(value, 1, int.MaxValue);
    }
    [Export(PropertyHint.Range, "0,10,1,or_greater")]
    public int Pierce
    {
        get => _pierce;
        set => _pierce = Mathf.Clamp(value, 0, int.MaxValue);
    }

    [Export]
    public DamageType DamageType { get; set; } = DamageType.PHYSICAL;
}