using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public bool isEnabled = false;

    public List<ShopItems> inventoryItems;
    public List<InventoryTemplate> inventoryPanels;
    public InventoryTemplate inventoryPrefab;

    void Start()
    { 
    }

    public void Update()
    {
        if (isEnabled)
        {
            
            gameObject.SetActive(true);
            inventoryItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().smallPowerups;
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                inventoryPanels[i].gameObject.SetActive(true);//adds panels depending on how many items is in inventory
            }

            LoadPanels();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < inventoryPanels.Count; i++)
        {
            if (inventoryItems[i].enabled)
            {
                inventoryPanels[i].gameObject.GetComponent<Image>().sprite = inventoryItems[i].sprite;
                inventoryPanels[i].level.text = new string('I', inventoryItems[i].level);
            }
        }
    }
}
