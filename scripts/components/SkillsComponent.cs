namespace Godot.Game.HSFMS.Components;

[GlobalClass]
public partial class SkillsComponent : Component
{
    private int _maxSkillPoints;

    public override void _Ready()
    {
        base._Ready();
    }
    private int _actionPoints;
    [Export(PropertyHint.Range, "1,3,1, or_greater")]
    public int MaxSkillPoints
    {
        get => _maxSkillPoints; 
        set
        {
            _maxSkillPoints = Mathf.Clamp(value, 1, int.MaxValue);
            GD.Print(_maxSkillPoints + " maximum actions points set.");
        }
    }

    [Export(PropertyHint.Range, "1,100,1, or_greater")]
    public int ActionPoints
    {
        get { return _actionPoints; }
        set
        {
            _actionPoints = Mathf.Clamp(value, 1, _maxSkillPoints);
            GD.Print(_actionPoints + " actions points set.");
        }
    }
}