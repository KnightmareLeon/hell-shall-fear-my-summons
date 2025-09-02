using Godot.Game.HSFMS.Skills;
using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS;

[GlobalClass][Tool]
public partial class PlayerSkillSelectionState : BattleControllerState
{
    [Export]
    private PlayerUnitSelectionState _playerUnitSelectionState;
    [Export]
    private PlayerSkillTargetingState _playerSkillTargetingState;
    public override State ProcessSignal(SignalType signalType, params Variant[] args)
    {
        switch (signalType)
        {
            case SignalType.ON_GETTING_SELECTED_AREA:
                if (BattleController.SetSelectedArea((CharacterBody2D)args[0], (UnitPlacementArea)args[1]))
                {
                    return null;
                }
                else
                {
                    return _playerUnitSelectionState;
                }
            case SignalType.ON_GETTING_SELECTED_SKILL:
                BattleController.SetSelectedActiveSkill((ActiveSkill) args[0]);
                return _playerSkillTargetingState;
            default:
                return null;
        }
    }

}
