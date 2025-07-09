namespace Godot.Game.HSFMS;

[GlobalClass]
public partial class MovableCellHandler : Node2D
{
    private int _rows = 1;
    private int _columns = 1;

    private MovableCell[,] _movableCells;

    [Export]
    public int Rows
    {
        get { return _rows; }
        set
        {
            if (value < 1)
            {
                value = 1;
            }
            FreeMovableCells();
            _rows = value;
            GD.Print(value + " rows set.");
            GenerateMovableCells();
        }
    }

    [Export]
    public int Columns
    {
        get { return _columns; }
        set
        {
            if (value < 1)
            {
                value = 1;
            }
            FreeMovableCells();
            _columns = value;
            GD.Print(value + " columns set.");
            GenerateMovableCells();
        }
    }

    private void GenerateMovableCells()
    {
        _movableCells = new MovableCell[Columns, Rows];
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                PackedScene movableCellScene = GD.Load<PackedScene>("res://scenes/movable_cell.tscn");
                MovableCell movableCell = (MovableCell)movableCellScene.Instantiate();
                movableCell.Index = (i, j);
                movableCell.Position = new((i * 64) + 32, (j * 64) + 32);
                _movableCells[i, j] = movableCell;
                GD.Print("Set " + i + " " + j);
                AddChild(movableCell);
            }
        }
    }

    private void FreeMovableCells()
    {
        if (_movableCells != null)
        {
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    GD.Print(i + " " + j);
                    _movableCells[i, j]?.QueueFree();
                }
            }
        }

    }

}
