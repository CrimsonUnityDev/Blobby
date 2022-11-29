using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabUIList : MonoBehaviour
{
    [SerializeField] private SpawnPrefabList list;
    [SerializeField] private RectTransform container;
    [SerializeField] private SpawnPrefabUICell prefabCell;

    private void OnEnable()
    {
        for (int i=container.childCount-1; i>=0; i--)
        {
            Destroy(container.GetChild(i).gameObject);
        }

        for (int i=0; i<list.gameObjects.Count; i++)
        {
            SpawnPrefabUICell cell = Instantiate(prefabCell, container);
            cell.Configure(list.gameObjects[i], ClickCallback);
        }
    }

    private void ClickCallback()
    {
        gameObject.SetActive(false);
    }
}
