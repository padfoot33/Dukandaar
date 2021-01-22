using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellerContentScript : MonoBehaviour
{
    private static string newSellerUrl = "https://dukandaar1211.000webhostapp.com/customer/getid.php";
    public GameObject SellerDetailsPanel;
    public GameObject ListPanel;
    void Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("city", PlayerPrefs.GetString("city"));
        //form.AddField("city", "Bongaigaon");
        WWW www = new WWW(newSellerUrl, form);

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
                string[] shopData = www.text.Split('/');
                Dictionary<int, string[]> sData = new Dictionary<int, string[]>();

                for (int i = 0; i < shopData.Length; i++)
                {
                    sData.Add(i, shopData[i].Split(';'));
                }

                for (int i = 0; i < sData.Count; i++)
                {
                    print(sData[i][4]);
                    GameObject shopButton = Instantiate(Resources.Load<GameObject>("ShopButton"), transform);

                    string id= sData[i][0], name= sData[i][1], shopname= sData[i][2],address = sData[i][3], state = sData[i][4];
                    //setting id in the button text
                    shopButton.transform.GetChild(0).GetComponentInChildren<Text>().text = id;
                    shopButton.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = shopname;
                    shopButton.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "(Owned By : " + address + ")";
                    shopButton.GetComponent<Button>().onClick.AddListener(delegate 
                    {
                        ShopButtonClick(id,name,shopname,address,state); 
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

   public void ShopButtonClick(string id,string name,string shopname,string address,string state)
   {
        SellerDetailsPanel.SetActive(false);
        PlayerPrefs.SetString("ShopId", id);
        ListPanel.SetActive(true);
        ListPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "\n"+id;
        ListPanel.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = shopname + "\nOwned By :("+ name + ")";
        ListPanel.transform.GetChild(0).GetChild(2).GetComponent<Text>().text += "\n"+address + " ,"+ state;

   }
}
