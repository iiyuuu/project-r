using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public float duration;
    private IEnumerator coroutine;
    public string level;
    [SerializeField] private Animator animator;
    public float transitionTime;
    void Start()
    {
        coroutine = Wait();
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            StopCoroutine(coroutine);
            StartCoroutine(LoadingLevel());
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(duration);
        StartCoroutine(LoadingLevel());
    }

    IEnumerator LoadingLevel()
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
    }
}
