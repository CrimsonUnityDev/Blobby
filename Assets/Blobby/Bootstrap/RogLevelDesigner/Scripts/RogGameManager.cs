using System.Collections;
using Hypertonic.GridPlacement;
using UnityEngine;


public class RogGameManager : MonoBehaviour
{
    [SerializeField] private LevelDesignerManager manager;
    [SerializeField] private GridManager gridManager;

    private void Start()
    {
        manager.Configure(gridManager);
    }
    private void Update()
    {
        bool pressed = UnityEngine.InputSystem.Keyboard.current.pKey.isPressed;
        if (pressed)
        {
            //HandleConfirmButtonPressed in GridControlManager for when im less tired
            if (manager.IsPlacingGridObject)
            {
                // manager.ConfirmPlacement();
                Hypertonic.GridPlacement.Example.BasicDemo.Button_ConfirmPlacement.Trigger();
                // Hypertonic.GridPlacement.Example.BasicDemo.Button_GridObjectSelectionOption.Trigger(null);

            } 
            else if (!manager.IsPainting)
                {
                    manager.StartPaintMode();
                }
        }
        else
        {
            if (manager.IsPainting)
            {
                    manager.EndPaintMode();
            }
        }
    }
}
