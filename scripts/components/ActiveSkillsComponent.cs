using Godot.Collections;
using Godot.Game.HSFMS.Skills;

namespace Godot.Game.HSFMS.Components;

[GlobalClass]
[Tool]
public partial class ActiveSkillsComponent : Component
{
    private int _maxSkillPoints;

    private int _currentSkillPoints;
    [Export(PropertyHint.Range, "1,3,1, or_greater")]
    public int MaxSkillPoints
    {
        get => _maxSkillPoints;
        set => _maxSkillPoints = Mathf.Clamp(value, 1, int.MaxValue);

    }
    public int CurrentSkillPoints
    {
        get => _currentSkillPoints;
        set => _currentSkillPoints = Mathf.Clamp(value, 1, _maxSkillPoints);
    }

    public Button[] SkillButtons { get; private set; }


    public override void _Ready()
    {
        LoadSkillButtons();
    }

    public void LoadSkillButtons()
    {
        SkillButtons = new Button[GetChildCount()];
        Array<Node> children = GetChildren();
        for (int i = 0; i < GetChildCount(); i++)
        {
            if (children[i] is ActiveSkill activeSkill)
            {
                Button skillButton = new()
                {
                    Icon = activeSkill.Icon
                };
                SkillButtons[i] = skillButton;
            }
        }
    }
}