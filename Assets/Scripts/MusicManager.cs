using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    public static MusicManager Instance {get; private set;}
    private AudioSource audioSource;
    private float musicVolume;
    
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        musicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 1f);
        audioSource.volume = musicVolume;
    }

    public void ChangeVolume()
    {
        musicVolume += .1f;
        if (musicVolume > 1.1f)
        {
            musicVolume = 0f;
        }
        
        audioSource.volume = musicVolume;
        Debug.Log(musicVolume);
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, musicVolume);
        PlayerPrefs.Save();
    }
    
    public float GetVolume()
    {
        return musicVolume;
    }
}
