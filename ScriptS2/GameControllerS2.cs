using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerS2 : MonoBehaviour {
	public bool isPause;
	public bool isPlay;
	public static GameControllerS2 instance;
	// Use this for initialization
	void Start () {
		instance = this;
		isPause = false;
		isPlay = true;
	}
	public void Play()
	{
		isPause = false;
	}

	public void Pause()
	{
		isPause = true;
	}

	public void Resume()
	{
		isPause = false;
	}

	public void Restart()
	{
		//对一些变量的值进行reset
		GameAttributeS2.instance.reset();
		PlayerControllerS2.instance.Reset();
		PlayerControllerS2.instance.Play();
	}

	//	public void Exit()
	//	{
	//		
	//		//预定义 unity退出和application发布后的·
	//#if UNITY_EDITOR
	//		UnityEditor.EditorApplication.isPlaying = false;
	//#else
	//			Application.Quit();
	//#endif
	//	}

	// Update is called once per frame
	void Update () {

	}
}
