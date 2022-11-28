using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rog/Data/LoadEvents")]
public class LoadEvents : ScriptableObject
{
	public delegate void LoadStateEvent(LoadState list);
	public event LoadStateEvent loadedState;

	public delegate void LoadEvent();
	public event LoadEvent finalizedLoad;
	public event LoadEvent startedLoad;

	private LoadState state;


	public void TriggerStartLoad()
	{
		if (startedLoad != null)
		{
			startedLoad();
		}
	}

	public void TriggerLoadState(LoadState state)
	{
		this.state = state;
		if (loadedState != null)
		{
			loadedState(state);
		}
	}


	public void TriggerFinalizedLoad()
	{
		if (finalizedLoad != null)
		{
			finalizedLoad();
		}
	}
}
