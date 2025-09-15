namespace Godot.Game.HSFMS;

[GlobalClass]
[Tool]
public partial class ActionBar : PanelContainer
{
    private HBoxContainer _hBoxContainer;

    [Export]
    private Button _cancelButton;

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

    public void RemoveSkillButtons(Callable onGettingActiveSkillCallable)
    {
        foreach (Node child in _hBoxContainer.GetChildren())
        {
            if (child is SkillButton skillButton)
            {
                _hBoxContainer.RemoveChild(skillButton);
                skillButton.Disconnect(nameof(skillButton.SendActiveSkill), onGettingActiveSkillCallable);
            }
        }
    }

    public void ShowCancelButton()
    {
        _cancelButton.Visible = true;
    }

}
