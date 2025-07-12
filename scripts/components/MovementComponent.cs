namespace Godot.Game.HSFMS.Components;

[GlobalClass]
public partial class MovementComponent : Component
{
    private int _movement = 1;
    [Export(PropertyHint.Range, "1,10,1")]
    public int Movement
    {
        get => _movement;
        set
        {
            _movement = Mathf.Clamp(value, 1, 10);
            GD.Print("Movement set to " + _movement);
        }
    }
}