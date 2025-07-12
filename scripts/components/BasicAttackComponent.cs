using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Components;

[GlobalClass]
public partial class BasicAttackComponent : Component
{
    private DamageType _attackType;
    private int _range = 1;

    [Export]
    public DamageType AttackType { get; set; }
    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int Range
    {
        get => _range;
        set => _range = Mathf.Clamp(value, 1, int.MaxValue);
    }

}