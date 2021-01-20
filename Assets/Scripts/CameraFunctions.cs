using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFunctions : MonoBehaviour
{

    public GameObject UI, mainMenuSign, mainMenuText, backButton, arrowLever, chestToDisable;

    public void activateUI()
    {
        UI.SetActive(true);
		GameController.getInstance().pausable=true;
    }

	public void deactivateUI() {
		UI.SetActive(false);
	}

	public void activateMainMenuSign() {
		mainMenuSign.SetActive(true);
		mainMenuText.SetActive(true);
	}

	public void activateBackButton() {
		backButton.SetActive(true);
	}

	public void deleteArrowLever() {
		arrowLever.SetActive(false);
	}

	public void endLevelTransition() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Single);
	}

	public void deleteChest() {
		chestToDisable.SetActive(false);
	}

}
