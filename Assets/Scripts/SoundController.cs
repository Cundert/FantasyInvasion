using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    static public SoundController instance;

    public enum SoundId { diamond, explosion, attack, slimeAttack, swordAttack, lever, leverReady, minecart, chestOpening, fireballSummon, fireballExplosion, victory, iceballSummon, iceballExplosion };

	private float lastVolume;

    static public SoundController getInstance()
    {
        return instance;
    }

    private void Start()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);
		AudioSource[] sounds = GetComponents<AudioSource>();
		for (int i = 0; i<sounds.Length; ++i) {
			sounds[i].volume=Settings.soundVolume;
		}
		lastVolume=Settings.soundVolume;
	}

    public void play(SoundId id)
    {
        GetComponents<AudioSource>()[(int)id].Play();
    }

	public void sampleSound() {
		if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play(0);
	}

	public void Update() {
		if (lastVolume!=Settings.soundVolume) {
			lastVolume=Settings.soundVolume;
			AudioSource[] sounds = GetComponents<AudioSource>();
			for (int i = 0; i<sounds.Length; ++i) {
				sounds[i].volume=Settings.soundVolume;
			}
		}
	}

}
