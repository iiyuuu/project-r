using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public LevelLoader loader;
    public PlayerData playerdata;
    public GameObject playerPrefab;
    public void PlayGame()
    {
        string path = Application.persistentDataPath + "/player.data";//path for save file
        if (File.Exists(path))
        {
            LoadGame();//make a player, add variables into it, then throw it into hub
        }
        else
        {
            NewGame();
        }
       
    }

    public void NewGame()
    {
        StartCoroutine(loader.LoadingLevel("Tutorial Level"));
    }
    public void LoadGame()
    {
        GameObject player = Instantiate(playerPrefab);
        player.GetComponent<PlayerStats>().Load();  
    }

    public void QuitGame()
    {
        StartCoroutine(QuitRoutine()); 
    }
    
    List<int> widths = new List<int>() {800, 1280, 1366, 1920};
    List<int> heights = new List<int>() {600, 720, 768, 1080};

    public void SetScreenSize (int index) 
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);
    }

    public void SetFullscreen (bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
    }

    public IEnumerator QuitRoutine()
    {
        loader.animator.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        Debug.Log("QUIT");
        Application.Quit();
    }

}
