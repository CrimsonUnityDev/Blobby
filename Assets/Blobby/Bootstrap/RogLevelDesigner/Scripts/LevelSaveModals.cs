using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaveModals : MonoBehaviour
{
    [SerializeField] private LevelDesignerManager manager;
    [SerializeField] private TMPro.TMP_InputField input;

    public void Load()
    {
        manager.Load(input.text);
    }

    public void Save()
    {
        manager.Save(input.text);
    }
}
