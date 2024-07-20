using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundManager : MonoBehaviour {
	
	public AudioSource SpeechSoundSource;
	public AudioSource BackgroundSoundSource;
	public AudioSource EffectSoundSource;
	public AudioSource GameplaySoundSource;
	public AudioSource ParralaxEffect;

	public List<AudioClip> listSpeechSnd;
	public List<AudioClip> listBackgroundSnd;
	public List<AudioClip> listEffectSnd;
	public List<AudioClip> listSuggestionSnd;
	public List<AudioClip> listParralaxEffect;

	public static SoundManager instance;

	private bool _enableBgMusic = true;
	private bool _enableMusic = true;




	// Use this for initialization
	void Awake () {
		instance = this;
	}

	public bool isEnableMusic {
		set {_enableMusic = value;
			if (value == true) {
				if (_enableBgMusic) {
					if (BackgroundSoundSource) {
						BackgroundSoundSource.Play ();
					}
				}
			} else {
				if (SpeechSoundSource) {
					SpeechSoundSource.Stop ();
				}
				if (BackgroundSoundSource) {
					BackgroundSoundSource.Stop ();
				}
				if (EffectSoundSource) {
					EffectSoundSource.Stop ();
				}
	
				if (GameplaySoundSource) {
					GameplaySoundSource.Stop ();
				}

				if (ParralaxEffect) {
					ParralaxEffect.Stop ();
				}
			}		
		}
	}


	public bool isEnableBGM {
		set {_enableBgMusic = value;
			if (_enableMusic && value) {
				BackgroundSoundSource.Play ();
			} else {
				BackgroundSoundSource.Stop ();
			}		
		}
	}


	public void PlaySpeechSound(string clip)
	{
		if (_enableMusic) {
			
			AudioClip ac = GetSpeechAudioClipFromName (clip);
			if (ac != null) {
				SpeechSoundSource.clip = ac;
				SpeechSoundSource.Play ();
			}
		}

	}


	public void playSpeechSound(int id)
	{
		if (_enableMusic) {
			
			AudioClip ac = GetSpeechAudioClipByID (id);
			if (ac != null) {
				SpeechSoundSource.clip = ac;
				SpeechSoundSource.Play ();
			}
		}
	}

	public void PlayBackgroundSound(string clip)
	{
		if (_enableMusic && _enableBgMusic) {
			AudioClip ac = GetBackgroundAudioClipFromName (clip);
			if (ac != null) {
				BackgroundSoundSource.clip = ac;			
				BackgroundSoundSource.Play ();
			}
		}
	}

	public void playParralaxEffect(string clip)
	{
		if (_enableMusic) {
			
			AudioClip ac = getParralaxAudioClipFromName (clip);
			if (ac != null) {
				ParralaxEffect.clip = ac;			
				ParralaxEffect.Play ();
			}
		}
	}


	public void PlayEffectSound(string clip)
	{		
		if (_enableMusic) {
			
			AudioClip ac = GetEffectAudioClipFromName (clip);
			if (ac != null) {
				
				EffectSoundSource.clip = ac;			
				EffectSoundSource.Play ();
			}
			else
			{
				ac = GetGameplayAudioClipFromName (clip);
				if (ac != null) {
					GameplaySoundSource.clip = ac;				
					GameplaySoundSource.Play ();
				}
			}
		}
	}

	public void PlayGameplaySound(string clip)
	{		

		if (_enableMusic) {
			
			AudioClip ac = GetGameplayAudioClipFromName (clip);
			if (ac != null) {
				GameplaySoundSource.clip = ac;			
				GameplaySoundSource.Play ();
			}
			else
			{
				ac = GetEffectAudioClipFromName (clip);
				if (ac != null) {
					EffectSoundSource.clip = ac;
					
					EffectSoundSource.Play ();
				}
			}
		}
	}

	public void playSuggestion(int id) {
		if (_enableMusic) {
			
			AudioClip ac = GetGameplayAudioClipByID (id);
			if (ac != null) {
				GameplaySoundSource.clip = ac;			
				GameplaySoundSource.Play ();
			}
		}
	}


	public void PauseBackgroundSound(bool isPause)
	{
		if (_enableMusic && _enableMusic) {
			if (isPause)
				BackgroundSoundSource.Pause ();
			else
				BackgroundSoundSource.UnPause ();
		}
	}


	public AudioClip GetSpeechAudioClipFromName(string name)
	{
		foreach (AudioClip ac in listSpeechSnd) 
		{
			if(name == ac.name)
				return ac;
		}
		return null;
	}

	public AudioClip GetSpeechAudioClipByID(int nameID)
	{		
		return listSpeechSnd.ElementAt (nameID);
	}

	public AudioClip GetEffectAudioClipFromName(string name)
	{
		foreach (AudioClip ac in listEffectSnd) 
		{
			if(name == ac.name)
				return ac;
		}
		return null;
	}

	public AudioClip GetGameplayAudioClipFromName(string name)
	{
		foreach (AudioClip ac in listSuggestionSnd) 
		{
			if(name == ac.name)
				return ac;
		}

		return null;
	}

	public AudioClip GetGameplayAudioClipByID(int id)
	{
		if (id >=0 && id < listSuggestionSnd.Count()) {
			return listSuggestionSnd.ElementAt(id);
		} else {
			Debug.LogError ("Out of list suggestion");
			return null;
		}
	}

	public AudioClip GetBackgroundAudioClipFromName(string name)
	{
		foreach (AudioClip ac in listBackgroundSnd) 
		{
			if(name == ac.name)
				return ac;
		}

		return null;
	}

	public AudioClip getParralaxAudioClipFromName(string name)
	{
		foreach (AudioClip ac in listParralaxEffect) 
		{
			if(name == ac.name)
				return ac;
		}

		return null;
	}	


}
