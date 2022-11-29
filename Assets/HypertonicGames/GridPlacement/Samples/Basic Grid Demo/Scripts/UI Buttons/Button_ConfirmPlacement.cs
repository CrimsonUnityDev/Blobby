using UnityEngine;
using UnityEngine.UI;

namespace Hypertonic.GridPlacement.Example.BasicDemo
{
    [RequireComponent(typeof(Button))]
    public class Button_ConfirmPlacement : MonoBehaviour
    {
        public static event System.Action OnConfirmPlacementPressed;

        public static void Trigger()
        {
            OnConfirmPlacementPressed?.Invoke();
        }
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => OnConfirmPlacementPressed?.Invoke());
        }
    }
}