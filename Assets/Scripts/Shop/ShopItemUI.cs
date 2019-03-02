using UnityEngine;
using System.Collections;


/// <summary>
/// 商城Item控制器.
/// </summary>
public class ShopItemUI : MonoBehaviour {

    private Transform m_Transform;

    private UILabel ui_Speed;
    private UILabel ui_Rotate;
    private UILabel ui_Price;
    private GameObject shipParent; //飞机木星父物体.

    private GameObject buyButton;  //绑定购买事件.
    private GameObject itemState;  //商品状态.

    public int itemPrice;          //商品价格
    public int itemId;             //商品编号.

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        ui_Speed = m_Transform.FindChild("Speed/Speed_Num").GetComponent<UILabel>();
        ui_Rotate = m_Transform.FindChild("Rotate/Rotate_Num").GetComponent<UILabel>();
        ui_Price = m_Transform.FindChild("Buy_Button/Price").GetComponent<UILabel>();
        shipParent = m_Transform.FindChild("Model").gameObject;
        itemState = m_Transform.FindChild("Buy_Button").gameObject;

        buyButton = m_Transform.FindChild("Buy_Button/Bg").gameObject;
        UIEventListener.Get(buyButton).onClick = BuyButtonClick;
        
    }

    /// <summary>
    /// 给单个商品的UI赋值
    /// </summary>
    public void SetUIValue(string id, string speed, string rotate, string price,GameObject model,int state)
    {
        //给UI元素赋值.
        ui_Speed.text = speed;
        ui_Rotate.text = rotate;
        ui_Price.text = price;

        itemPrice = int.Parse(price);
        itemId = int.Parse(id);

        //判断商品购买状态.
        if (state == 1)
        {
            itemState.SetActive(false);
        }

        //实例化飞机模型，设置相关细节参数.
        GameObject ship =  NGUITools.AddChild(shipParent, model);
        ship.layer = 8; //设置模型所处层为NGUI层.
        Transform ship_Transform = ship.GetComponent<Transform>();
        ship_Transform.FindChild("Mesh").gameObject.layer = 8;  //给子物体设置为NGUI层.
        //设置飞机模型的缩放位置信息
        if (model.name == "Ship_4")
        {
            ship_Transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
        else
        {
            ship_Transform.localScale = new Vector3(8, 8, 8);   
        }
        
        ship_Transform.localPosition = new Vector3(0, -46, 0);
        Vector3 rot = new Vector3(-90, 0, 0);
        ship_Transform.localRotation = Quaternion.Euler(rot);

    }

    /// <summary>
    /// 购买点击事件
    /// </summary>
    private void BuyButtonClick(GameObject go)
    {
        SendMessageUpwards("CalcItemPrice",this);
    }

    /// <summary>
    /// 购买成功.
    /// </summary>
    public void BuyEnd()
    {
        itemState.SetActive(false);
    }
}
