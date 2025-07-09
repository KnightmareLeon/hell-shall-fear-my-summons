namespace Godot.Game.HSFMS;

public partial class MovableCell : Area2D
{
    private CollisionShape2D _collisionShape2d = new();
    private RectangleShape2D _rectangleShape2D = new();

    private Vector2 _cellSize  = new(64, 64);

    public (int, int) Index { get; set; }
    public Vector2 CellSize
    {
        get { return _cellSize; }
        set
        {
            _cellSize = value;
            _rectangleShape2D.Size = _cellSize;
        }
    }
    public override void _Ready()
    {
        _rectangleShape2D.Size = CellSize;
        _collisionShape2d.Shape = _rectangleShape2D;
        _collisionShape2d.Position = new(0, 0);
        AddChild(_collisionShape2d);
    }

    
}
