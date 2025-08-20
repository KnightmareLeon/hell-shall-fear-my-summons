namespace Godot.Game.HSFMS;

public partial class SkillButton : Button
{

    public override void _Ready()
    {
        Connect("pressed",new Callable(this, nameof(OnButtonPressed)));
    }


    public void OnButtonPressed()
    {

    }
}
