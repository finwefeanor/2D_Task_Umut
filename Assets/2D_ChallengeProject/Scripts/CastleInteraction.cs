using UnityEngine;
using System;

public class CastleInteraction : MonoBehaviour
{
    private bool isPlayerInRange;
    public AudioSource castleMusic;
    private MusicManager musicManager;
    public FadeController fadeController;

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        // Get the AudioSource component attached to the castle
    }
       

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            musicManager.StopGeneralMusic();
            if (!castleMusic.isPlaying)
            {
                castleMusic.Play();
            }

            fadeController.FadeInAndSwitchCamera(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (castleMusic.isPlaying)
            {
                castleMusic.Stop();
            }
            if (musicManager != null && musicManager.generalMusic != null)
                musicManager.generalMusic.Play();
            else
                Debug.LogError("MusicManager or generalMusic is not properly initialized.");
            //  implement switching back to the main camera here
            //fadeController.FadeInAndSwitchCamera(false);
            // Ensure FadeController is active before using it
            if (fadeController != null)
            {
                if (!fadeController.gameObject.activeSelf)
                {
                    fadeController.gameObject.SetActive(true); // Activate if not already active
                }
                fadeController.FadeInAndSwitchCamera(false);
            }
            else
            {
                Debug.LogError("FadeController is null or inactive.");
            }
        }
    }
}