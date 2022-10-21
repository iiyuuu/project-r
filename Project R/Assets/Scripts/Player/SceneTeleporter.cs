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
    bool isDuplicate = true;
    System.Random rand = new System.Random();

    public void Start()
    {
        isDuplicate = true;
        loadingScreen = GameObject.FindGameObjectWithTag("Loading Screen").transform.GetChild(0).gameObject;
        loadingBar = loadingScreen.GetComponentInChildren<Slider>(true);
        spawn = GameObject.FindGameObjectWithTag("Spawn");
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        OnLevelFinishedLoading();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger)
        {

            if (controls.usedScenes.Count == 4)
            {
                sceneToLoad = "F1_BossRoom";
                isDuplicate = false;
            }

            if (sceneToLoad.Length != 0)//if there is already a thing in the fill bar
            {
                isDuplicate = false;
                
            }
            else
            {
                while (isDuplicate)
                {
                    switch (rand.Next(1, 5))
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

                    if (!(controls.usedScenes.Contains(sceneToLoad)))
                    {
                        controls.usedScenes.Add(sceneToLoad);
                        isDuplicate = false;
                    }

                    
                }
                
            }
            StartCoroutine(LoadSceneAsynchronously(sceneToLoad));



            //playerStorage.initialValue = playerPosition;
            //SceneManager.LoadScene(sceneToLoad);



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

    private void OnLevelFinishedLoading()
    {
        //Debug.Log(controls.usedScenes.Count);
        controls.gameObject.transform.position = spawn.transform.position;
        loadingScreen.SetActive(false);
    }

    //return to hub function
}
