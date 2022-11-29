using Hypertonic.GridPlacement.GridInput;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "Mouse Hover Input Definition", menuName = "Grid/Mouse Hover Input Definition")]
public class MouseHoverInputDefinition : GridInputDefinition
{
    public override Vector3? InputPosition()
    {
        return Mouse.current.position.ReadValue();
    }

    public override bool ShouldInteract()
    {
        return true;
    }
}
