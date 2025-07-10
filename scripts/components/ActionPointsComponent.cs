namespace Godot.Game.HSFMS.Components;

[GlobalClass]
public partial class ActionPointsComponent : Component
{
    private int _maxActionPoints;

    private int _actionPoints;
    [Export]
    public int MaxActionPoints
    {
        get { return _maxActionPoints; }
        set
        {
            if (value < 1)
            {
                value = 1;
            }

            _maxActionPoints = value;
            GD.Print(_maxActionPoints + " maximum actions points set.");
        }
    }

    [Export]
    public int ActionPoints
    {
        get { return _actionPoints; }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            if (value > _maxActionPoints)
            {
                value = _maxActionPoints;
            }

            _actionPoints = value;
            GD.Print(_actionPoints + " actions points set.");
        }
    }
}