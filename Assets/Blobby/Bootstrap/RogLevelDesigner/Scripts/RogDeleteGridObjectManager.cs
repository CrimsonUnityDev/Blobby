using Hypertonic.GridPlacement;
using Hypertonic.GridPlacement.Models;
using UnityEngine;


    public class RogDeleteGridObjectManager : MonoBehaviour
    {
        [SerializeField] private LayerMask mask;
        private void Update()
        {

            if (UnityEngine.InputSystem.Mouse.current.rightButton.isPressed)
            {
                FireDeleteRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());
            }


        }

        private void FireDeleteRay(Vector2 mousePosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask.value))
            {
                Transform gameObjectHit = hit.transform;
                
                GridObjectInfo gridObjectInfo = gameObjectHit.GetComponent<GridObjectInfo>();

                Debug.LogError("HIT + " + gridObjectInfo + " " +gameObjectHit);
                // Check to see if a grid object was hit
                if(gridObjectInfo != null && gridObjectInfo.HasBeenPlaced)
                {
                Debug.LogError("SENDING");
                    GridManagerAccessor.GridManager.DeleteObject(gameObjectHit.gameObject, false);
                }

            }
        }
    }