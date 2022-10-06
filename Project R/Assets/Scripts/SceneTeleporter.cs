using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class SceneTeleporter : MonoBehaviour
{

    public string sceneToLoad;
    public GameObject loadingScreen;
    public Slider loadingBar;
    public Vector2 playerPosition;
    List<string> usedScenes = new List<string>(); //keeps track of used scenes
    bool isDuplicate = true;
    System.Random rand = new System.Random();


    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            isDuplicate = true;

            while(isDuplicate)
            {
                switch(rand.Next(1,4))
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
                }

                if(!(usedScenes.Contains(sceneToLoad)))
                {
                    usedScenes.Add(sceneToLoad);
                    isDuplicate = false;
                }

                if(usedScenes.Count == 4)
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
}
