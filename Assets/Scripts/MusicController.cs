using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

	private void Update() {
		GetComponent<AudioSource>().volume=Settings.musicVolume;
	}

}
