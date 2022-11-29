using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hypertonic.GridPlacement;
using Hypertonic.GridPlacement.Enums;
using Hypertonic.GridPlacement.Models;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Rog/LevelDesigner/LevelDesignerManager")]
public class LevelDesignerManager : ScriptableObject
{
    private const string _playPrefsSaveDataKey = "level_key_";
    public GridSettings _gridSettings;

    public GameObject _gridObjectPrefab;

    private GridManager _gridManager;

    public SpawnPrefabList spawnList;

    public GridSettings[] layers;

    public bool IsPainting;

    //implementing when im less tired, we neeed a unique grid manager 
    // for each one to set up, but we configure only 1 at startup
    // public void SetLayer(int index)
    // {
    //     _gridSettings = layers[index]
    //     GridManagerAccessor.SetSelectedGridManager(_gridSettingsA.Key);      

    // }

    public void StartPaintMode()
    {
        IsPainting=true;
        _gridManager.StartPaintMode(_gridObjectPrefab);
    }

      [System.Serializable]
    public class SaveData
    {
        public int gridSize;
        public List<GridObjectSaveData> GridObjectSaveDatas = new List<GridObjectSaveData>();
    }

    [System.Serializable]
    public class GridObjectSaveData
    {
        public string PrefabName;
        public Vector2Int GridCellIndex;
        public ObjectAlignment ObjectAlignment;
        public Quaternion ObjectRotation;

        public GridObjectSaveData(string prefabKey, Vector2Int gridCellIndex, ObjectAlignment objectAlignment, Quaternion objectRotation)
        {
            PrefabName = prefabKey;
            GridCellIndex = gridCellIndex;
            ObjectAlignment = objectAlignment;
            ObjectRotation = objectRotation;
        }
    }

    public void Load(string fileName)
    {
_ = LoadGridData(fileName);
    }

    private void HandleLoadGridObjectsPressed(string fileName)
    {
        _ = LoadGridData(fileName);
    }

    public void SetCells(System.Single value)
    {
        SetCells((int) value);
    }

    public void SetCells(int size)
    {
        GridManagerAccessor.GridManager.GridSettings.Width =  size;
        GridManagerAccessor.GridManager.GridSettings.Height =  size;
        GridManagerAccessor.GridManager.GridSettings.AmountOfCellsX =  size;
        GridManagerAccessor.GridManager.GridSettings.AmountOfCellsY =  size;
    }

    private async Task LoadGridData(string fileName)
    {
        string saveDataAsJson = File.ReadAllText(Path.Combine(Application.dataPath, "Saves", fileName + ".json"));

        SaveData saveData = JsonUtility.FromJson<SaveData>(saveDataAsJson);
        SetCells(saveData.gridSize);

        List<GridObjectPositionData> gridObjectPositionDatas = new List<GridObjectPositionData>();

        foreach (GridObjectSaveData gridObjectSaveData in saveData.GridObjectSaveDatas)
        {
            GameObject prefab = spawnList.gameObjects.Where(x => x.name.Equals(gridObjectSaveData.PrefabName.Replace("(Clone)", ""))).FirstOrDefault();
            Debug.LogError(prefab);
            if (prefab == null)
            {
                Debug.LogErrorFormat("The save game manager does not have a prefab reference for the object: {0}", gridObjectSaveData.PrefabName);
                continue;
            }

            GameObject gridObject = Instantiate(prefab);

            // Set the rotation back to the saved rotation of the object
            gridObject.transform.rotation = gridObjectSaveData.ObjectRotation;

            // Remove the "(Clone)" from instantiated name.
            gridObject.name = prefab.name;


            GridObjectPositionData gridObjectPositionData = new GridObjectPositionData(gridObject, gridObjectSaveData.GridCellIndex, gridObjectSaveData.ObjectAlignment);
            gridObjectPositionDatas.Add(gridObjectPositionData);
        }

        GridData gridData = new GridData(gridObjectPositionDatas);

        await GridManagerAccessor.GridManager.PopulateWithGridData(gridData, true);
    }

    public void Save(string fileName)
    {
        GridData gridData = GridManagerAccessor.GridManager.GridData;

            SaveData saveData = new SaveData();
            saveData.gridSize = GridManagerAccessor.GridManager.GridSettings.AmountOfCellsX;

            for (int i = 0; i < gridData.GridObjectPositionDatas.Count; i++)
            {
                GridObjectPositionData gridObjectPositionData = gridData.GridObjectPositionDatas[i];

                GridObjectSaveData gridObjectSaveData = new GridObjectSaveData(gridObjectPositionData.GridObject.name,
                    gridObjectPositionData.GridCellIndex,
                    gridObjectPositionData.ObjectAlignment,
                    gridObjectPositionData.GridObject.transform.localRotation);

                saveData.GridObjectSaveDatas.Add(gridObjectSaveData);
            }

            string saveDataAsJson = JsonUtility.ToJson(saveData);

            string filePath = Path.Combine(Application.dataPath, "Saves", fileName + ".json");

            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            file.Directory.Create();

            Debug.LogError(file.Directory);

            File.WriteAllText(filePath, saveDataAsJson);


            // PlayerPrefs.SetString(_playPrefsSaveDataKey + fileName, saveDataAsJson);
    }

    public void EndPaintMode()
    {
        IsPainting=false;
        _gridManager.EndPaintMode();
    }

   public void SendEmail ()
    {
        string email = "me@example.com";
        string subject = MyEscapeURL("My Subject");
        string body = MyEscapeURL("My Body\r\nFull of non-escaped chars");
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    private string MyEscapeURL (string url)
    {
        return UnityWebRequest.EscapeURL(url).Replace("+","%20");
    }

    public void SetPrefabToSpawn(GameObject target)
    {
        _gridObjectPrefab = target;
    }

    public void ConfirmPlacement()
    {
        _gridManager.ConfirmPlacement();
    }

    public void Configure(GridManager manager)
    {
         if(_gridSettings == null)
        {
            Debug.LogError("The GameManager needs to have the grid settings assigned.");
            return;
        }

        if(_gridObjectPrefab == null)
        {
            Debug.LogError("The GameManager needs to have the grid object prefab assigned.");
            return;
        }

        if (manager==null)
        {
            _gridManager = new GameObject("Grid Manager").AddComponent<GridManager>();
            _gridManager.Setup(_gridSettings);
        }
        else
        {
            _gridManager=manager;
        }

    }

    public bool IsPlacingGridObject => _gridManager.IsPlacingGridObject;
    
}
