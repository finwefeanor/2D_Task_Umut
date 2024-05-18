using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;  // Singleton instance
    public AudioSource generalMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;  // Assign the current instance if it's the first one
            DontDestroyOnLoad(gameObject);  // Prevent the MusicManager from being destroyed on scene loads
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Destroy this object if an instance already exists
            return;
        }
    }

    void Start()
    {
        if (generalMusic != null)
            generalMusic.Play();  // Only play if the AudioSource is assigned
    }

    public void StopGeneralMusic()
    {
        if (generalMusic.isPlaying)
            generalMusic.Stop();  // Stop the music if it's playing
    }
}

