using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cy: ---1 is a group of code for camera buffer, which don't work
public class CameraManager : MonoBehaviour {

	public GameObject target;
	public float distance;
	public float height;
	Vector3 pos;//cy: ---1

	// Use this for initialization
	void Start () {
//		pos = transform.position;//cy: ---1
	}
	
	// Update is called once per frame
	//无法保证他的update先执行还是PlayerCon的update先执行
	void Update () {
		
	}

	void LateUpdate()
	{
		//transform.position = new Vector3 (target.transform.position.x,
			//target.transform.position.y + height,
			//target.transform.position.z - distance);

		//pos.x = Mathf.Lerp (pos.x, target.transform.position.x, Time.deltaTime);
		if (GameController.instance.isPlay && !GameController.instance.isPause) {
			pos.x = target.transform.position.x;
			pos.y = Mathf.Lerp (pos.y, target.transform.position.y+height, Time.deltaTime * 5);
			//pos.z = Mathf.Lerp (pos.z, target.transform.position.z-distance, Time.deltaTime * 5);
			pos.z = target.transform.position.z-distance;
			transform.position = pos;
		}

	}
}
