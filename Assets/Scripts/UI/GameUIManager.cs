using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏界面UI逻辑控制
/// </summary>

public class GameUIManager : MonoBehaviour {

    private GameObject m_GamePanel;//游戏面板
    private GameObject m_OverPanel;//结束面板

    private UILabel label_Score;   //分数UI
    private UILabel label_Time;    //时间UI.

    private GameObject m_ButtonControl;//控制器1

    private GameObject button_Reset;//重置按钮

    private int time;   //时间

    public int Time
    {
        get { return time; }
        set {
            time = value;
            UpdateTimeLabel(time);
        }
    }

    //OverPaneInfo
    private UILabel heightScore;  //最高分数.
    private UILabel starNum;      //奖励数量.
    private UILabel timeNum;       //时间长度.

	void Start () {
        m_GamePanel = GameObject.Find("GamePanel");
        m_OverPanel = GameObject.Find("OverPanel");

        label_Score = GameObject.Find("StarNum1").GetComponent<UILabel>();
        label_Score.text = "0";
        label_Time = GameObject.Find("Time1").GetComponent<UILabel>();
        label_Time.text = "0:00";
        StartCoroutine("AddTime");

        heightScore = GameObject.Find("Score/ScoreNum").GetComponent<UILabel>();
        starNum = GameObject.Find("Star/StarNum").GetComponent<UILabel>();
        timeNum = GameObject.Find("Time/TimeNum").GetComponent<UILabel>();

        m_ButtonControl = GameObject.Find("ButtonControl");
        button_Reset = GameObject.Find("Reset");
        UIEventListener.Get(button_Reset).onClick = ResetButtononClick;

        m_OverPanel.SetActive(false);//结束面板默认隐藏
	}
	
    /// <summary>
    /// 更新分数显示
    /// </summary>
    /// <param name="score"></param>
	public void UpdateScoreLabel(int score)
    {
        label_Score.text = score.ToString();
    }
	
    /// <summary>
    /// 更新时间UI显示.
    /// </summary>
    private void UpdateTimeLabel(int t)
    {
        if(t<60)
        {
            label_Time.text = "0:" + t;
        }
        else
        {
            label_Time.text = t / 60 + ":" + t % 60;
        }
    }

    /// <summary>
    /// 显示结束面板
    /// </summary>
    public void ShowOverPanel()
    {
        m_ButtonControl.SetActive(false);
        m_OverPanel.SetActive(true);
        StopAddTime();
        SetOverPanelInfo();
    }

    /// <summary>
    /// 重置按钮被点击，回到开始面板
    /// </summary>
    /// <param name="go"></param>
    private void ResetButtononClick(GameObject go)
    {
        SceneManager.LoadScene("Start");
    }

    /// <summary>
    /// 协程累加时间.
    /// </summary>
    /// <returns></returns>
    IEnumerator AddTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            Time++;
        }
    }

    /// <summary>
    /// 停止时间累加.
    /// </summary>
    private void StopAddTime()
    {
        StopCoroutine("AddTime");
    }

    /// <summary>
    /// 给结束面板赋值.
    /// </summary>
    private void SetOverPanelInfo()
    {
        int t = int.Parse(label_Score.text);
        starNum.text = "+" + t*10;
        timeNum.text = "+" + time.ToString();
        int tempHeightScore = t * 10 + time;
        heightScore.text = tempHeightScore.ToString();

        //存储最高分.
        PlayerPrefs.SetInt("HeightScore", tempHeightScore);
        //存储金币数.
        PlayerPrefs.SetInt("GoldNum", t * 10);
    }
}
