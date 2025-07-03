using System;

namespace Godot.Game.HSFMS;

public partial class Idle : State
{
    [Export]
    private AnimatedSprite2D _animatedSprited2D;

    public override void Enter()
    {
        _animatedSprited2D.Play("idle");
    }
}
