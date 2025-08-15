namespace Godot.Game.HSFMS;

[GlobalClass] [Tool]
public partial class CharacterPlacementArea : Area2D
{
    [Signal]
    public delegate void SendSelectedAreaEventHandler(CharacterBody2D character, CharacterPlacementArea characterPlacementArea);

    private AnimatedSprite2D _selectionAnimation;

    private CharacterBody2D _character;

    public (int, int) Index { get; set; }
    public override void _Ready()
    {
        _selectionAnimation = (AnimatedSprite2D)GetNode("SelectionAnimation");
        Connect("input_event", new Callable(this, nameof(OnAreaInputEvent)));
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
    }

    public void Select()
    {
        _selectionAnimation.Visible = true;
        _selectionAnimation.Play("selected");
    }

    public void Unselect()
    {
        _selectionAnimation.Visible = false;
        _selectionAnimation.Stop();
    }

    public void EnemyTargetHighlight()
    {

    }

    public void AllyTargetHighlight()
    {
        
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
