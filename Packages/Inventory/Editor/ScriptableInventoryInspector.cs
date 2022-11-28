using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(ScriptableInventory))]
public class ScriptableInventoryInspector : Editor
{
    private SerializedProperty invProp;
    private SerializedProperty contentsProp;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ScriptableInventory si = (ScriptableInventory) target;
        Inventory inv = si.Get();

        if (inv!=null)
        {
            Dictionary<InventoryItemType, int> contents = inv.contents;
            if (contents!=null)
            {
                List<InventoryItemType> keys = contents.Keys.ToList();
                if (keys == null || keys.Count <= 0)
                {
                    EditorGUILayout.LabelField("Inventory empty.");
                }
                GUI.enabled = false;
                for (int i=0; i< keys.Count; i++)
                {
                    EditorGUILayout.IntField( keys[i].itemName, contents[keys[i]] );
                }
                GUI.enabled = true;
            }
            else
            {
                EditorGUILayout.LabelField("Inventory exists, but contents null");
            }

        }
        else
        {
            EditorGUILayout.LabelField("Invalid Inventory!");
        }
        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
    }
}
