using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public TextMeshProUGUI text;
    public int currency;
    public ShopManager shop;

    public void ChangeCurrency(int currencyValue)
    {
        currency += currencyValue;
        text.text = "X" + currency.ToString();
        if (shop != null)
        {
            if (shop.isEnabled)
            {
                shop.CheckPurchasable();
            }
        }
        
        
    }
}
