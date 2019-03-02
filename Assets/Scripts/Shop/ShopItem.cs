
/// <summary>
/// 商城物品Ite实体类
/// </summary>
public class ShopItem {
    private string speed;
    private string rotate;
    private string model;
    private string price;
    private string id;
    private string ship;

    public ShopItem(string id, string speed, string rotate, string model,string ship, string price)
    {
        this.id = id;
        this.speed = speed;
        this.rotate = rotate;
        this.model = model;
        this.ship = ship;
        this.price = price;
    }
    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Speed 
    {
        get { return speed; }
        set { speed = value; }
    }
    public string Rotate
    {
        get { return rotate; }
        set { rotate = value; }
    }
    public string Model
    {
        get { return model; }
        set { model = value; }
    }
    public string Ship
    {
        get { return ship; }
        set { ship = value; }
    }
    public string Price
    {
        get { return price; }
        set { price = value; }
    }

    public override string ToString()
    {
        return string.Format("speef:{0},rotate:{1},model:{2},price:{3}", speed, rotate, model, price);
    }
}
