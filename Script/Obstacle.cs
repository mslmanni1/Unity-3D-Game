using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	public int hurtValue = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public virtual void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			AudioManager.instance.PlayHitAudio ();
			GameAttribute.instance.life -= hurtValue;
		}
	}
}
