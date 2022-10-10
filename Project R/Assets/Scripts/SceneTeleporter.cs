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


    public void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Teleporter")) {

            if(SceneManager.GetActiveScene().name == "Tutorial level") {
                Debug.Log(SceneManager.GetActiveScene().name);
                sceneToLoad = "Hub";
                isDuplicate = false;
            }
            else if (usedScenes.Count == 4) {
                sceneToLoad = "F1_BossRoom";
                isDuplicate = false;
            }
            else {
                isDuplicate = true;
            }

            while (isDuplicate) {

                int num = rand.Next(1, 5);

                switch (num) {
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
                    case 5:
                        print("5 chosen");
                        break;
                }

                if (!(usedScenes.Contains(sceneToLoad)) && isDuplicate) {
                    usedScenes.Add(sceneToLoad);
                    isDuplicate = false;
                }
            }

            foreach (string x in usedScenes)
            {
                print(x);
            }
            print("\n---------\n");

            SceneManager.LoadScene(sceneToLoad);
            targetPosition.x = 0;
            targetPosition.y = 0;
            Player.position = targetPosition;
        }       
    }
}
