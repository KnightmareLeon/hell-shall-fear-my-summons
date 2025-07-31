namespace Godot.Game.HSFMS;

public partial class ActionBar : PanelContainer
{
    private HBoxContainer _hBoxContainer;

    public override void _Ready()
    {
        _hBoxContainer = GetNode<HBoxContainer>("HBoxContainer");
    }

    public void GetActionButtons(Button[] actions)
    {
        foreach (Button action in actions)
        {
            _hBoxContainer.AddChild(action);
        }
    }

    public void RemoveActionButtons()
    {
        foreach (Node child in _hBoxContainer.GetChildren())
        {
            if (child is Button)
            {
                _hBoxContainer.RemoveChild(child);
            }
        }
    }

}
