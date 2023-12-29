using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Box : MonoBehaviour
{
    [SerializeField] private GameObject itemName, description, price;

    public void setBoxes(string iName, string desc, string val)
    {
        itemName.GetComponent<TextMeshProUGUI>().text = iName;
        description.GetComponent<TextMeshProUGUI>().text = desc;
        price.GetComponent<TextMeshProUGUI>().text = val;
    }
}
