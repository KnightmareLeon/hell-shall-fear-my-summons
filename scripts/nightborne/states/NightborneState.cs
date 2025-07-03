namespace Godot.Game.HSFMS.Nightborne;

[GlobalClass]
public partial class NightborneState : State
{
    protected Nightborne nightborne;

    public override void Enter()
    {
        nightborne = (Nightborne)Parent;
    }
}