using System.Collections;
using System.Collections.Generic;
using System.Xml;  //引入命名空间

/// <summary>
/// 商城功能模块数据操作.
/// </summary>
public class ShopData {

    //用于存储xml数据的实体集合
    public List<ShopItem> shopList = new List<ShopItem>();

    //商品的购买状态集合
    public List<int> shopState = new List<int>();
    
    //金币数.
    public int goldCount = 0;
    //最高分数
    public int heightScore = 0;


    /// <summary>
    /// 通过指定路径读取xml文件.
    /// </summary>
    /// <param name="path">xml路径</param>
    public void ReadXmlByPath(string path)
    {
        XmlDocument doc = new XmlDocument();
        //doc.Load(path);//PC.
        doc.LoadXml(path);//安卓.
        XmlNode root = doc.SelectSingleNode("Shop");
        XmlNodeList nodeList = root.ChildNodes;
        foreach (XmlNode node in nodeList)
        {
            string speed = node.ChildNodes[0].InnerText;
            string rotate = node.ChildNodes[1].InnerText;
            string model = node.ChildNodes[2].InnerText;
            string price = node.ChildNodes[3].InnerText;
            string id = node.ChildNodes[4].InnerText;
            string ship = node.ChildNodes[5].InnerText;

            //遍历完毕后存储到list集合中
            ShopItem item = new ShopItem(id,speed ,rotate ,model ,ship,price );
            shopList.Add(item);
        }
    }

    /// <summary>
    /// 读取金币和最高分数.
    /// </summary>
    public void ReadScordAndGold(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;

        goldCount = int.Parse( nodeList[0].InnerText);
        heightScore = int.Parse(nodeList[1].InnerText);

        //读取商品的购买状态.
        for (int i = 2; i < 6; i++)
        {
            shopState.Add(int.Parse(nodeList[i].InnerText));
        }
    }

    /// <summary>
    /// 更新xml文档内容.
    /// </summary>
    public void UpdateXMLDate(string path,string key,string value)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;

        foreach (XmlNode node in nodeList)
        {
            if (node.Name == key)
            {
                node.InnerText = value;
                doc.Save(path);
            }
        }
    }
}
