using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Components;

public partial class DamageComponent : Component
{
    private int _damage = 1;
    [Export]
    public DamageType AttackType { get; set; }
    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int Damage
    {
        get => _damage;
        set => _damage = Mathf.Clamp(value, 1, int.MaxValue);
    }

    public void DealDamage()
    {
        
    }
}