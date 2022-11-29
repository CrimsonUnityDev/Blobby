using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rog/Audio/AudioManager")]
public class AudioData : ScriptableObject
{
	public delegate void AudioSliderEvent(float value);
	public event AudioSliderEvent musicSlider;

	public float initialVolume = 0.8f;
	public float initialAnmVol = 0.8f;

	public float musicVol;
	public float sfxVol;

	public void TriggerMusicSlider(float value)
	{
		if (musicSlider != null)
		{
			musicVol = value;
			musicSlider(value);
		}
	}
	public event AudioSliderEvent sfxSlider;

	public void TriggerSFXSlider(float value)
	{
		if (sfxSlider != null)
		{
			sfxVol = value;
			sfxSlider(value);
		}
	}

}
