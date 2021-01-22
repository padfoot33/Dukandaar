using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using MEC;
public class StateListScript : MonoBehaviour
{
    public Dropdown CityDropdown; 
    void Start()
    {
        TextAsset txt = Resources.Load<TextAsset>("States");
        string[] states = txt.text.Split('\n');
        List<string> s = states.ToList();
        Debug.Log(states.Length);
        gameObject.GetComponent<Dropdown>().ClearOptions();
        gameObject.GetComponent<Dropdown>().AddOptions(s);
        gameObject.GetComponent<Dropdown>().onValueChanged.AddListener(delegate
        {
            ChangeCity();
        });
        CityDropdown.ClearOptions();
        TextAsset txtcity = Resources.Load<TextAsset>("1");
        string[] city = txtcity.text.Split('\n');
        List<string> c = city.ToList();
        CityDropdown.AddOptions(c);
        print(CityDropdown.options[CityDropdown.value].text);
    }

    public void ChangeCity()
    {
        CityDropdown.ClearOptions();
        int selectedState = gameObject.GetComponent<Dropdown>().value;
        selectedState = selectedState + 1;
        print(selectedState);
        TextAsset txtCity = Resources.Load<TextAsset>(""+selectedState+"");
        string[] city = txtCity.text.Split('\n');
        List<string> c = city.ToList();
        CityDropdown.AddOptions(c);
        
    }
}
