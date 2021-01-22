using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopButton : MonoBehaviour
{
    GameObject Canvas;

    private void Start()
    {
        Canvas = GameObject.Find("Canvas");
    }

    public void OnShopButton()
    {
        GameObject SellerDetailsPanel = Canvas.transform.GetChild(1).gameObject;
        GameObject ListPanel = Canvas.transform.GetChild(3).gameObject;
        Debug.Log("done!");
        SellerDetailsPanel.SetActive(false);
        ListPanel.SetActive(true);
        ListPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text ="";
    }
}
