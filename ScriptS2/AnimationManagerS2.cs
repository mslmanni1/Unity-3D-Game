using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagerS2 : MonoBehaviour {
	public delegate void AnimationHandler();


	Animation animation;
	public static AnimationManagerS2 instance;

	//这是原本在playerContrller里面的isROll 变量
	public bool isRoll = false;



	public AnimationClip Dead;
	public AnimationClip JumpDown;
	public AnimationClip JumpLoop;
	public AnimationClip JumpUp;
	public AnimationClip Roll;
	public AnimationClip Run;
	public AnimationClip TurnLeft;
	public AnimationClip TurnRight;

	public AnimationHandler animationHandler;

	// Use this for initialization
	void Start () {
		instance = this;
		animationHandler = PlayRun;
		animation = GetComponent<Animation> ();

	}


	
	// Update is called once per frame
	void Update () {
		if (animationHandler != null) {
			animationHandler ();
		}
	}


	//各种animation
	public void PlayDead()
	{
		animation.Play(Dead.name);
	}

	public void PlayJumpDown()
	{
		animation.Play(JumpDown.name);
	}

	public void PlayJumpLoop()
	{
		animation.Play(JumpLoop.name);
	}

	public void PlayJumpUp()
	{
		animation.Play(JumpUp.name);
		//快要播放完的时候， 切换为run
		if (animation [JumpUp.name].normalizedTime > 0.95f) {
			animationHandler = PlayRun;
		}
	}

	public void PlayRoll()
	{
		animation.Play(Roll.name);
		if (animation [Roll.name].normalizedTime > 0.95f) {
			animationHandler = PlayRun;

			//PlayerControllerS2.instance.isRoll = false;
			isRoll = false;
		} else {
			//PlayerControllerS2.instance.isRoll = true;
			isRoll = true;
		}
	}

	public void PlayDoubleJump()
	{
		//here different from vedio
		animation.Play(Roll.name);
		if (animation [Roll.name].normalizedTime > 0.95f) {
			animationHandler = PlayJumpLoop;
		}

	}

	public void PlayRun()
	{
		animation.Play(Run.name);
	}

	public void PlayTurnLeft()
	{
		animation.Play(TurnLeft.name);

		if (animation [TurnLeft.name].normalizedTime > 0.95f) {
			animationHandler = PlayRun;
		}
	}

	public void PlayTurnRight()
	{
		animation.Play(TurnRight.name);
		if (animation [TurnRight.name].normalizedTime > 0.95f) {
			animationHandler = PlayRun;
		}
	}
}
