namespace Godot.Game.HSFMS.Skills;

[GlobalClass]  [Tool]
public abstract partial class BaseSkill : Node
{
    [Export]
    private string _effectsDescription;
    [Export]
    private string _flavorText;
    [Export]
    public string Icon;
}