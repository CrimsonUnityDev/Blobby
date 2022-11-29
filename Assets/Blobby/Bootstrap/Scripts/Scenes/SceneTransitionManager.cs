using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
	[SerializeField] private LoadEvents events;
	[SerializeField] private SceneManagerData data;
	private void Awake()
	{
		events.startedLoad += CheckFinishLoad;
	}
	private void OnDestroy()
	{
		events.startedLoad -= CheckFinishLoad;
	}

	private void CheckFinishLoad()
	{
		StartCoroutine(DoCheckFinishLoad());
	}

	private IEnumerator DoCheckFinishLoad()
	{
		while (data.LoadOperations.Count > 0)
		{
			for (int i = data.LoadOperations.Count - 1; i >= 0; i--)
			{
				if (data.LoadOperations[i].isDone)
				{
					data.LoadOperations.Remove(data.LoadOperations[i]);
				}
			}
			yield return null;
		}

		events.TriggerFinalizedLoad();
	}

}
