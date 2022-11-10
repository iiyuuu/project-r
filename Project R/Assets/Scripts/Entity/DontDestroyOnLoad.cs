using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    public static DontDestroyOnLoad instance;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null && instance != gameObject.GetComponent<DontDestroyOnLoad>())
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        
    }

}
