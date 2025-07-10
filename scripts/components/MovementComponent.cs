namespace Godot.Game.HSFMS.Components;

[GlobalClass]
public partial class MovementComponent : Component
{
    [Export]
    public int Movement { get; set; } = 1;
}