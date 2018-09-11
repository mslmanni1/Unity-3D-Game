using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontrollerS2 : MonoBehaviour {
	//新建4个game object 对应4个ui
	public GameObject PlayUI;
	public GameObject ResumeUI;
	public GameObject RestartUI;
	public GameObject PauseUI;

	public Canvas canvas;
	public static UIcontrollerS2 instance;
	// Use this for initialization
	public void PlayHandler()
	{
		HidePlayUI();
		ShowPauseUI();
		//AudioManager.instance.PlayButtonAudio();
		//UI与实际的逻辑交互
		GameControllerS2.instance.Play();
	}

	public void PauseHandler()
	{
		ShowResumeUI();
		HidePauseUI();
		//AudioManager.instance.PlayButtonAudio();
		GameControllerS2.instance.Pause();
	}

	public void ResumeHandler()
	{
		HideResumeUI();
		ShowPauseUI();
		//AudioManager.instance.PlayButtonAudio();
		GameControllerS2.instance.Resume();
	}

	public void RestartHandler()
	{
		HideRestartUI();
		ShowPauseUI();
		//AudioManager.instance.PlayButtonAudio();
		GameControllerS2.instance.Restart();
	}
	//	public void ExitHandler()
	//	{
	//			HideRestartUI();
	//			ShowPauseUI();
	//		//		//		AudioManager.instance.PlayButtonAudio();
	////		GameController.instance.Exit();
	//	}

	public void HidePlayUI()
	{
		iTween.MoveTo(PlayUI, canvas.transform.position + new Vector3(-Screen.width/2-2000, 0, 0), 1.0f);
	}

	public void ShowPauseUI()
	{
		iTween.MoveTo(PauseUI, canvas.transform.position + new Vector3(-Screen.width/2+120, -Screen.height / 2+120, 0), 1.0f);
	}

	public void ShowResumeUI()
	{
		iTween.MoveTo(ResumeUI, canvas.transform.position + Vector3.zero, 1.0f);
	}

	public void HidePauseUI()
	{
		iTween.MoveTo(PauseUI, canvas.transform.position + new Vector3(-Screen.width / 2 - 2000, -Screen.height / 2, 0), 1.0f);
	}

	public void HideResumeUI()
	{
		iTween.MoveTo(ResumeUI, canvas.transform.position + new Vector3(-Screen.width / 2 - 2000, 0, 0), 1.0f);
	}



	public void ShowRestartUI()
	{
		iTween.MoveTo(RestartUI, canvas.transform.position + Vector3.zero, 1.0f);
	}

	public void HideRestartUI()
	{
		iTween.MoveTo(RestartUI, canvas.transform.position + new Vector3(-Screen.width / 2 - 2000, 0, 0), 1.0f);
	}


	void Start () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {

	}
}
