using System.Collections.Generic;
using System.Linq;
using Godot.Collections;

namespace Godot.Game.HSFMS;

[GlobalClass][Tool]
public partial class BattleController : Node
{
    private CharacterPlacementArea[,] _playerCharacterPlacementAreas;
    private CharacterPlacementArea[,] _enemyCharacterPlacementAreas;
    private CharacterPlacementArea _selectedCharacterPlacementArea;
    private Node _playerPlacementAreasNode;
    private Node _enemyPlacementAreasNode;
    private int _playerTotalRows = 1;
    private int _playerTotalColumns = 1;
    private int _enemyTotalRows = 1;
    private int _enemyTotalColumns = 1;
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
            _playerCharacterPlacementAreas = new CharacterPlacementArea[_playerTotalRows, _playerTotalColumns];
        }
    }
    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int PlayerTotalColumns
    {
        get => _playerTotalColumns;
        set
        {
            _playerTotalColumns = Mathf.Clamp(value, 1, int.MaxValue);
            _playerCharacterPlacementAreas = new CharacterPlacementArea[_playerTotalRows, _playerTotalColumns];
        }
    }

    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int EnemyTotalRows
    {
        get => _enemyTotalRows;
        set
        {
            _enemyTotalRows = Mathf.Clamp(value, 1, int.MaxValue);
            _enemyCharacterPlacementAreas = new CharacterPlacementArea[_enemyTotalRows, _enemyTotalColumns];
        }
    }
    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int EnemyTotalColumns
    {
        get => _enemyTotalColumns;
        set
        {
            _enemyTotalColumns = Mathf.Clamp(value, 1, int.MaxValue);
            _enemyCharacterPlacementAreas = new CharacterPlacementArea[_enemyTotalRows, _enemyTotalColumns];
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
        return [.. result];
    }
    public override void _Ready()
    {
        ConnectingCharacterPlacementsAreas();
    }

    private void ConnectingCharacterPlacementsAreas()
    {
        foreach (Node child in _playerPlacementAreasNode.GetChildren())
        {
            if (child is CharacterPlacementArea childCharArea)
            {
                childCharArea.Connect(nameof(childCharArea.SendSelectedArea), new Callable(this, nameof(OnGettingSelectedArea)));
            }
        }
        foreach (Node child in _enemyPlacementAreasNode.GetChildren())
        {
            if (child is CharacterPlacementArea childCharArea)
            {
                childCharArea.Connect(nameof(childCharArea.SendSelectedArea), new Callable(this, nameof(OnGettingSelectedArea)));
            }
        }
    }

    private void OnGettingSelectedArea(CharacterBody2D character, CharacterPlacementArea characterPlacementArea)
    {
        _selectedCharacterPlacementArea?.Unselect();
        if (_selectedCharacterPlacementArea != characterPlacementArea)
        {
            _selectedCharacterPlacementArea = characterPlacementArea;
            _selectedCharacterPlacementArea.Select();
        }
    }

}
