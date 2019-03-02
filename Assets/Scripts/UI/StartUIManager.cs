using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// 开始界面UI逻辑控制器
/// </summary>

public class StartUIManager : MonoBehaviour {

    private GameObject m_StartPanel;//开始面板
    private GameObject m_SetingPanel;//设置面板

    private GameObject button_Seting;//设置按钮
    private GameObject button_Close;//关闭按钮
    private GameObject button_Play;//开始按钮
	
	
	void Start () {
        m_StartPanel = GameObject.Find("StartPanel");
        m_SetingPanel = GameObject.Find("SetingsPanel");
        //设置按钮
        button_Seting = GameObject.Find("Seting");
        UIEventListener.Get(button_Seting).onClick = SetingButtonClick;
        //关闭按钮
        button_Close = GameObject.Find("Close");
        UIEventListener.Get(button_Close).onClick = CloseButtononClick;
        //开始按钮
        button_Play = GameObject.Find("Play");
        UIEventListener.Get(button_Play).onClick = PlayButtononClick;

        m_SetingPanel.SetActive(false);//默认隐藏设置面板

	}
	
	/// <summary>
	/// 设置按钮被点击
	/// </summary>
	private void SetingButtonClick(GameObject go)
    {
        //首先判断设置面板是否显示，如果已经显示则不执行该逻辑
        if(m_SetingPanel.activeSelf == false)
        {
            Debug.Log("设置按钮被单击");
            m_SetingPanel.SetActive(true);
        }
    }

    /// <summary>
    /// 关闭按钮被点击
    /// </summary>
    private void CloseButtononClick(GameObject go)
    {
        m_SetingPanel.SetActive(false);
    }

    /// <summary>
    /// 开始按钮被点击
    /// </summary>
    private void PlayButtononClick(GameObject go)
    {
        //跳转场景
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// 设置开始按钮的状态
    /// </summary>
    /// <param name="state"></param>
    public void SetPlayButtonState(int state)
    {
        if (state == 1)
        { 
            button_Play.SetActive(true); 
        }
        else
        {
            button_Play.SetActive(false); 
        }
    }
}
