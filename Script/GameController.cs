using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public bool isPause;
	public bool isPlay;
	public static GameController instance;
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
		GameAttribute.instance.reset();
		PlayerController.instance.Reset();
		PlayerController.instance.Play();
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
