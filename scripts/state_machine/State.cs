using System;

namespace Godot.Game.HSFMS;

[GlobalClass]
public partial class State : Node
{
    public Node Parent { get; set; }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual State ProcessFrame(double delta) => null;

    public virtual State ProcessPhysics(double delta) => null;

    public virtual State ProcessInput(InputEvent @event) => null;

    public virtual State ProcessSignal(string signalName, params Variant[] args) => null;

}