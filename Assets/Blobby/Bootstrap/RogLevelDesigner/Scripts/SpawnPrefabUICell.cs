using UnityEngine;
using UnityEngine.UI;
using Hypertonic.GridPlacement;

public class SpawnPrefabUICell : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private LevelDesignerManager manager;
    private GameObject targetPrefab;
    private System.Action callback;

    public void Configure(GameObject obj, System.Action callback)
    {
        targetPrefab = obj;
        this.callback = callback;
        text.text = targetPrefab.name;
    }

    public void OnClick()
    {
        GameObject objectToPlace = Instantiate(targetPrefab, GridManagerAccessor.GridManager.GetGridPosition(), new Quaternion());
        objectToPlace.name = targetPrefab.name;
        GridManagerAccessor.GridManager.EnterPlacementMode(objectToPlace);
        manager.SetPrefabToSpawn(targetPrefab);
        callback();
        Hypertonic.GridPlacement.Example.BasicDemo.Button_GridObjectSelectionOption.Trigger(objectToPlace);
    }
}
