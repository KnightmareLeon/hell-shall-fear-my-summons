using Godot.Game.HSFMS.Components;

namespace Godot.Game.HSfMS;

public partial class GameManager : Node
{

    [Export]
    private Node _characters;
    [Export]
    private Node _characterPlacementAreas;
    private CharacterPlacementArea _currentCharacterPlacementArea;

    public override void _Ready()
    {
        Script charAreaScript = GD.Load<Script>("res://scripts/CharacterPlacementArea.cs");
        foreach (Node child in _characterPlacementAreas.GetChildren())
        {
            if (child is Area2D && (Script)child.GetScript() == charAreaScript)
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

}
