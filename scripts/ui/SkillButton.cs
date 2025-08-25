using Godot.Game.HSFMS.Skills;

namespace Godot.Game.HSFMS;

[GlobalClass]
public partial class SkillButton : Button
{
    [Signal]
    public delegate void ButtonPressedEventHandler();

    public ActiveSkill ActiveSkill { get; set; }
    public override void _Ready()
    {
        Connect("pressed", new Callable(this, nameof(OnButtonPressed)));
    }


    public void OnButtonPressed()
    {

    }
}
