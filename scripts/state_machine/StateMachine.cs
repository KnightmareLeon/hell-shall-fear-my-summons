namespace Godot.Game.HSFMS;

[GlobalClass]
public partial class StateMachine : Node
{

    [Export]
    public State InitialState { get; set; }
    public State CurrentState { get; set; }
    public override void _Ready()
    {
        foreach (Node child in GetChildren())
        {
            if (child is State state)
            {
                state.Parent = GetParent();
            }
        }

        ChangeState(InitialState);
    }

    public void ChangeState(State state)
    {
        state?.Exit();

        CurrentState = state ?? CurrentState;

        CurrentState.Enter();
        
    }

    public void ProcessFrame(double delta) { ChangeState(CurrentState.ProcessFrame(delta)); }

    public void ProcessPhysics(double delta) { ChangeState(CurrentState.ProcessPhysics(delta)); }

    public void ProcessInput(InputEvent @event) { ChangeState(CurrentState.ProcessInput(@event)); }

    public void ProcessSignal(string signalName, params Variant[] args) { ChangeState(CurrentState.ProcessSignal(signalName, args)); }
}
