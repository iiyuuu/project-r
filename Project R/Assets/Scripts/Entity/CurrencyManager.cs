using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public TextMeshProUGUI text;
    public TextMeshProUGUI shopText;
    public int currency;
    public ShopManager shop;

    public void Awake()
    {
        shop = FindObjectOfType<ShopManager>(true);
        shopText = shop.GetComponentInChildren<TextMeshProUGUI>(true);
    }
    public void Update()
    {
        Awake();
    }
    public void ChangeCurrency(int currencyValue)
    {
        currency += currencyValue;
        text.text = "X" + currency.ToString();
        
        if (shop != null)
        {
            
            if (shop.isEnabled)
            {
                shopText.text = "X" + currency.ToString();
                shop.CheckPurchasable();
            }
        }
        
        
    }
}
