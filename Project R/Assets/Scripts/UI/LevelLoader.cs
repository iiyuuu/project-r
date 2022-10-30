using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public float transitionTime;
    public SceneTeleporter teleporter;

    public IEnumerator LoadingLevel(string scene)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        if(scene != null)
        {
            SceneManager.LoadScene(scene);

            if (teleporter != null)
            {
                teleporter.StartLoad(scene);
            }
        }
        
        
    }
}
