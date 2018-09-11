using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinS2 : ItemS2 {
	
	public override void HitItem()
	{
		base.HitItem();
		GameAttributeS2.instance.AddCoin(1);
	}

	public override void PlayHitAudio()
	{
		AudioManagerS2.instance.PlayCoinAudio ();
	}
}
