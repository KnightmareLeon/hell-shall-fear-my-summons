namespace Godot.Game.HSFMS.Nightborne;

public partial class Idle : NightborneState
{
    [Export]
    private AnimatedSprite2D _animatedSprite2D;

    public override void Enter()
    {
        base.Enter();
        _animatedSprite2D.Play("idle");
    }

}
