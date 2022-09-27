using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode[] interactKey;
    public UnityEvent interactAction;
    public UnityEvent uninteractAction;


    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey[0]))
            {
                interactAction.Invoke();
            }
            else if (Input.GetKeyDown(interactKey[1]))
            {
                uninteractAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControls>().PromptEnable();
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControls>().PromptDisable();
            isInRange = false;
        }
    }
}
