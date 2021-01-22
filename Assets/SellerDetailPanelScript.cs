using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SellerDetailPanelScript : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("name");
        transform.GetChild(0).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString("city");
        
    }
}
