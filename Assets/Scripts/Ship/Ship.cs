using UnityEngine;
using System.Collections;

/// <summary>
/// 飞机控制器
/// </summary>

public class Ship : MonoBehaviour {

    private bool isLeft = false;//左转
    private bool isRight = false;//右转
    private bool isDeath = false;//角色是否死亡.
    private MissileManager m_MissileManager;
    private GameObject Nuke;//爆炸文件
    private int rewardNum = 0;//吃了多少奖励物品

    private Transform m_Transform;

    private GameUIManager m_GameUIManager;//引用

    private int speed;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private int rotate;
    public int Rotate
    {
        get { return rotate; }
        set { rotate = value; }
    }

    public bool IsLeft
    {
        get { return isLeft; }
        set { isLeft = value; }
    }

    public bool IsRight
    {
        get { return isRight; }
        set { isRight = value; }
    }

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        m_MissileManager = GameObject.Find("MissileManager").GetComponent<MissileManager>();
        Nuke = Resources.Load<GameObject>("Nuke");//特效
        m_GameUIManager = GameObject.Find("UI Root").GetComponent<GameUIManager>();
    }
	
	void Update () {
        //只有角色活着才能飞行
       if(isDeath == false)
       {
           m_Transform.Translate(Vector3.forward * speed);

           if (isLeft)
           {
               m_Transform.Rotate(Vector3.up * -1 * rotate);
           }

           if (isRight)
           {
               m_Transform.Rotate(Vector3.up * 1 * rotate);
           }
       }
	}

    private void OnTriggerEnter(Collider coll)
    {
        //和四边边缘相撞
        if(coll.tag == "Border")
        {
            isDeath = true;
            m_GameUIManager.ShowOverPanel();//显示结束面板
        }

        //飞机与导弹相撞
        if (coll.tag == "Missile")
        {
            isDeath = true;//主角死亡
            m_GameUIManager.ShowOverPanel();//显示结束面板
            GameObject.Instantiate(Nuke,m_Transform.position,Quaternion.identity);//播放特效
            m_MissileManager.StopCreate();//停止生成导弹
            GameObject.Destroy(coll.gameObject);//销毁导弹
            gameObject.SetActive(false);//隐藏角色
        }

        if (coll.tag == "Reward")
        {
            rewardNum++;//累加数量
            m_GameUIManager.UpdateScoreLabel(rewardNum);//更新UI分数显示
            GameObject.Destroy(coll.gameObject);//销毁奖励物品
        }
    }
}
