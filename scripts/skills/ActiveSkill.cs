using System;
using Godot.Game.HSFMS.Components;
using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Skills;

[GlobalClass]
public partial class ActiveSkill : Node
{
    private int _cooldown = 1;
    private int _currentCooldown = 0;
    [Signal]
    public delegate void DamageEventHandler(int damage, DamageType damageType);
    [Export]
    public string AnimationName { get; set; }
    [Export(PropertyHint.Range, "0,10,1,or_greater")]
    public int Cooldown
    {
        get => _cooldown;
        set => _cooldown = Mathf.Clamp(value, 0, int.MaxValue);
    }

    [Export(PropertyHint.Range, "0,10,1,or_greater")]
    public int CurrentCooldown
    {
        get => _currentCooldown;
        set => _currentCooldown = Mathf.Clamp(value, 0, Cooldown);
    }
    
    [Export]
    public int SkillCost { get; set; } = 1;
    public virtual void DoSkill()
    {

    }

    public void DealDamage()
    {
        foreach (Node child in GetChildren())
        {
            if (child is DamageComponent damageComponent)
            {
                EmitSignal(nameof(Damage), damageComponent.Damage, Variant.From(damageComponent.DamageType));
            }
        }
    }
}