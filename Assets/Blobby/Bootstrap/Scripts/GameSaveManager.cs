using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rog/Data/GameSaveManager")]
public class GameSaveManager : ScriptableObject
{
	private const string LOAD_STATE_KEY = "load_state";
	private const string SLOT_KEY = "slot_";
	[SerializeField] private GameState defaultState;
	public GameState Load(string slot = null)
	{
		slot = PlayerPrefs.GetString(SLOT_KEY + slot, null);
		if (string.IsNullOrEmpty(slot))
		{
			return defaultState.Clone();
		}
		else
		{
			return JsonUtility.FromJson<GameState>(slot);
		}
	}

	public void Save(GameState state, string slot)
	{
		LoadState fileList = GetLoadState();
		if (!fileList.files.Contains(slot))
		{
			fileList.files.Add(slot);
			string filesJson = JsonUtility.ToJson(fileList);
			PlayerPrefs.GetString(LOAD_STATE_KEY, filesJson);
		}
		string json = JsonUtility.ToJson(state);
		PlayerPrefs.SetString(SLOT_KEY + slot, json);
	}

	public LoadState GetLoadState()
	{
		string json = PlayerPrefs.GetString(LOAD_STATE_KEY, null);
		if (string.IsNullOrEmpty(json))
		{
			return new LoadState();
		}
		else
		{
			return JsonUtility.FromJson<LoadState>(json);
		}
	}
}
