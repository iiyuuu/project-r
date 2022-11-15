using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    public string currentScene;
    public AudioMixer mixer;
    [SerializeField]
    private float masterVolume;
    public Sound[] sounds;
    public static AudioManager instance;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume - GetMasterVolume();
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public float GetMasterVolume()
    {
        if(mixer.GetFloat("MasterVol", out masterVolume))
        {
            return 10 * Mathf.Log10(masterVolume);
        }
        else
        {
            return 0f;
        }
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.Contains("F1"))
        {
            Play("F1 BGM");
        }
        else if (currentScene.Contains("F2_Rest")){
            Stop("F1 BGM");
            Play("Hub Rest");
        }
        else if (currentScene.Contains("F2_Zone"))
        {
            Stop("Hub Rest");
            Play("F2 BGM");
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            if (name.Contains("BGM") || name.Contains("Rest"))
            {
                if (!s.source.isPlaying && Time.timeScale != 0)
                {
                    s.source.Play();
                }
            }
            else
            {
                s.source.Play();
            }
            
        }
        
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Stop();
        }
    }






}
