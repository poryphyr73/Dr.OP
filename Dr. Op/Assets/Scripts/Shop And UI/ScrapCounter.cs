using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrapCounter : MonoBehaviour
{
    private TextMeshProUGUI textmesh;
    private void Awake()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
    }

    public void updateText(int scrapCount)
    {
        textmesh.text = scrapCount.ToString();
    }
}
