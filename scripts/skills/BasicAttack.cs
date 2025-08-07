namespace Godot.Game.HSFMS.Skills;

[GlobalClass, Icon("res://assets/icons/icon_sword.png")] 
[Tool]
public partial class BasicAttack : ActiveSkill
{
    public override void _Ready()
    {
        EffectsDescription = "This unit's basic attack.";
        Cooldown = 0;
        SkillCost = 1;
        QuantityType = Types.QuantityType.SINGLE;
        TargetType = Types.TargetType.ENEMY;
        if (Icon == null)
        {
            Icon = ResourceLoader.Load<Texture2D>("res://assets/icons/game/basic_attack.png");
        }
    }

}
