using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	public void NewGameClicked()
	{
		gameData.NewGame();
	}

	public void LoadGameClicked()
	{
	}

	public void LoadSlot(string slot)
	{
		gameData.StartGame(slot);
	}

	public void SaveSlot(string slot)
	{

	}

	public void Exit()
	{
		Application.Quit();
	}
}
