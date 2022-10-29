using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;
    [SerializeField] private float fadeSpeed;

    public void ShowUI()
    {
        fadeIn = true;
        canvasGroup.gameObject.GetComponent<Image>().raycastTarget = true;
    }

    public void HideUI()
    {
        fadeOut = true;
        canvasGroup.gameObject.GetComponent<Image>().raycastTarget = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideUI();
        }
        if (fadeIn && canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            if(canvasGroup.alpha >= 1)
            {
                fadeIn = false;
            }
        }

        if(fadeOut && canvasGroup.alpha >= 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            if(canvasGroup.alpha == 0)
            {
                fadeOut = false;
            }
        }
    }
}
