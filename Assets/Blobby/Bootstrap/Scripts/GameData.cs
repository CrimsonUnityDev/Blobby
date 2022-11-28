using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rog/Data/GameData")]
public class GameData : ScriptableObject
{
	[SerializeField] private GameEvents gameEvents;
	[SerializeField] private GameSaveManager saveManager;
	[SerializeField] private LoadEvents loadEvents;

	private GameState gameState;
	public GameState GameState => gameState;
	private LoadState loadState;
	public LoadState LoadState => loadState;

	public void Initialize()
	{
		loadState = saveManager.GetLoadState();
		loadEvents.TriggerLoadState(loadState);

		gameEvents.homePressed += SaveCurrentSlot;
	}

	private void OnDestroy()
	{
		gameEvents.homePressed -= SaveCurrentSlot;
		SaveCurrentSlot();
	}

	public void NewGame()
	{
		gameState = saveManager.Load(null);
		gameEvents.TriggerLoadState(gameState);
	}

	public void StartGame(string slot)
	{
		gameState = saveManager.Load(slot);
		gameEvents.TriggerLoadState(gameState);
	}

	public void SaveCurrentSlot()
	{
		SaveSlot("current");
	}

	public void SaveSlot(string slot)
	{
		saveManager.Save(gameState, slot);
	}


}
