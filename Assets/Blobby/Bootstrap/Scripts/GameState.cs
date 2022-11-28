using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//should include every variable that describes the current state of the game
[System.Serializable]
public class GameState 
{
	public GameState Clone()
	{
		GameState state = new GameState();
		return state;
	}
}