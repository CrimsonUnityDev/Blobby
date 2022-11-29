using System.Collections;
using System.Collections.Generic;
using Trisibo;
using UnityEngine;

[CreateAssetMenu(menuName = "Rog/Data/SceneTransition")]
public class SceneTransition : ScriptableObject
{
    public SceneField[] scenesToLoad;
    public SceneField[] scenesToUnload;

    public delegate void TransitionEvent(SceneTransition transition);
    public event TransitionEvent onTransition;

    public void TriggerTransition()
    {
        if (onTransition!=null)
        {
            onTransition(this);
        }
    }
}
