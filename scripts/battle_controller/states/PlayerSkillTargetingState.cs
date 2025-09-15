using Godot.Game.HSFMS.Skills;
using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS;

[GlobalClass][Tool]
public partial class PlayerSkillTargetingState : BattleControllerState
{
    [Export]
    private PlayerSkillSelectionState _playerSkillSelectionState;
    public override void Enter()
    {
        base.Enter();
        BattleController.HighlighTargets(isPlayer: true);
    }

    public override State ProcessSignal(SignalType signalType, params Variant[] args)
    {
        switch (signalType)
        {
            case SignalType.ON_GETTING_SELECTED_AREA:
                return null;
            case SignalType.ON_GETTING_SELECTED_SKILL:
                BattleController.SetSelectedActiveSkill((ActiveSkill)args[0]);
                BattleController.HighlighTargets(isPlayer: true);
                return null;
            case SignalType.ON_CANCEL_BUTTON_PRESSED:
                return _playerSkillSelectionState;
            default:
                return null;
        }
    }

}
