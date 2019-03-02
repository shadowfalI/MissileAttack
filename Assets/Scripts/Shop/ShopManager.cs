using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;      //IO文件操作
/// <summary>
/// 商城模块总控制器
/// </summary>
public class ShopManager : MonoBehaviour {

    //商城数据对象.
    private ShopData shopData;

    //xml路径.
    //private string xmlPath = "Assets/Datas/ShopData.xml";//PC.
    private string xmlPath;//安卓.

    //xml存档路径.
    //private string savePath = "Assets/Datas/SaveData.xml";//PC.
    private string savePath = Application.persistentDataPath + "/SaveData.xml";//安卓.
    private string content = "<SaveData><GoldCount>100000</GoldCount><HeightScore>0</HeightScore><ID0>1</ID0><ID1>0</ID1><ID2>0</ID2><ID3>0</ID3></SaveData>";

    //商城商品模板.
    private GameObject ui_ShopItem;

    //两个按钮
    private GameObject leftButton;
    private GameObject rightBUtton;

    //商城商品UI集合
    private List<GameObject> shopUI = new List<GameObject>();

    //要展现的商品UI的索引.
    private int index = 0;

    //界面UI.
    private UILabel starNum;
    private UILabel scoreNum;

    //引用StartUIManager.
    private StartUIManager m_StartUIManager;

	void Start () {
        xmlPath = Resources.Load("ShopData").ToString();//安卓.
        if (!File.Exists(savePath))
        {
            File.WriteAllText(savePath, content);//安卓.
        }

        //实例化商城数据对象.
        shopData = new ShopData();
        //加载xml
        shopData.ReadXmlByPath(xmlPath);
        shopData.ReadScordAndGold(savePath);
        //shopData.shopList;

        ui_ShopItem = Resources.Load<GameObject>("UI/ShopItem");
        m_StartUIManager = GameObject.Find("UI Root").GetComponent<StartUIManager>();

        //按钮事件绑定
        leftButton = GameObject.Find("LeftButton");
        rightBUtton = GameObject.Find("RightButton");
        UIEventListener.Get(leftButton).onClick = LeftButtonClick;
        UIEventListener.Get(rightBUtton).onClick = RightButtonClick;

        //同步UI与xml中的数据
        starNum = GameObject.Find("Star/StarNum").GetComponent<UILabel>();
        scoreNum = GameObject.Find("Score/ScoreNum").GetComponent<UILabel>();
        //读取playerprefs中的新的最高分.
        int tempHeightScore = PlayerPrefs.GetInt("HeightScore", 0);
        if (tempHeightScore > shopData.heightScore)
        {
            //更新UI.
            UpdateUIHeightScore(tempHeightScore);
            //更新xml，存储最高分.
            shopData.UpdateXMLDate(savePath, "HeightScore", tempHeightScore.ToString());
            //清空playerprefs.
            PlayerPrefs.SetInt("HeightScore", 0);
        }
        else
        {
            //更新UI.
            UpdateUIHeightScore(shopData.heightScore);
        }
        
        //读取playerprefs中的金币数.
        int tempGold = PlayerPrefs.GetInt("GoldNum", 0);
        if (tempGold > 0)
        {
            int gold = shopData.goldCount + tempGold;
            //更新UI.
            UpdateUIGold(gold);
            //更新xml中的存储.  
            shopData.UpdateXMLDate(savePath, "GoldCount", gold.ToString());
            //清空playerprefs.
            PlayerPrefs.SetInt("GoldNum",0);
        }
        else
        {
            //更新UI.
            UpdateUIGold(shopData.goldCount);
        }

        SetPlayInfo(shopData.shopList[0]);

        CreateAllShopUI();
	}

    /// <summary>
    /// 更新UI显示
    /// </summary>
    private void UpdateUI()
    {
        starNum.text = shopData.goldCount.ToString();
        scoreNum.text = shopData.heightScore.ToString();
    }

    /// <summary>
    /// 更新最高分UI.
    /// </summary>
    private void UpdateUIHeightScore(int heightScore)
    {
        scoreNum.text = heightScore.ToString();
    }

    /// <summary>
    /// 更新金币UI.
    /// </summary>
    private void UpdateUIGold(int gold)
    {
        starNum.text = gold.ToString();
    }

    /// <summary>
    /// 创建商城模板中所有商品.
    /// </summary>
    private void CreateAllShopUI()
    {
        for (int i = 0; i < shopData.shopList.Count; i++)
        {
            //实例化单个商品UI.
           GameObject item = NGUITools.AddChild(gameObject,ui_ShopItem);
            //加载对应的飞机模型
           GameObject ship = Resources.Load<GameObject>(shopData.shopList[i].Model);
            //给商品UI元素赋值.
           item.GetComponent<ShopItemUI>().SetUIValue(shopData.shopList[i].Id, shopData.shopList[i].Speed, shopData.shopList[i].Rotate, shopData.shopList[i].Price,ship,shopData.shopState[i]);
           
            //将生成的UI添加到集合中
           shopUI.Add(item);

        }

        //隐藏UI.
        ShopUIHideAndShow(index);
    }
	
    /// <summary>
    /// 左侧按钮点击事件
    /// </summary>
    private void LeftButtonClick(GameObject go)
    {
        if (index > 0)
        {
            index--;
            int temp = shopData.shopState[index];
            m_StartUIManager.SetPlayButtonState(temp);
            SetPlayInfo(shopData.shopList[index]);
            ShopUIHideAndShow(index);
            
        }
        
    }

    /// <summary>
    /// 右侧按钮点击事件
    /// </summary>
    private void RightButtonClick(GameObject go)
    {
        if (index < shopUI.Count - 1)
        {
            index++;
            int temp = shopData.shopState[index];
            m_StartUIManager.SetPlayButtonState(temp);
            SetPlayInfo(shopData.shopList[index]);
            ShopUIHideAndShow(index);
        }
    }

    /// <summary>
    /// 商品UI的显示与隐藏
    /// </summary>
    private void ShopUIHideAndShow(int index)
    {
        for (int i = 0; i < shopUI.Count; i++)
			{
			    shopUI[i].SetActive(false);
			}
        shopUI[index].SetActive(true);
    }

    //计算商品价格
    private void CalcItemPrice(ShopItemUI item)
    {
        if (shopData.goldCount >= item.itemPrice)
        {
            item.BuyEnd();                              //隐藏购买UI按钮.
            shopData.goldCount -= item.itemPrice;       //减去已消耗的金币
            UpdateUI();                                 //更新UI.
            shopData.UpdateXMLDate(savePath, "GoldCount", shopData.goldCount.ToString());//更新xml金币数量
            shopData.UpdateXMLDate(savePath, "ID" + item.itemId, "1");  //更新xml中商品状态.
        }
    }

    /// <summary>
    /// 存储当前角色信息到PlayerPrefs中去.
    /// </summary>
    private void SetPlayInfo(ShopItem item)
    {
        PlayerPrefs.SetString("PlayerName", item.Ship);
        PlayerPrefs.SetInt("PlayerSpeed", int.Parse(item.Speed));
        PlayerPrefs.SetInt("PlayerRotate", int.Parse(item.Rotate));
    }
}
