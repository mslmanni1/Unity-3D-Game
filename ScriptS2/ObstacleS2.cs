using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleS2 : MonoBehaviour {
	public int hurtValue = 1;
	public int moveSpeed = 0;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Translate (0, 0, -moveSpeed * Time.deltaTime);

	}
	public virtual void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			Debug.Log (other.tag);
//			Debug.Log("1");
//			Debug.Log(GameAttributeS2.instance.life);
			AudioManagerS2.instance.PlayHitAudio ();
			GameAttributeS2.instance.life -= hurtValue;
		}
		moveSpeed = 0;
	}
}
