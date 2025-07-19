using System;
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
        set
        {
            int maxValue = int.MaxValue;
            if (_baseStats != null)
            {
                maxValue = _baseStats.MaxHealth;
            }
            _currentHealth = Mathf.Clamp(value, 1, maxValue);
        }
    }
    public Collections.Dictionary<DamageType, int> ResistanceModifiers = [];
    public Collections.Dictionary<DamageType, bool> ImmunityModifiers = [];
    public override void _Ready()
    {

    }

    public override string[] _GetConfigurationWarnings()
    {
        if (Stats == null)
        {
            return ["Character needs custom Base Stats resource."];
        }
        return [];
    }

}
