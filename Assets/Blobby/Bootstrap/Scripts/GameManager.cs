using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameData data;
	[SerializeField] private GameEvents gameEvents;
	[SerializeField] private LoadEvents loadEvents;

	private GameState state;

	private void Awake()
	{
		state = data.GameState;

		loadEvents.finalizedLoad += BeginGame;
	}

	private void OnDestroy()
	{
		loadEvents.finalizedLoad -= BeginGame;
	}

	private void BeginGame()
	{
		//Game events trigger 
		loadEvents.finalizedLoad -= BeginGame;
	}

}
