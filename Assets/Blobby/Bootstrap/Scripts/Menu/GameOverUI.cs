using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	[SerializeField] private GameEvents events;
	[SerializeField] private RectTransform container;

	private void Awake()
	{
	}
	private void OnDestroy()
	{
	}

	private void HandleGameOver(Event e)
	{
		
			container.gameObject.SetActive(true);
		
	}
}
