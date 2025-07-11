using Godot.Game.HSFMS.Components;

namespace Godot.Game.HSfMS;

public partial class GameManager : Node
{

    [Export]
    private Node _characters;
    private CharacterBody2D _selectedCharacter;

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
    }


    public void GetSelectedUnit(CharacterBody2D character)
    {
        _selectedCharacter = character;
        GD.Print(character.Name + " currently selected.");
    }
}
