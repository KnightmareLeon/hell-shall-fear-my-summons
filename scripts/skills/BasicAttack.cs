namespace Godot.Game.HSFMS.Skills;

[Tool]
[GlobalClass]
public partial class BasicAttack : ActiveSkill
{
    public override string[] _GetConfigurationWarnings()
    {
        Script damageComponentScript = GD.Load<Script>("res://scripts/components/DamageComponent.cs");
        foreach (Node child in GetChildren())
        {
            if (child is not null && (Script)child.GetScript() == damageComponentScript)
            {
                return [];
            }
        }
        return ["Basic Attack Skill needs one DamageComponent"];
    }

    public override void _Ready()
    {
        if (!IsConnected("child_entered_tree", new Callable(this, nameof(OnChildEnteredTree))))
        {
            Connect("child_entered_tree", new Callable(this, nameof(OnChildEnteredTree)));
        }
        if (!IsConnected("child_exiting_tree", new Callable(this, nameof(OnChildExitingTree))))
        {
            Connect("child_exiting_tree", new Callable(this, nameof(OnChildExitingTree)));
        }
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
