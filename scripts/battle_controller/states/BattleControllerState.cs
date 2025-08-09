namespace Godot.Game.HSFMS;

[GlobalClass]
public partial class BattleControllerState : State
{
    protected BattleController BattleController;
    public override void Enter()
    {
        BattleController = (BattleController)Parent;
    }

}
