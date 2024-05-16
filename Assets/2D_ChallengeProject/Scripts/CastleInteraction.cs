using UnityEngine;

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



    //void Update()
    //{
    //    if (isPlayerInRange && !audioSource.isPlaying)
    //    {
    //        // Play the music if the player is in range and the music is not already playing
    //        audioSource.Play();
    //    }
    //}

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

            fadeController.FadeInAndSwitchCamera();
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
            musicManager.generalMusic.Play();
            //  implement switching back to the main camera here
        }
    }
}
