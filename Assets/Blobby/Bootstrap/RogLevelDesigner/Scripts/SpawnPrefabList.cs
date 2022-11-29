using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rog/LevelDesigner/SpawnPrefabList")]
public class SpawnPrefabList : ScriptableObject
{
    public List<GameObject> gameObjects;
}
