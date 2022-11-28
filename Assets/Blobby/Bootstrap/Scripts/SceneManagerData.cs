using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Rog/Data/SceneManagerData")]
public class SceneManagerData : ScriptableObject
{
	[SerializeField] private LoadEvents loadEvents;
	[SerializeField] private GameEvents gameEvents;
	[SerializeField] private string menuScene;
	[SerializeField] private string audioScene;
	[SerializeField] private string bootStrapScene;
	[SerializeField] private string gameScene;
	[SerializeField] private string hudScene;
	[SerializeField] private string inputScene;
	[SerializeField] private string sceneTransitionScene;

	private List<AsyncOperation> loadOperations;
	public List<AsyncOperation> LoadOperations => loadOperations;

	public void Initialize()
	{
		loadEvents.loadedState += HandleLoadedSaveState;
		gameEvents.loadedState += HandleLoadedGameState;
		gameEvents.homePressed += HandleHomePressed;
	}

	private void OnDisable()
	{
		loadEvents.loadedState -= HandleLoadedSaveState;
		gameEvents.loadedState -= HandleLoadedGameState;
		gameEvents.homePressed -= HandleHomePressed;
	}

	private void HandleLoadedSaveState(LoadState list)
	{
		SceneManager.LoadSceneAsync(sceneTransitionScene, LoadSceneMode.Single); //annoying.

		string[] scenesToLoad = { menuScene, audioScene, inputScene };
		string[] scenesToUnload = { bootStrapScene };

		AsyncLoadScenes(scenesToLoad, scenesToUnload);
	}

	private void HandleLoadedGameState(GameState state)
	{
		string[] scenesToLoad = { gameScene, hudScene };
		string[] scenesToUnload = { menuScene };

		AsyncLoadScenes(scenesToLoad, scenesToUnload);
	}

	private void HandleHomePressed()
	{
		string[] scenesToLoad = { menuScene };
		string[] scenesToUnload = { gameScene, hudScene };

		AsyncLoadScenes(scenesToLoad, scenesToUnload);
	}

	private void AsyncLoadScenes(string[] loadScenes, string[] unloadScenes)
	{
		loadOperations = loadScenes.Select(x => SceneManager.LoadSceneAsync(x, LoadSceneMode.Additive)).Concat(unloadScenes.Select(x => SceneManager.UnloadSceneAsync(x))).ToList();
		loadEvents.TriggerStartLoad();
	}
}
