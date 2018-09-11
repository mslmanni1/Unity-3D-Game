using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManagerS2 : MonoBehaviour {

	public AudioClip button;
	public AudioClip coin;
	public AudioClip getitem;
	public AudioClip hit;
	public AudioClip slide;

	public static AudioManagerS2 instance;

	public Sprite soundOnSprite;
	public Sprite soundOffSprite;

	public Image soundImage;

	private void PlayAudio(AudioClip clip)
	{
		if (GameAttributeS2.instance.soundOn)
		{
			AudioSource.PlayClipAtPoint (clip, PlayerControllerS2.instance.transform.position);
		}
	}

	public void SwitchSound()
	{
		GameAttributeS2.instance.soundOn = !(GameAttributeS2.instance.soundOn);
		soundImage.sprite = GameAttributeS2.instance.soundOn ? soundOnSprite : soundOffSprite;
	}

	public void PlayButtonAudio()
	{
		PlayAudio (button);	
	}

	public void PlayCoinAudio()
	{
		PlayAudio (coin);	
	}

	public void PlayGetItemAudio()
	{
		PlayAudio (getitem);	
	}

	public void PlayHitAudio()
	{
		PlayAudio (hit);	
	}

	public void PlaySlideAudio()
	{
		PlayAudio (slide);	
	}


	// Use this for initialization
	void Start () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {

	}
}
