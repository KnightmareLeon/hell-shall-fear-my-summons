using Godot;
using System;

public partial class CharacterPlacementArea : Area2D
{
    [Signal]
    public delegate void SendSelectedCharacterEventHandler(CharacterBody2D character);
    public override void _Ready()
    {
        Connect("input_event", new Callable(this, nameof(OnAreaInputEvent)));
    }

    public void OnAreaInputEvent(Node viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
            {
                GD.Print(Name + " clicked!");
            }
        }
    }
}
