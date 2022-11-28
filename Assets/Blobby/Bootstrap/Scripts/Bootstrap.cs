using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
	[SerializeField] private SceneManagerData sceneData;
	[SerializeField] private GameData gameData;
	private void Awake()
	{
		sceneData.Initialize(); //ensure scene changes are goind to respond to load events in GameData
		gameData.Initialize();
	}
}