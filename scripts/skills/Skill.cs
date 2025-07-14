using Godot.Game.HSFMS.Components;
using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Skills;

[GlobalClass]
public partial class Skill : Node
{
    [Signal]
    public delegate void DamageEventHandler(int damage, DamageType damageType);
    [Export]
    public string AnimationName { get; set; }
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