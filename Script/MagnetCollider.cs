using UnityEngine;
using System.Collections;

/// <summary>
/// 磁铁碰撞体
/// </summary>
public class MagnetCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")    //撞击到金币
        {
            StartCoroutine(HitCoin(other.gameObject));  //开启拾取金币协程
        }
    }

    /// <summary>
    /// 吸取金币
    /// </summary>
    /// <param name="coin">金币</param>
    /// <returns></returns>
    IEnumerator HitCoin(GameObject coin)
    {
        //循环标致
        bool isLoop = true;
        while(isLoop)
        {
            if (coin == null)   //金币为空
            {
                isLoop = false;
                continue;   //继续
            }
            //将金币逐渐吸到主角所在的位置
            coin.transform.position = Vector3.Lerp(coin.transform.position, PlayerController.instance.gameObject.transform.position, Time.deltaTime * 20);

            //如果金币位置与主角很近，则直接碰撞金币
            if (Vector3.Distance(coin.transform.position, PlayerController.instance.gameObject.transform.position) < 0.5f)
            {
                //碰撞
                coin.GetComponent<Coin>().HitItem();

                //退出循环
                isLoop = false;
            }
            yield return null;
        }
    }
}