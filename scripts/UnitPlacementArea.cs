namespace Godot.Game.HSFMS;

[GlobalClass] [Tool]
public partial class UnitPlacementArea : Area2D
{
    [Signal]
    public delegate void SendSelectedAreaEventHandler(CharacterBody2D character, UnitPlacementArea characterPlacementArea);

    private AnimatedSprite2D _selectionAnimation;
    private AnimatedSprite2D _targetingAnimation;

    private CharacterBody2D _character;

    public (int, int) Index { get; set; }
    public override void _Ready()
    {
        _selectionAnimation = (AnimatedSprite2D)GetNode("SelectionAnimation");
        _targetingAnimation = (AnimatedSprite2D)GetNode("TargetingAnimation");
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
        _targetingAnimation.Visible = true;
        _targetingAnimation.Play("EnemyHighlighted");
    }

    public void AllyTargetHighlight()
    {
        _targetingAnimation.Visible = true;
        _targetingAnimation.Play("AllyHighlighted");
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
