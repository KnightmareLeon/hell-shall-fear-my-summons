using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS;

[GlobalClass][Tool]
public partial class Damage : GodotObject
{
    private int _baseDamage = 1;
    private int _pierce = 0;
    [Export]
    public DamageType DamageType { get; private set; }
    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int BaseDamage
    {
        get => _baseDamage;
        private set => _baseDamage = Mathf.Clamp(value, 1, int.MaxValue);
    }
    [Export(PropertyHint.Range, "0,10,1,or_greater")]
    public int Pierce
    {
        get => _pierce;
        private set => _pierce = Mathf.Clamp(value, 0, int.MaxValue);
    }
    [Export]
    public bool IsPercentage { get; private set; } = false;
    [Export]
    public bool DoesLifesteal { get; private set; } = false;
}
