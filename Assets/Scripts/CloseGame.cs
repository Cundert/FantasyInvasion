using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseGame : MonoBehaviour
{

	public bool ending;

	void Update() {
		if (ending) {
			if (Input.GetKey("escape")|| Input.GetMouseButtonDown(0)) {
				SceneManager.LoadScene("MainMenu3D", LoadSceneMode.Single);
			}
		} else if (Input.GetKey("escape")) {
			Application.Quit();
		}
	}
}
