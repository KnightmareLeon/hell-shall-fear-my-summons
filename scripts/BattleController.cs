using Godot.Collections;

namespace Godot.Game.HSFMS;

[GlobalClass][Tool]
public partial class BattleController : Node
{
    private CharacterPlacementArea[,] _characterPlacementAreas;
    private CharacterPlacementArea _selectedCharacterPlacementArea;
    private Node _characterPlacementAreasNode;
    private int _totalRows = 1;
    private int _totalColumns = 1;
    [Export]
    private Node _characters;
    [Export]
    public Node CharacterPlacementAreasNode
    {
        get => _characterPlacementAreasNode;
        set
        {
            _characterPlacementAreasNode = value;
            UpdateConfigurationWarnings();
        }
    }
    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int TotalRows
    {
        get => _totalRows;
        set
        {
            _totalRows = Mathf.Clamp(value, 1, int.MaxValue);
            _characterPlacementAreas = new CharacterPlacementArea[_totalRows, _totalColumns];
        }
    }
    [Export(PropertyHint.Range, "1,10,1,or_greater")]
    public int TotalColumns
    {
        get => _totalColumns;
        set
        {
            _totalColumns = Mathf.Clamp(value, 1, int.MaxValue);
            _characterPlacementAreas = new CharacterPlacementArea[_totalRows, _totalColumns];
        }
    }


    public override string[] _GetConfigurationWarnings()
    {
        if (_characterPlacementAreasNode == null)
        {
            return ["Battle Controller needs a node that will handle the Character Placement Areas."];
        }
        return [];
    }
    public override void _Ready()
    {
        ConnectingCharacterPlacementsAreas();
    }

    private void ConnectingCharacterPlacementsAreas()
    {
        foreach (Node child in _characterPlacementAreasNode.GetChildren())
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
