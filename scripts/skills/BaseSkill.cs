namespace Godot.Game.HSFMS.Skills;

[GlobalClass]  [Tool]
public abstract partial class BaseSkill : Node
{
    [Export]
    public string EffectsDescription { get; protected set; }
    [Export]
    private string _flavorText;
    [Export]
    public Texture2D Icon { get; set; }
}