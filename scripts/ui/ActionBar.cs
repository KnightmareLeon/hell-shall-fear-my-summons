namespace Godot.Game.HSFMS;

[GlobalClass] [Tool]
public partial class ActionBar : PanelContainer
{
    private HBoxContainer _hBoxContainer;

    public override void _Ready()
    {
        _hBoxContainer = GetNode<HBoxContainer>("HBoxContainer");
    }

    public void GetSkillButtons(SkillButton[] skills)
    {
        foreach (SkillButton skill in skills)
        {
            _hBoxContainer.AddChild(skill);
        }
    }

    public void RemoveSkillButtons()
    {
        foreach (Node child in _hBoxContainer.GetChildren())
        {
            if (child is SkillButton)
            {
                _hBoxContainer.RemoveChild(child);
            }
        }
    }

}
