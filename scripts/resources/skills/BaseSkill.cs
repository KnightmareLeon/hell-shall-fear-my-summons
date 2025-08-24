using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Skills;

[GlobalClass]  [Tool]
public abstract partial class BaseSkill : Resource
{
    [Export]
    public string EffectsDescription { get; protected set; }
    [Export]
    private string _flavorText;
    [Export]
    public TargetType TargetType { get; set; }
    [Export]
    public Texture2D Icon { get; set; }
}