using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS;

[Tool]

public partial class PlayerSkillSelectionState : BattleControllerState
{
    [Export]
    PlayerSkillTargetingState _playerSkillTargetingState;
    public override State ProcessSignal(SignalType signalType, params Variant[] args)
    {
        switch (signalType)
        {
            case SignalType.ON_GETTING_SELECTED_AREA:

                BattleController.SetSelectedArea((CharacterBody2D)args[0], (CharacterPlacementArea)args[1]);
                return _playerSkillTargetingState;
            default:
                return null;
        }
    }

}
