using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
	public void startToLevel() {
		SceneManager.LoadScene("LevelScene");
	}

	public void LevelBacktoStart() {
		SceneManager.LoadScene("startScene");
	}

	public void backtoLevels() {
		SceneManager.LoadScene("LevelScene");
	}
	public string levelPrefix = "Level0";

	public void loadLevel(int levelID) {
		SceneManager.LoadScene( levelPrefix + levelID);
	}

}
