namespace Godot.Game.HSFMS.Components;

[GlobalClass]
public partial class ActionPointsComponent : Component
{
    private int _maxActionPoints;

    private int _actionPoints;
    [Export(PropertyHint.Range, "1,3,1, or_greater")]
    public int MaxActionPoints
    {
        get => _maxActionPoints; 
        set
        {
            _maxActionPoints = Mathf.Clamp(value, 1, int.MaxValue);
            GD.Print(_maxActionPoints + " maximum actions points set.");
        }
    }

    [Export(PropertyHint.Range, "1,100,1, or_greater")]
    public int ActionPoints
    {
        get { return _actionPoints; }
        set
        {
            _actionPoints = Mathf.Clamp(value, 1, _maxActionPoints);
            GD.Print(_actionPoints + " actions points set.");
        }
    }
}