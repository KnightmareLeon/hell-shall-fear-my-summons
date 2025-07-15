using Godot.Game.HSFMS.Components;

namespace Godot.Game.HSfMS;

public partial class GameManager : Node
{

    [Export]
    private Node _characters;
    [Export]
    private Node _characterPlacementAreas;
    private CharacterBody2D _selectedCharacter;

    private CharacterPlacementArea _currentCharacterPlacementArea;

    public override void _Ready()
    {
        foreach (Node child in _characters.GetChildren())
        {
            if (child is CharacterBody2D && child.HasNode("SelectableComponent"))
            {
                SelectableComponent selectableComp = (SelectableComponent)child.GetNode("SelectableComponent");
                selectableComp.Connect(nameof(selectableComp.SendSelectedCharacter), new Callable(this, nameof(GetSelectedUnit)));
            }
        }

        foreach (Node child in _characterPlacementAreas.GetChildren())
        {
            if (child is Area2D && (Script)child.GetScript() == GD.Load<Script>("res://scripts/CharacterPlacementArea.cs"))
            {
                CharacterPlacementArea childArea = (CharacterPlacementArea)child;
                childArea.Connect(nameof(childArea.SendSelectedArea), new Callable(this, nameof(OnGettingSelectedArea)));
            }
        }
    }

    private void OnGettingSelectedArea(CharacterBody2D character, CharacterPlacementArea characterPlacementArea)
    {
        _currentCharacterPlacementArea?.Unselect();
        if (_currentCharacterPlacementArea != characterPlacementArea)
        {
            _currentCharacterPlacementArea = characterPlacementArea;
            _currentCharacterPlacementArea.Select();
        }
    }

    public void GetSelectedUnit(CharacterBody2D character)
    {
        _selectedCharacter = character;
        GD.Print(character.Name + " currently selected.");
    }
}
