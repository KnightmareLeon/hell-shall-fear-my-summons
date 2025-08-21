namespace Godot.Game.HSFMS;

public partial class SkillButton : Button
{
    [Signal]
    public delegate void ButtonPressedEventHandler();
    public override void _Ready()
    {
        Connect("pressed",new Callable(this, nameof(OnButtonPressed)));
    }


    public void OnButtonPressed()
    {

    }
}
