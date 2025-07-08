namespace Godot.Game.HSFMS.Components;

[GlobalClass]
public partial class ClickableComponent : Component
{
    private CharacterBody2D _characterBody2D;
    public override void _Ready()
    {
        if (GetParent() is CharacterBody2D cb2d)
        {
            _characterBody2D = cb2d;
            _characterBody2D.InputPickable = true;
            _characterBody2D.Connect("input_event", new Callable(this, nameof(OnCharacterInputEvent)));
        }
    }

    public void OnCharacterInputEvent(Node viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
            {
                GD.Print(_characterBody2D.Name + " clicked!");
            }
        }
    }

} 
