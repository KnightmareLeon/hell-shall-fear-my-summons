
using Godot.Collections;
using Godot.Game.HSFMS.Resources;

namespace Godot.Game.HSFMS.Skills;

[GlobalClass]  [Tool]
public partial class ActiveSkill : BaseSkill
{
    private int _cooldown = 1;
    private int _currentCooldown = 0;
    private int _skillCost = 0;
    [Export]
    public Array<Hit> HitArray;

    [Export(PropertyHint.Range, "0,10,1,or_greater")]
    public int Cooldown
    {
        get => _cooldown;
        set => _cooldown = Mathf.Clamp(value, 0, int.MaxValue);
    }
    public int CurrentCooldown
    {
        get => _currentCooldown;
        set => _currentCooldown = Mathf.Clamp(value, 0, Cooldown);
    }
    
    [Export(PropertyHint.Range, "0,10,1,or_greater")]
    public int SkillCost
    {
        get => _skillCost;
        set => _skillCost = Mathf.Clamp(value,0,int.MaxValue);
    }
    [Export]
    public string AnimationName { get; set; }

    public virtual void DoSkill()
    {

    }

    public void DealDamage()
    {

    }
}