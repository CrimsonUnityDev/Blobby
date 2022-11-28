using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanelOpener : MonoBehaviour
{
    public GameObject SettingsPanel;

   public void OpenSettingsPanel()
    {
        if(SettingsPanel != null)
        {
            bool isActive = SettingsPanel.activeSelf;
            SettingsPanel.SetActive(!isActive);
        }
    }
}
