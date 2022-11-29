using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Trisibo;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Rog/Data/SceneManagerData")]
public class SceneManagerData : ScriptableObject
{
	[SerializeField] private LoadEvents loadEvents;
	[SerializeField] private GameEvents gameEvents;
	[SerializeField] private SceneTransition gameStateLoadTransition;
	[SerializeField] private SceneTransition loadStateLoadTransition;
	[SerializeField] private SceneTransition homePressedTransition;
	[SerializeField] private SceneField sceneTransitionScene;

	[SerializeField] private SceneTransition[] transitions;

	private List<AsyncOperation> loadOperations;
	public List<AsyncOperation> LoadOperations => loadOperations;

	public void Initialize()
	{
		for (int i=0; i<transitions.Length; i++)
		{
			transitions[i].onTransition += HandleSceneTransitionTrigger;
		}

		loadEvents.loadedState += HandleLoadedSaveState;
		gameEvents.loadedState += HandleLoadedGameState;
		gameEvents.homePressed += HandleHomePressed;	
	}

	private void OnDisable()
	{
		for (int i=0; i<transitions.Length; i++)
		{
			transitions[i].onTransition -= HandleSceneTransitionTrigger;
		}
		loadEvents.loadedState -= HandleLoadedSaveState;
		gameEvents.loadedState -= HandleLoadedGameState;
		gameEvents.homePressed -= HandleHomePressed;
	}

	private void HandleSceneTransitionTrigger(SceneTransition transition)
	{
		AsyncLoadScenes(transition.scenesToLoad, transition.scenesToUnload);
	}

	private void HandleLoadedSaveState(LoadState list)
	{
		SceneManager.LoadSceneAsync(sceneTransitionScene.BuildIndex, LoadSceneMode.Single);
		loadStateLoadTransition.TriggerTransition();
	}

	private void HandleLoadedGameState(GameState state)
	{
		gameStateLoadTransition.TriggerTransition();
	}

	private void HandleHomePressed()
	{
		homePressedTransition.TriggerTransition();
	}


	private void AsyncLoadScenes(SceneField[] loadScenes, SceneField[] unloadScenes)
	{
		loadOperations = loadScenes.Select(x => SceneManager.LoadSceneAsync(x.BuildIndex , LoadSceneMode.Additive)).Concat(unloadScenes.Select(x => SceneManager.UnloadSceneAsync(x.BuildIndex))).ToList();
		loadEvents.TriggerStartLoad();
	}
}
