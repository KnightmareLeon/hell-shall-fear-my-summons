using Godot.Collections;

namespace Godot.Game.HSFMS;

[GlobalClass][Tool]
public partial class Hit : GodotObject
{
    [Export]
    public Array<Damage> DamageArray { get; set; }
}
