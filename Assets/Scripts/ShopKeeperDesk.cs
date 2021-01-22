using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeperDesk : MonoBehaviour
{
    public Text ShopName;
    public Text ShopCity;
    public GameObject ItemListPanel;
    public Transform content;
    private static string getSelle = "https://dukandaar1211.000webhostapp.com/seller/getitems.php";

    void Start()
    {
        ShopName.text = PlayerPrefs.GetString("sshopname");
        ShopCity.text = PlayerPrefs.GetString("city");
        WWWForm form = new WWWForm();
        //form.AddField("shopid", PlayerPrefs.GetString("shopid"));
        form.AddField("shopid", 1);
        WWW www = new WWW(getSelle, form);
        StartCoroutine("WaitForRequest", www);
    }
    
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log(www.text);
            if (www.text != "")
            {
                string[] itemsData = www.text.Split('/');
                Dictionary<int, string[]> sData = new Dictionary<int, string[]>();
                for (int i = 0; i < itemsData.Length-1; i++)
                {
                    sData.Add(i, itemsData[i].Split(';'));
                }
                
                for (int i = 0; i < sData.Count; i++)
                {
                    GameObject itemButton = Instantiate(Resources.Load<GameObject>("ItemButton"), content);

                    string items = sData[i][0];
                    string name = sData[i][1];

                    //setting id in the button text
                    itemButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = name;
                    itemButton.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = items;
                    itemButton.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        ItemButtonClick(name,items);
                    });
                }
            }

            else
                print("NoData");
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    public void ItemButtonClick(string name, string items)
    {
        ItemListPanel.SetActive(true);
        ItemListPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "\n" + items;
    }

}
