using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlaySound("Menu_BGM");
        }
    }

    public void PlaySound(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void StopSound()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }


}
