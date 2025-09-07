using System.Collections.Generic;
using Godot.Game.HSFMS.Components;
using Godot.Game.HSFMS.Skills;

namespace Godot.Game.HSFMS;

[GlobalClass]
[Tool]
public partial class BattleController : Node
{
    private UnitPlacementArea[,] _playerUnitPlacementAreas = new UnitPlacementArea[1, 1];
    private UnitPlacementArea[,] _enemyUnitPlacementAreas = new UnitPlacementArea[1, 1];
    private UnitPlacementArea _selectedUnitPlacementArea;
    private Node _playerPlacementAreasNode;
    private Node _enemyPlacementAreasNode;
    private int _playerTotalRows = 1;
    private int _playerTotalColumns = 1;
    private int _enemyTotalRows = 1;
    private int _enemyTotalColumns = 1;
    private Callable _onGettingSelectedAreaCallable;
    private Callable _onGettingActiveSkillCallable;
    private ActiveSkill _selectedActiveSkill;
    [Export]
    private StateMachine _stateMachine;
    [Export]
    private ActionBar _actionBar;
    [ExportGroup("Characters")]
    [Export]
    private Node _characters;
    [Export]
    public Node PlayerPlacementAreasNode
    {
        get => _playerPlacementAreasNode;
        set
        {
            _playerPlacementAreasNode = value;
            UpdateConfigurationWarnings();
        }
    }
    [Export]
    public Node EnemyPlacementAreasNode
    {
        get => _enemyPlacementAreasNode;
        set
        {
            _enemyPlacementAreasNode = value;
            UpdateConfigurationWarnings();
        }
    }
    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int PlayerTotalRows
    {
        get => _playerTotalRows;
        set
        {
            _playerTotalRows = Mathf.Clamp(value, 1, int.MaxValue);
            _playerUnitPlacementAreas = new UnitPlacementArea[_playerTotalRows, _playerTotalColumns];
            UpdateConfigurationWarnings();
        }
    }
    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int PlayerTotalColumns
    {
        get => _playerTotalColumns;
        set
        {
            _playerTotalColumns = Mathf.Clamp(value, 1, int.MaxValue);
            _playerUnitPlacementAreas = new UnitPlacementArea[_playerTotalRows, _playerTotalColumns];
            UpdateConfigurationWarnings();
        }
    }

    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int EnemyTotalRows
    {
        get => _enemyTotalRows;
        set
        {
            _enemyTotalRows = Mathf.Clamp(value, 1, int.MaxValue);
            _enemyUnitPlacementAreas = new UnitPlacementArea[_enemyTotalRows, _enemyTotalColumns];
            UpdateConfigurationWarnings();
        }
    }
    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int EnemyTotalColumns
    {
        get => _enemyTotalColumns;
        set
        {
            _enemyTotalColumns = Mathf.Clamp(value, 1, int.MaxValue);
            _enemyUnitPlacementAreas = new UnitPlacementArea[_enemyTotalRows, _enemyTotalColumns];
            UpdateConfigurationWarnings();
        }
    }


    public override string[] _GetConfigurationWarnings()
    {
        List<string> result = [];
        if (_playerPlacementAreasNode == null)
        {
            result.Add("Battle Controller needs a node that will handle the Character Placement Areas for the Player.");
        }
        if (_enemyPlacementAreasNode == null)
        {
            result.Add("Battle Controller needs a node that will handle the Character Placement Areas for the Enemy.");
        }
        if (_playerPlacementAreasNode != null && _playerPlacementAreasNode.GetChildCount() > PlayerTotalColumns * PlayerTotalRows)
        {
            result.Add("The number of children of the node handling the player's character placement areas must not exceed " + (PlayerTotalColumns * PlayerTotalRows) + ".");
        }
        if (_enemyPlacementAreasNode != null && _enemyPlacementAreasNode.GetChildCount() > EnemyTotalColumns * EnemyTotalRows)
        {
            result.Add("The number of children of the node handling the enemy's character placement areas must not exceed " + (EnemyTotalColumns * EnemyTotalRows) + ".");
        }
        return [.. result];
    }
    public override void _Ready()
    {
        _onGettingSelectedAreaCallable = new Callable(this, nameof(OnGettingSelectedArea));
        _onGettingActiveSkillCallable = new Callable(this, nameof(OnGettingActiveSkill));
        ConnectingCharacterPlacementsAreas();
    }

