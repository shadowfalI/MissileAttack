using UnityEngine;
using System.Collections;
/// <summary>
/// 导弹控制脚本.
/// </summary>
public class Missile : MonoBehaviour {

    private Transform m_Transform;
    private Transform player_Transform;
    private GameObject Multi;//特效文件

    //导弹默认的前方.
    private Vector3 normalForward = Vector3.forward;

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        player_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Multi = Resources.Load<GameObject>("Multi");//特效加载
    }
	

	void Update () {
        m_Transform.Translate(Vector3.forward);

        //导弹追踪角色-------------------------------------------------.
        //计算方向(导弹与角色之间的方向.
        Vector3 dir = player_Transform.position - m_Transform.position;
        //插值计算当前导弹要旋转的角度.
        normalForward =  Vector3.Lerp(normalForward, dir, Time.deltaTime);
        //改变当前导弹的操作.
        m_Transform.rotation = Quaternion.LookRotation(normalForward);
	}


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Missile")
        {
            GameObject.Instantiate(Multi,m_Transform.position,Quaternion.identity);//爆炸特效
            GameObject.Destroy(gameObject);//相撞后销毁自身
        }
    }
}
