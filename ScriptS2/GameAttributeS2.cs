using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAttributeS2 : MonoBehaviour {
	public int coin;

	public int multiply = 1;

	public int life = 1;
	public int initial_life = 1;
	public Text Text_Coin;

	public static GameAttributeS2 instance;
	public bool soundOn = true;
	// Use this for initialization
	void Start () {
		coin = 0;
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		Text_Coin.text = coin.ToString ();
	}

	public void AddCoin(int coinNumber) {
		coin += multiply * coinNumber;
	}

	public void reset(){
		life = initial_life;
		coin = 0;
		multiply = 1;
	}
}
