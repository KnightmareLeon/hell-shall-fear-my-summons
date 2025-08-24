using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS;

[GlobalClass][Tool]
public partial class PlayerSkillSelectionState : BattleControllerState
{
    [Export]
    private PlayerUnitSelectionState _playerUnitSelectionState;
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
            default:
                return null;
        }
    }

}
