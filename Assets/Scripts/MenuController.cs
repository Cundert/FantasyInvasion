using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public GameObject title, background, settings, back, settingsButton, exitButton;

	public void backFromCreditsToMenu() {
		back.SetActive(false);
		settings.SetActive(false);
		Camera.main.GetComponent<Animator>().SetTrigger("goBack");
	}

	public void setMusicVolume(float value) {
		Settings.musicVolume=value;
	}

	public void setSoundVolume(float value) {
		Settings.soundVolume=value;
	}

	public void changeShadows(int value) {
		switch (value) {
			case 0:
				QualitySettings.shadows=ShadowQuality.Disable;
				break;
			case 1:
				QualitySettings.shadows=ShadowQuality.HardOnly;
				break;
			case 2:
				QualitySettings.shadows=ShadowQuality.All;
				break;
		}
	}

	public void changeAntiAliasing(int value) {
		switch (value) {
			case 0:
				QualitySettings.antiAliasing=0;
				break;
			case 1:
				QualitySettings.antiAliasing=2;
				break;
			case 2:
				QualitySettings.antiAliasing=4;
				break;
			case 3:
				QualitySettings.antiAliasing=8;
				break;
		}
	}

	public void backToMenu() {
		SceneManager.LoadScene("MainMenu3D", LoadSceneMode.Single);
		GameController.getInstance().pausa=false;
	}

	public void showSettingsInPause() {
		settings.SetActive(true);
		back.SetActive(true);
		title.SetActive(false);
		settingsButton.SetActive(false);
		exitButton.SetActive(false);
	}

	public void backFromSettingsInPause() {
		settings.SetActive(false);
		back.SetActive(false);
		title.SetActive(true);
		settingsButton.SetActive(true);
		exitButton.SetActive(true);
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			LayerMask mask = LayerMask.GetMask("MainMenuButton");
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 20.0f, mask)) {
				switch (hit.transform.tag) {
					case ("PlayButton"):
						if (title.activeInHierarchy) {
							Camera.main.GetComponent<Animator>().SetTrigger("MenuToPlay");
							title.SetActive(false);
							background.SetActive(false);
							Debug.Log("PlayButton");
						}
						break;
					case ("SettingsButton"):
						if (title.activeInHierarchy) {
							Camera.main.GetComponent<Animator>().SetTrigger("MenuToSettings");
							title.SetActive(false);
							background.SetActive(false);
							settings.SetActive(true);
							Debug.Log("Settings");
						}
						break;
					case ("CreditsButton"):
						if (title.activeInHierarchy) {
							Camera.main.GetComponent<Animator>().SetTrigger("credits");
							title.SetActive(false);
							background.SetActive(false);
							Debug.Log("Credits");
						}
						break;
					case ("InstructionsButton"):
						if (title.activeInHierarchy) {
							Camera.main.GetComponent<Animator>().SetTrigger("MenuToInstructions");
							title.SetActive(false);
							background.SetActive(false);
							Debug.Log("Instructions");
						}
						break;
					case ("Level1Button"):
						Debug.Log("Cargando el nivel 1");
						SceneManager.LoadScene("Level1_0", LoadSceneMode.Single);
						break;
					case ("Level2Button"):
						Debug.Log("Cargando el nivel 2");
						SceneManager.LoadScene("Level2_0", LoadSceneMode.Single);
						break;
					default:
						break;
				}
			}
		}
	}

}
