using UnityEngine;
using System.Collections;

/// <summary>
/// 奖励物品管理器
/// </summary>

public class RewardManager : MonoBehaviour {

    private Transform m_Transform;
    private GameObject prefab_reward;//奖励物品预制体 
    private int rewardCount = 0;     //当前场景中存在多少奖励物品
    private int rewardMaxCount = 3;     //当前场景中最多存在多少奖励物品
	
	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        prefab_reward = Resources.Load<GameObject>("Reward");
        InvokeRepeating("CreateReward",5,5);//奖励物品生成
	}

    /// <summary>
    /// 生成奖励物品
    /// </summary>
    private void CreateReward()
    {
        //存在的奖励物品不能超过最大值
        if (rewardCount < rewardMaxCount)
        {
            Vector3 pos = new Vector3(Random.Range(580,-390),0,Random.Range(-600,530));
            GameObject.Instantiate(prefab_reward, pos, Quaternion.identity, m_Transform);
            rewardCount++;
        }
    }

    /// <summary>
    /// 停止生成奖励物品
    /// </summary>
	public void StopCreate()
    {
        CancelInvoke();
    }

    /// <summary>
    /// 当前奖励物品--
    /// </summary>
    public void RewardCountDown()
    {
        rewardCount--;
    }
}
