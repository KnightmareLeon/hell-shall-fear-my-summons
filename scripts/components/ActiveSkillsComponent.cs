namespace Godot.Game.HSFMS.Components;

[GlobalClass]
public partial class ActiveSkillsComponent : Component
{
    private int _maxSkillPoints;

    public override void _Ready()
    {
        base._Ready();
    }
    private int _currentSkillPoints;
    [Export(PropertyHint.Range, "1,3,1, or_greater")]
    public int MaxSkillPoints
    {
        get => _maxSkillPoints; 
        set => _maxSkillPoints = Mathf.Clamp(value, 1, int.MaxValue);

    }

    [Export(PropertyHint.Range, "1,100,1, or_greater")]
    public int CurrentSkillPoints
    {
        get => _currentSkillPoints; 
        set => _currentSkillPoints = Mathf.Clamp(value, 1, _maxSkillPoints);
    }
}