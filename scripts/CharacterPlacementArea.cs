namespace Godot.Game.HSFMS;

[GlobalClass] [Tool]
public partial class CharacterPlacementArea : Area2D
{
    [Signal]
    public delegate void SendSelectedAreaEventHandler(CharacterBody2D character, CharacterPlacementArea characterPlacementArea);

    private AnimatedSprite2D _animatedSprite2D;

    private CharacterBody2D _character;
    public override void _Ready()
    {
        _animatedSprite2D = (AnimatedSprite2D)GetNode("AnimatedSprite2D");
        Connect("input_event", new Callable(this, nameof(OnAreaInputEvent)));
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
    }

    public void Select()
    {
        _animatedSprite2D.Visible = true;
        _animatedSprite2D.Play("selected");
    }

    public void Unselect()
    {
        _animatedSprite2D.Visible = false;
        _animatedSprite2D.Stop();
    }

    public void OnBodyEntered(Node2D body)
    {
        _character = (CharacterBody2D)body;
    }

    public void OnAreaInputEvent(Node viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
            {
                GD.Print(Name + " selected!");
                if (_character != null)
                {
                    EmitSignal(nameof(SendSelectedArea), _character, this);
                }
            }
        }
    }
}
