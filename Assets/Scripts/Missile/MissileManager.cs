using UnityEngine;
using System.Collections;

/// <summary>
/// 导弹生成器.
/// </summary>
public class MissileManager : MonoBehaviour {

    private Transform m_Transform;
    private Transform[] createPoints;//导弹生产点数组.
    private GameObject prefab_Missile3;//导弹预制体;

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        createPoints = GameObject.Find("CreatePoints").GetComponent<Transform>().GetComponentsInChildren<Transform>();
        prefab_Missile3 = Resources.Load<GameObject>("Missile_3");
    
        //调用生成器.
        InvokeRepeating("CreateMissile", 3, 5);
    }
	

    /// <summary>
    /// 生成导弹的方法.
    /// </summary>
    private void CreateMissile()
    {
        int index = Random.Range(0, createPoints.Length);
        GameObject.Instantiate(prefab_Missile3, createPoints[index].position, Quaternion.identity, m_Transform);
    }

    //停止导弹生成
    public void StopCreate()
    {
        CancelInvoke();
    }
}
