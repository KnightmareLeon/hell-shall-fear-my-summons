using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Components;

[GlobalClass]
public partial class HealthComponent : Component
{

    [Export]
    private int _maxHealth = 100;
    private int _health = 100;

    [Export(PropertyHint.Range, "1,100,1,or_greater")]
    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = Mathf.Clamp(value, 1, int.MaxValue);
            GD.Print("Max Health set to " + _maxHealth);
        }
    }

    [Export(PropertyHint.Range, "0,100,1,or_greater")]
    public int Health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, _maxHealth);
    }

    [Export]
    private DefenseComponent _defenseComponent;

    [Signal]
    public delegate void ZeroHealthEventHandler();

    public void TakeDamage(int damage, DamageType damageType, int pierce = 0)
    {
        if (_defenseComponent != null)
        {
            damage = _defenseComponent.ReduceDamage(damage, damageType, pierce);
        }

        Health -= damage;

        if (Health == 0)
        {
            EmitSignal(nameof(ZeroHealthEventHandler));
        }
    }

    public void Heal(int heal)
    {
        Health += heal;
    }
}