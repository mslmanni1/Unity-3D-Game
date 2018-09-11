using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSetter : MonoBehaviour {

	public GameObject floorOnRunning;
	public GameObject floorForward;

	public static FloorSetter instance;
	// Use this for initialization
	void Start () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		if(transform.position.z > floorOnRunning.transform.position.z)
		{
			RemoveItem (floorOnRunning);
			AddItem (floorOnRunning);
			floorOnRunning.transform.position = new Vector3(0, 0,
				floorForward.transform.position.z + 60);
			
			GameObject temp = floorOnRunning;
			floorOnRunning = floorForward;
			floorForward = temp;
		}
	}


	void RemoveItem(GameObject floor)
	{
		var item = floor.transform.Find("Item");//???????????????
		if (item != null)
		{
			foreach (var child in item)
			{
				Transform childTranform = child as Transform;
				if (childTranform != null)
				{
					Destroy(childTranform.gameObject);
				}
			}
		}
	}

	void AddItem(GameObject floor)
	{
		var item = floor.transform.Find("Item");
		if (item != null)
		{
			var patternManager = PatternManager.instance;
			if (patternManager != null && patternManager.Patterns!=null && patternManager.Patterns.Count>0)
			{
				var pattern = patternManager.Patterns[Random.Range(0, patternManager.Patterns.Count)];
				if(pattern!=null && pattern.PatternItems!=null && pattern.PatternItems.Count>0)
				{
					foreach (var patternItem in pattern.PatternItems)
					{
						var newObj = Instantiate(patternItem.gameobject);
						newObj.transform.parent = item;
						newObj.transform.localPosition = patternItem.position;
					}
				}
			}
		}
	}
}
