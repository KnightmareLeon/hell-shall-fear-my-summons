namespace Godot.Game.HSFMS;

public partial class Idle : NightborneState
{
    [Export]
    private AnimatedSprite2D animatedSprite2D;

    public override void Enter()
    {
        base.Enter();
        animatedSprite2D.Play("idle");
    }

}
