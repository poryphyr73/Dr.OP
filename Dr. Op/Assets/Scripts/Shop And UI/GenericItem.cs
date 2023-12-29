using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericItem : MonoBehaviour
{
    [SerializeField] [TextArea(2, 2)] private string itemName, desc;
    [SerializeField] int scrapCost;
    [SerializeField] GameObject menu;
    [SerializeField] bool destroyOnPurchase;
    private Player player;
    private GameObject menuItem;
    private ScrapCounter scrapCount;
    private WaveSpawner waveSpawner;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>();
        scrapCount = GameObject.FindGameObjectWithTag("ScrapBar").gameObject.GetComponent<ScrapCounter>();
        waveSpawner = GameObject.FindGameObjectWithTag("WaveHandler").gameObject.GetComponent<WaveSpawner>();
    }
    private void OnMouseEnter()
    {
        menuItem = Instantiate(menu);
        
        menuItem.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").gameObject.transform);
        menuItem.SetActive(false);
        menuItem.GetComponent<UI_Box>().setBoxes(itemName, desc, (scrapCost * (waveSpawner.shopAppearances)).ToString());
        menuItem.SetActive(true);
    }

    private void OnMouseOver()
    {
        menuItem.transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(1)) tryBuy();
    }

    private void OnMouseExit()
    {
        Destroy(menuItem);
    }

    public void tryBuy()
    {
        if (player.scrapVal >= scrapCost * (waveSpawner.shopAppearances))
        {
            player.findItemWith(itemName);
            player.scrapVal -= scrapCost*(waveSpawner.shopAppearances);
            Debug.Log(player.scrapVal);
            scrapCount.updateText((int)player.scrapVal);
            if (destroyOnPurchase)
            {
                Destroy(menuItem);
                Destroy(gameObject);
            }
        }
    }
}
