using UnityEngine;
using System.Collections;

/// <summary>
/// 自动销毁
/// </summary>
public class AutoDestory : MonoBehaviour {

    /// <summary>
    /// 销毁时间
    /// </summary>
    public float destoryTime = 0.7f;

	// Use this for initialization
	void Start () {

        //某个时间后，自动销毁对应的gameObject
        Destroy(gameObject, destoryTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}