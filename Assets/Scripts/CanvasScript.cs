using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasScript : MonoBehaviour
{
    private static string newCustomerUrl = "https://dukandaar1211.000webhostapp.com/customer/NewCustomer.php";
    private static string newSellerUrl = "https://dukandaar1211.000webhostapp.com/seller/NewSeller.php";
    private static string addItemsUrl = "https://dukandaar1211.000webhostapp.com/seller/additems.php";
    
    [SerializeField] GameObject ChkLoginPanel;
    [SerializeField] GameObject CLoginPanel;
    [SerializeField] GameObject SLoginPanel;
    [SerializeField] GameObject SellerDetailPanel;
    [SerializeField] GameObject ListPanel;
    [SerializeField] GameObject ShopKeeperDesk;
    [SerializeField] InputField ItemInput;
    [SerializeField] Text ItemsList;
    [SerializeField] InputField CName;
    [SerializeField] Dropdown CState;
    [SerializeField] Dropdown CCity;
    [SerializeField] GameObject CConfirmPanel;
    [SerializeField] InputField SName;
    [SerializeField] InputField SShopName;
    [SerializeField] InputField SAddress;
    [SerializeField] Dropdown SState;
    [SerializeField] Dropdown SCity;
    [SerializeField] GameObject SConfirmPanel;
    [SerializeField] GameObject ListConfirmPanel;

    private void Start()
    {
        if(PlayerPrefs.GetString("who") == "Seller")
        {
            ChkLoginPanel.SetActive(false);
            ShopKeeperDesk.SetActive(true);
        }
        else if (PlayerPrefs.GetString("who") == "Buyer")
        {
            ChkLoginPanel.SetActive(false);
            SellerDetailPanel.SetActive(true);
        }
    }

    public void OnSellerButton()
    {
        PlayerPrefs.SetString("who", "Seller");
        ChkLoginPanel.SetActive(false);
        SLoginPanel.SetActive(true);
    }
    public void OnBuyerButton()
    {
        PlayerPrefs.SetString("who", "Buyer");
        ChkLoginPanel.SetActive(false);
        CLoginPanel.SetActive(true);
    }

    public void OnAddButon()
    {
        if (ItemInput.text != "")
        {
            ItemsList.text = ItemsList.text + "\n" + ItemInput.text;
            ItemInput.text = null;
        }
    }

    public void CLSubmit()
    {
        if(CName.text != "")
        {
            CConfirmPanel.SetActive(true);
            CConfirmPanel.transform.GetChild(2).GetComponent<Text>().text = CName.text;
            CConfirmPanel.transform.GetChild(3).GetComponent<Text>().text = CState.options[CState.value].text;
            CConfirmPanel.transform.GetChild(4).GetComponent<Text>().text = CCity.options[CCity.value].text;
        }
    }
    public void OnCYesButton()
    {
        PlayerPrefs.SetString("name", CName.text);
        PlayerPrefs.SetString("state", CState.options[CState.value].text);
        PlayerPrefs.SetString("city", CCity.options[CCity.value].text);

        CustomerDBEntry();

        CLoginPanel.SetActive(false);
        CConfirmPanel.SetActive(false);
        SellerDetailPanel.SetActive(true);
    }

    public void OnCNoButton()
    {
        CConfirmPanel.SetActive(false);
    }

    public void SLSubmit()
    {
        if (SName.text != "" && SAddress.text != "" && SShopName.text != "")
        {
            SConfirmPanel.SetActive(true);
            SConfirmPanel.transform.GetChild(2).GetComponent<Text>().text = "Name : ";
            SConfirmPanel.transform.GetChild(3).GetComponent<Text>().text = "Shop : ";
            SConfirmPanel.transform.GetChild(4).GetComponent<Text>().text = "State : ";
            SConfirmPanel.transform.GetChild(5).GetComponent<Text>().text = "City : ";
            SConfirmPanel.transform.GetChild(6).GetComponent<Text>().text = "Address : ";

            SConfirmPanel.transform.GetChild(2).GetComponent<Text>().text += SName.text;
            SConfirmPanel.transform.GetChild(3).GetComponent<Text>().text += SShopName.text;
            SConfirmPanel.transform.GetChild(4).GetComponent<Text>().text += SState.options[SState.value].text;
            SConfirmPanel.transform.GetChild(5).GetComponent<Text>().text += SCity.options[SCity.value].text;
            SConfirmPanel.transform.GetChild(6).GetComponent<Text>().text += SAddress.text;

        }
    }

    public void OnSYesButton()
    {
        PlayerPrefs.SetString("sname", SName.text);
        PlayerPrefs.SetString("sshopname", SShopName.text);
        PlayerPrefs.SetString("saddress", SAddress.text);
        PlayerPrefs.SetString("sstate", SState.options[SState.value].text);
        PlayerPrefs.SetString("scity", SCity.options[SCity.value].text);

        SellerDBEntry();
        
        SLoginPanel.SetActive(false);
        SConfirmPanel.SetActive(false);
        ShopKeeperDesk.SetActive(true);
    }

    public void OnSNoButton()
    {
        SConfirmPanel.SetActive(false);
    }

    public void CustomerDBEntry()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", PlayerPrefs.GetString("name"));
        form.AddField("state", PlayerPrefs.GetString("state"));
        form.AddField("city", PlayerPrefs.GetString("city"));
        WWW www = new WWW(newCustomerUrl, form);

        StartCoroutine("WaitForRequest", www);

    }

    public void SellerDBEntry()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", PlayerPrefs.GetString("sname"));
        form.AddField("state", PlayerPrefs.GetString("sstate"));
        form.AddField("city", PlayerPrefs.GetString("scity"));
        form.AddField("shopname", PlayerPrefs.GetString("sshopname"));
        form.AddField("address", PlayerPrefs.GetString("saddress"));
        WWW www = new WWW(newSellerUrl, form);

        StartCoroutine("WaitForRequest", www);

    }
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    public void OnSendButton()
    {
        ListConfirmPanel.SetActive(true);
        ListConfirmPanel.transform.GetChild(0).GetChild(1).GetComponentInChildren<Text>().text = ItemsList.text;
        PlayerPrefs.SetString("ItemList", ItemsList.text);
    }
    public void OnBackButton()
    {
        ListPanel.SetActive(false);
        SellerDetailPanel.SetActive(true);
    }
    public void OnListYes()
    {
        ListConfirmPanel.SetActive(false);
        WWWForm form = new WWWForm();
        form.AddField("shopid", PlayerPrefs.GetString("ShopId"));
        form.AddField("city", PlayerPrefs.GetString("city"));
        form.AddField("items", PlayerPrefs.GetString("ItemList"));
        form.AddField("name", PlayerPrefs.GetString("name"));
        WWW www = new WWW(addItemsUrl, form);

        StartCoroutine("SendListCoroutine", www);
    }

    IEnumerator SendListCoroutine(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    public void OnListNo()
    {
        ListConfirmPanel.SetActive(false);
    }
}
