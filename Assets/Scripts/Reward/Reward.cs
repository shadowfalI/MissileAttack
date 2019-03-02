using UnityEngine;
using System.Collections;

/// <summary>
/// 奖励物品控制
/// </summary>


public class Reward : MonoBehaviour {

    private Transform m_Transform;
	
	void Start () {
	    m_Transform = gameObject.GetComponent<Transform>();
	}
	
	
	void Update () {
        m_Transform.Rotate(Vector3.left);
	}

    void OnDestroy()
    {
        SendMessageUpwards("RewardCountDown");//给父级物体发生消息
    }

}
