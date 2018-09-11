using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardS2 : ObstacleS2 {
	public override void OnTriggerEnter (Collider other)
	{	
		Debug.Log(AnimationManagerS2.instance.isRoll);
		if (!AnimationManagerS2.instance.isRoll) {
			base.OnTriggerEnter (other);
		}
	}

}
