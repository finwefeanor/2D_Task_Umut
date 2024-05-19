using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int armorReduction = 5; // Damage reduction if armor is equipped
    public AudioSource playerHitSound;
    public SpriteRenderer clothesRenderer;
    public AudioSource playerDieSound;
    public int additionalHatArmorReduction = 3;
    private static bool isDying = false;
    public PlayerInventory playerInventory;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(20); 
        }
    }

    public void TakeDamage(int damage)
    {
        if (clothesRenderer.gameObject.activeSelf)
        {
            damage -= armorReduction; // Reduce damage if armor is equipped
        }

        if (playerInventory.CurrentlyEquippedHat != null)
        {
            damage -= additionalHatArmorReduction;  // Further reduce damage if hat is equipped
        }

        health -= damage;
        Debug.Log("Player health: " + health);

        playerHitSound.Play();

        if (playerHitSound != null) playerHitSound.Play();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isDying)
        {
            Debug.Log("Player has died");
            isDying = true;
            StartCoroutine(HandleDeath());
        }
        //Debug.Log("Player has died");
        // Implement player death logic  reload scene, show game over screen etc
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        // Example: Reload the current scene some end game animation later
        //StartCoroutine(HandleDeath());        

    }

    IEnumerator HandleDeath()
    {
        // Play the death sound
        if (playerDieSound != null && !playerDieSound.isPlaying)
        {
            playerDieSound.Play();
        }

        // Wait for the duration of the death sound  2-3 seconds
        yield return new WaitForSeconds(playerDieSound != null ? playerDieSound.clip.length : 3.0f);

        isDying = false; // Reset the flag if you intend to use the scene again later without reloading
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
