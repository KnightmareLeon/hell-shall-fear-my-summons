using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Components;

[GlobalClass]
public partial class HealthComponent : Component
{

    [Export]
    private int _maxHealth = 100;
    [Export]
    private int _health = 100;
    public int Health
    {
        get { return _health; }
        set
        {
            if (value < 0)
            {
                _health = 0;
            }
            if (value > _maxHealth)
            {
                _health = _maxHealth;
            }
        }
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