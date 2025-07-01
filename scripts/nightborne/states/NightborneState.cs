namespace Godot.Game.HSFMS;

[GlobalClass]
public partial class NightborneState : State
{
    protected Nightborne nightborne;

    public override void Enter()
    {
        nightborne = (Nightborne)Parent;
    }
}