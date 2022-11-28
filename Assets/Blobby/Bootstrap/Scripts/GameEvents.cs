using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rog/Data/Game Events")]
public class GameEvents : ScriptableObject
{
	private GameState state;
	public delegate void LoadStateEvent(GameState state);
	public delegate void GameEvent();
	public event LoadStateEvent loadedState;
	public void TriggerLoadState(GameState state)
	{
		this.state = state;
		if (loadedState != null)
		{
			loadedState(state);
		}
	}

	public event GameEvent homePressed;
	public void TriggerHomePressed()
	{
		if (homePressed != null)
		{
			homePressed();
		}
	}
}
