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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(20); // An Example damage value
        }
    }

    public void TakeDamage(int damage)
    {
        if (clothesRenderer.gameObject.activeSelf)
        {
            damage -= armorReduction; // Reduce damage if armor is equipped
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
        Debug.Log("Player has died");
        // Implement player death logic (e.g., reload scene, show game over screen)
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        // Example: Reload the current scene
        StartCoroutine(HandleDeath());
        

    }

    IEnumerator HandleDeath()
    {
        // Play the death sound
        if (playerDieSound != null)
        {
            playerDieSound.Play();
        }

        // Wait for the duration of the death sound or a fixed time (e.g., 2-3 seconds)
        yield return new WaitForSeconds(playerDieSound != null ? playerDieSound.clip.length : 3.0f);

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
