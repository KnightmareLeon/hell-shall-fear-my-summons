using System.Collections.Generic;
using System.Linq;
using Godot.Collections;
using Godot.Game.HSFMS.Components;
using Godot.Game.HSFMS.Resources;

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
    private Callable _onSkillButtonPressedCallable;

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
        _onSkillButtonPressedCallable = new Callable(this, nameof(OnSkillButtonPressed));
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
                    childCharArea.Connect(nameof(childCharArea.SendSelectedArea), new Callable(this, nameof(OnGettingSelectedArea)));
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
                    childCharArea.Connect(nameof(childCharArea.SendSelectedArea), new Callable(this, nameof(OnGettingSelectedArea)));
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
            _actionBar.RemoveSkillButtons(_onSkillButtonPressedCallable);
            if (character.HasNode("ActiveSkillsComponent"))
            {
                ActiveSkillsComponent activeSkillsComponent = character.GetNode<ActiveSkillsComponent>("ActiveSkillsComponent");
                SkillButton[] skillButtons = activeSkillsComponent.SkillButtons;
                _actionBar.GetSkillButtons(skillButtons);
                foreach (SkillButton skillButton in skillButtons)
                {
                    skillButton.Connect(Button.SignalName.Pressed, _onSkillButtonPressedCallable);
                }
            }
        }
        else
        {
            _selectedUnitPlacementArea = null;
        }
        if (_selectedUnitPlacementArea == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnGettingSelectedArea(CharacterBody2D character, UnitPlacementArea unitPlacementArea)
    {
        _stateMachine.ProcessSignal(Types.SignalType.ON_GETTING_SELECTED_AREA, character, unitPlacementArea);
    }

    public void OnSkillButtonPressed()
    {
        _stateMachine.ProcessSignal(Types.SignalType.ON_SKILL_BUTTON_PRESSED);
    }

    public void OnGettingHit(Hit hit)
    {

    }

    public void OnGettingTarget()
    {
        
    }
}
