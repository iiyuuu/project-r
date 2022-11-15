using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public bool isEnabled = false;
    public CurrencyManager currencyManager;
    public GameObject shopUI;
    public PlayerControls controls;

    public ShopItems[] shopItems;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsObject;
    public Button[] shopButtons;


    public void Start()
    {
    }

    private void Update()
    {
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        currencyManager = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<CurrencyManager>();
        if (isEnabled && controls != null)
        {
            for (int i = 0; i < shopItems.Length; i++)
            {
                shopPanelsObject[i].gameObject.SetActive(true);
            }
            LoadPanels();
            CheckPurchasable();
        }
        
    }

    public void EnableShop()
    {
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        isEnabled = true;
        shopUI.SetActive(true);
        controls.canMove = false;
        Cursor.visible = true;
    }

    public void DisableShop()
    {
        isEnabled = false;
        shopUI.SetActive(false);
        if (!controls.canMove)
        {
            controls.canMove = true;
        }
        Cursor.visible = false;
        FindObjectOfType<PlayerStats>().Save();
    }


    public void LoadPanels()
    {
        for(int i = 0; i < shopPanels.Length; i++)
        {
            shopPanels[i].titleText.text = shopItems[i].title;
            shopPanels[i].descriptionText.text = shopItems[i].description;
            shopPanels[i].costText.text = shopItems[i].baseCost.ToString();
        }
    }

    public void CheckPurchasable()
    {
        for(int i = 0; i < shopItems.Length; i++)
        {
            if (currencyManager.currency >= shopItems[i].baseCost)
            {
                shopButtons[i].interactable = true;
            }
            else
            {
                shopButtons[i].interactable= false;
            }
        }
    }

    public void PurchaseItem(int buttonNo)
    {
        if (currencyManager.currency >= shopItems[buttonNo].baseCost)
        {
            
            currencyManager.ChangeCurrency(-shopItems[buttonNo].baseCost);
            CheckPurchasable();
            //unlock item
        }
    }
}
