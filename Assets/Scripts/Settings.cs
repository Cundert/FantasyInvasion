using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static float musicVolume=1.0f;
	public static float soundVolume=1.0f;

	public enum settingType { none, music, sound, shadows, antialiasing }
	public settingType type;

	private void Start() {
		switch (type) {
			case settingType.none:
				break;
			case settingType.music:
				GetComponent<Slider>().value=Settings.musicVolume;
				break;
			case settingType.sound:
				GetComponent<Slider>().value=Settings.soundVolume;
				break;
			case settingType.shadows:
				GetComponent<Dropdown>().value=indexShadows();
				break;
			case settingType.antialiasing:
				GetComponent<Dropdown>().value=indexAntialiasing();
				break;
		}
	}

	private int indexShadows() {
		switch (QualitySettings.shadows) {
			case ShadowQuality.Disable:
				return 0;
			case ShadowQuality.HardOnly:
				return 1;
			case ShadowQuality.All:
				return 3;
			default:
				return 3;
		}
	}

	private int indexAntialiasing() {
		switch (QualitySettings.antiAliasing) {
			case 0:
				return 0;
			case 2:
				return 1;
			case 3:
				return 2;
			case 4:
				return 3;
			default:
				return 3;
		}
	}
}
