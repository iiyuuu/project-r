using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public TextMeshProUGUI text;
    int currency;

    public void ChangeCurrency(int currencyValue)
    {
        currency += currencyValue;
        text.text = "X" + currency.ToString();
    }
}
