using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneTeleporter : MonoBehaviour
{

    public string sceneToLoad;
    public Vector2 targetPosition;
    public Transform Player;
    List<string> usedScenes = new List<string>(); //keeps track of used scenes
    bool isDuplicate = true;
    System.Random rand = new System.Random();



    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("Teleporter"))
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

            foreach(string x in usedScenes){
                print(x);
            }
            

            SceneManager.LoadScene(sceneToLoad);
            targetPosition.x = 0;
            targetPosition.y = 0;
            Player.position = targetPosition;
        }
    }
}