    private void ConnectingCharacterPlacementsAreas()
    {
        int rowIndex = 0; int colIndex = 0;
        if (_playerPlacementAreasNode != null && _enemyUnitPlacementAreas != null)
        {
            foreach (Node child in _playerPlacementAreasNode.GetChildren())
            {
                if (child is UnitPlacementArea childCharArea)
                {
                    _playerUnitPlacementAreas[rowIndex, colIndex] = childCharArea;
                    childCharArea.Connect(nameof(childCharArea.SendSelectedArea), _onGettingSelectedAreaCallable);
                    childCharArea.Index = (rowIndex, colIndex++);
                    if (colIndex >= PlayerTotalColumns)
                    {
                        colIndex = 0; rowIndex++;
                    }
                }
            }
            rowIndex = 0; colIndex = 0;
            foreach (Node child in _enemyPlacementAreasNode.GetChildren())
            {
                if (child is UnitPlacementArea childCharArea)
                {
                    _enemyUnitPlacementAreas[rowIndex, colIndex] = childCharArea;
                    childCharArea.Connect(nameof(childCharArea.SendSelectedArea), _onGettingSelectedAreaCallable);
                    childCharArea.Index = (rowIndex, colIndex++);
                    if (colIndex >= EnemyTotalColumns)
                    {
                        colIndex = 0; rowIndex++;
                    }
                }
            }
        }

    }

    public bool SetSelectedArea(CharacterBody2D character, UnitPlacementArea unitPlacementArea)
    {
        _selectedUnitPlacementArea?.Unselect();
        if (_selectedUnitPlacementArea != unitPlacementArea)
        {
            _selectedUnitPlacementArea = unitPlacementArea;
            _selectedUnitPlacementArea.Select();
            _actionBar.RemoveSkillButtons(_onGettingActiveSkillCallable);
            if (character.HasNode("ActiveSkillsComponent"))
            {
                ActiveSkillsComponent activeSkillsComponent = character.GetNode<ActiveSkillsComponent>("ActiveSkillsComponent");
                SkillButton[] skillButtons = activeSkillsComponent.SkillButtons;
                _actionBar.GetSkillButtons(skillButtons);
                foreach (SkillButton skillButton in skillButtons)
                {
                    skillButton.Connect(nameof(skillButton.SendActiveSkill), _onGettingActiveSkillCallable);
                }
            }
            return true;
        }
        else
        {
            _actionBar.RemoveSkillButtons(_onGettingActiveSkillCallable);
            _selectedUnitPlacementArea = null;
            return false;
        }
    }

    public void SetSelectedActiveSkill(ActiveSkill activeSkill)
    {
        _selectedActiveSkill = activeSkill;
    }

    public void HighlightEnemyTargets(bool isPlayer)
    {
        if (!isPlayer) { return; }
        switch (_selectedActiveSkill.RangeType)
        {
            case Types.RangeType.MELEE:
            {
                int columnToBeHighlighted = 0;
                for (int i = 0; i < _enemyTotalColumns; i++)
                {
                    for (int j = 0; j < _enemyTotalRows; j++)
                    {
                        if (_enemyUnitPlacementAreas[j, i].HasCharacter())
                        {
                            columnToBeHighlighted = i;
                            break;
                        }
                    }
                }
                for (int j = 0; j < _enemyTotalRows; j++)
                {
                    _enemyUnitPlacementAreas[j, columnToBeHighlighted].EnemyTargetHighlight();
                }
                break;
            }
            case Types.RangeType.RANGED:
            {
                for (int i = 0; i < _enemyTotalColumns; i++)
                {
                    for (int j = 0; j < _enemyTotalRows; j++)
                    {
                        _enemyUnitPlacementAreas[j, i].EnemyTargetHighlight();
                    }
                }
                break;
            }
            default:
                break;
        }
        
    }

    public void HighlightAllyTargets(bool isPlayer)
    {
        if (!isPlayer) { return; }
        switch (_selectedActiveSkill.RangeType)
        {
            case Types.RangeType.SELF:
            {
                break;
            }
            case Types.RangeType.BEHIND:
            {
                break;
            }
            case Types.RangeType.BEHIND_AND_SIDES:
                break;
            default:
                break;
        }
    }
    private void OnGettingSelectedArea(CharacterBody2D character, UnitPlacementArea unitPlacementArea)
    {
        _stateMachine.ProcessSignal(Types.SignalType.ON_GETTING_SELECTED_AREA, character, unitPlacementArea);
    }

    private void OnGettingActiveSkill(ActiveSkill activeSkill)
    {
        _stateMachine.ProcessSignal(Types.SignalType.ON_GETTING_SELECTED_SKILL, activeSkill);
    }
}
