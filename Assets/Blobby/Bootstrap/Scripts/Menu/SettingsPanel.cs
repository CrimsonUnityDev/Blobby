using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
	[SerializeField] private GameEvents events;
	[SerializeField] private AudioData data;
	[SerializeField] private UnityEvent enableEvent;
	[SerializeField] private UnityEvent hideEvent;
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider sfxSlider;

	private void Awake()
	{
	}

	private void OnEnable()
	{
		enableEvent.Invoke();
		musicSlider.value = data.musicVol;
		sfxSlider.value = data.sfxVol;
	}
	public void HomePressed()
	{
		events.TriggerHomePressed();
	}

	public void MusicSlider(float volume)
	{
		data.TriggerMusicSlider(volume);
	}

	public void SFXSlider(float volume)
	{
		data.TriggerSFXSlider(volume);
	}

	public void Hide()
	{
		hideEvent.Invoke();
	}

}
