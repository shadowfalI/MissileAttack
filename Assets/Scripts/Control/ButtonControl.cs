using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {

    private Ship m_Ship;

    private GameObject left;
    private GameObject right;

	void Start () {
        left = GameObject.Find("Left");
        right = GameObject.Find("Right");

        m_Ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();

        UIEventListener.Get(left).onPress = LeftPress;
        UIEventListener.Get(right).onPress = RIghtPress;
	}
	
    private void LeftPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            Debug.Log("Left Press");
            m_Ship.IsLeft = true;
        }
        else
        {
            Debug.Log("Left Press Over");
            m_Ship.IsLeft = false;
        }
    }

    private void RIghtPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            Debug.Log("Right Press");
            m_Ship.IsRight = true;
        }
        else
        {
            Debug.Log("Right Press Over");
            m_Ship.IsRight = false;
        }
    }

}
