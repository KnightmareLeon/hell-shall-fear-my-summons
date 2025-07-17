using Godot.Game.HSFMS.Components;
using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Skills;

[GlobalClass]  [Tool]
public partial class ActiveSkill : BaseSkill
{
    private int _cooldown = 1;
    private int _currentCooldown = 0;
    private int _skillCost = 0;

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
    
    [Export(PropertyHint.Range, "0,10,1,or_greater")]
    public int SkillCost
    {
        get => _skillCost;
        set => _skillCost = Mathf.Clamp(value,0,int.MaxValue);
    }
    [Export]
    public string AnimationName { get; set; }
    [Signal]
    public delegate void DamageEventHandler(int damage, DamageType damageType);
    public virtual void DoSkill()
    {

    }

    public void DealDamage()
    {
        Script damageScript = GD.Load<Script>("res://scripts/components/DamageComponent.cs");
        foreach (Node child in GetChildren())
        {
            if ((Script)child.GetScript() == damageScript)
            {
                DamageComponent damageComponent = (DamageComponent)child;
                EmitSignal(nameof(Damage), damageComponent.Damage, Variant.From(damageComponent.DamageType));
            }
        }
    }
}