namespace Godot.Game.HSFMS.Component;

[GlobalClass]
public partial class HealthComponent : Node2D
{

    [Export]
    private int _maxHealth = 100;
    [Export]
    private int health = 100;
    public int Health
    {
        get { return health; }
        set
        {
            if (value < 0)
            {
                health = 0;
            }
            if (value > _maxHealth)
            {
                health = _maxHealth;
            }
        }
    }

    [Signal]
    public delegate void ZeroHealthEventHandler();

    public void TakeDamage(int damage)
    {
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