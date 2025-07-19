using System.Collections.Generic;
using System.Linq;
using Godot.Game.HSFMS.Resources;
using Godot.Game.HSFMS.Types;

namespace Godot.Game.HSFMS.Characters;

[GlobalClass]
[Tool]
public partial class Character : CharacterBody2D
{
    private int _currentHealth;

    private BaseStats _baseStats;
    [Export]
    public BaseStats Stats
    {
        get => _baseStats;
        set
        {
            _baseStats = value;
            UpdateConfigurationWarnings();
        }
    }
    public int CurrentHealth
    {
        get => _currentHealth;
        set => Mathf.Clamp(value, 1, _baseStats.MaxHealth);
    }
    public Collections.Dictionary<DamageType, int> ResistanceModifiers = [];
    public Collections.Dictionary<DamageType, bool> ImmunityModifiers = [];
    public override void _Ready()
    {
        if (Stats != null)
        {
            CurrentHealth = _baseStats.MaxHealth;
        }
    }

    public override string[] _GetConfigurationWarnings()
    {
        string[] warnings = [];
        if (Stats == null)
        {
            warnings = ["Character needs a BaseStats resource."];
        }
        return [.. base._GetConfigurationWarnings(), .. warnings];
    }

}
