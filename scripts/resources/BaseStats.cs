using System.Linq;
using System.Runtime.CompilerServices;
using Godot.Collections;
using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Resources;

[GlobalClass] [Tool]
public partial class BaseStats : Resource
{
    private int _maxHealth = 100;
    [Export]
    private Dictionary<DamageType, int> _resistances = [];
    [Export]
    private Dictionary<DamageType, bool> _immunities = [];
    [Export(PropertyHint.Range, "1,100,1,or_greater")]
    public int MaxHealth
    {
        get => _maxHealth;
        private set => _maxHealth = Mathf.Clamp(value, 1, int.MaxValue);
    }
    [Export]
    public double CriticalRate { get; set; } = 0.1;
    [Export]
    public double CriticalDamageRatio { get; set; } = 0.5;
    public int GetResistance(DamageType damageType)
    {
        return _resistances.TryGetValue(damageType, out int value) ? value : 0;
    }

    public bool GetImmunity(DamageType damageType)
    {
        return _immunities.TryGetValue(damageType, out bool immune) && immune;
    }
}
