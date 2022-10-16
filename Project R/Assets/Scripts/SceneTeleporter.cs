using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;

public class SceneTeleporter : MonoBehaviour
{

    public string sceneToLoad;
    public GameObject loadingScreen;
    public Slider loadingBar;
    public PlayerControls controls;
    public GameObject spawn;
    List<string> usedScenes = new List<string>(); //keeps track of used scenes
    bool isDuplicate = true;
    System.Random rand = new System.Random();

    public void Start()
    {
        isDuplicate = true;
        spawn = GameObject.FindGameObjectWithTag("Spawn");
    }

    private void FixedUpdate()
    {
        spawn = GameObject.FindGameObjectWithTag("Spawn");
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            

            if(sceneToLoad != null)//if there is already a thing in the fill bar
            {
                isDuplicate = false;
                StartCoroutine(LoadSceneAsynchronously(sceneToLoad));
                
            }

            while (isDuplicate)
            {
                switch (rand.Next(1, 4))
                {
                    case 1:
                        sceneToLoad = "F1_Zone1";
                        break;
                    case 2:
                        sceneToLoad = "F1_Zone2";
                        break;
                    case 3:
                        sceneToLoad = "F1_Zone3";
                        break;
                    case 4:
                        sceneToLoad = "F1_Zone4";
                        break;
                    default:
                        //Debug.Log("No room exists for this number : " + num.ToString());
                        break;
                }

                if (!(usedScenes.Contains(sceneToLoad)))
                {
                    usedScenes.Add(sceneToLoad);
                    isDuplicate = false;
                }

                if (usedScenes.Count == 4)
                {
                    sceneToLoad = "F1_BossRoom";
                    isDuplicate = false;
                }
            }

            //playerStorage.initialValue = playerPosition;
            //SceneManager.LoadScene(sceneToLoad);

            StartCoroutine(LoadSceneAsynchronously(sceneToLoad));

        }
    }

    IEnumerator LoadSceneAsynchronously(string sceneToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            
            yield return null;
        }
    }

    private void OnLevelWasLoaded()
    {
        spawn = GameObject.FindGameObjectWithTag("Spawn");
        controls.gameObject.transform.position = spawn.transform.position;
    }

    //return to hub function
}
