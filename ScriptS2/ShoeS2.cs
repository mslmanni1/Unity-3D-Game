using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeS2 : ItemS2 {
	public override void OnTriggerEnter (Collider other)
	{
		base.OnTriggerEnter (other);
		if (other.tag == "Player") {
			PlayerControllerS2.instance.UseShoe ();
		}
	}
}
