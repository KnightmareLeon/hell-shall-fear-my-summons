using Godot.Game.HSFMS.Skills;
using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS;

[GlobalClass][Tool]
public partial class PlayerSkillTargetingState : BattleControllerState
{
    public override void Enter()
    {
        base.Enter();
        BattleController.HighlightEnemyTargets(isPlayer: true);
    }

    public override State ProcessSignal(SignalType signalType, params Variant[] args)
    {
        switch (signalType)
        {
            case SignalType.ON_GETTING_SELECTED_AREA:
                return null;
            case SignalType.ON_GETTING_SELECTED_SKILL:
                BattleController.SetSelectedActiveSkill((ActiveSkill)args[0]);
                return null;
            default:
                return null;
        }
    }

}
