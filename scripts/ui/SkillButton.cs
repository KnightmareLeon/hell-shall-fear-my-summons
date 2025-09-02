using Godot.Game.HSFMS.Skills;

namespace Godot.Game.HSFMS;

[GlobalClass]
public partial class SkillButton : Button
{
    [Signal]
    public delegate void SendActiveSkillEventHandler(ActiveSkill activeSkill);
    public ActiveSkill ActiveSkill { get; set; }

    public override void _Ready()
    {
        Connect(BaseButton.SignalName.Pressed, new Callable(this, nameof(OnSkillButtonPressed)));
    }

    private void OnSkillButtonPressed()
    {
        EmitSignal(nameof(SendActiveSkill), ActiveSkill);
    }

}
