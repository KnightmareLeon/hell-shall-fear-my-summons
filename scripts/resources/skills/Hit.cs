using Godot.Collections;

namespace Godot.Game.HSFMS.Resources;

[GlobalClass]
[Tool]
public partial class Hit : Resource
{
    [Export]
    private Array<Damage> _damageArray;

    public Array<Damage> DamageModifierArray { get; set; }

    public Array<Damage> GetDamage()
    {
        return _damageArray;
    }
}
