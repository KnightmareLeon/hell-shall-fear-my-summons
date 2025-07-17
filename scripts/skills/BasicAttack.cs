using Godot.Game.HSFMS.Components;

namespace Godot.Game.HSFMS.Skills;

[GlobalClass] [Tool]
public partial class BasicAttack : ActiveSkill
{
    public override string[] _GetConfigurationWarnings()
    {
        foreach (Node child in GetChildren())
        {
            if (child is DamageComponent)
            {
                return [];
            }
        }
        return ["Basic Attack Skill needs one DamageComponent."];
    }

    public override void _Ready()
    {
        Connect("child_entered_tree", new Callable(this, nameof(OnChildEnteredTree)));
        Connect("child_exiting_tree", new Callable(this, nameof(OnChildExitingTree)));
    }

    private void OnChildEnteredTree(Node child)
    {
        UpdateConfigurationWarnings();
    }

    private void OnChildExitingTree(Node child)
    {
        UpdateConfigurationWarnings();
    }

}
