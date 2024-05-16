using UnityEngine;

public class CastleInteraction : MonoBehaviour
{
    private bool isPlayerInRange;
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to the castle
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isPlayerInRange && !audioSource.isPlaying)
        {
            // Play the music if the player is in range and the music is not already playing
            audioSource.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // Optionally, play the music immediately when the player enters
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Optionally, stop the music when the player exits
            audioSource.Stop();
        }
    }
}
