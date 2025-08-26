using Godot.Game.HSFMS.Skills;

namespace Godot.Game.HSFMS;

[GlobalClass]
public partial class SkillButton : Button
{

    public ActiveSkill ActiveSkill { get; set; }
}
