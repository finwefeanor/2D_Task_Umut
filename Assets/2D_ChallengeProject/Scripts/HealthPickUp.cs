using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 20; // Amount of health this pickup provides
    public ParticleSystem healthEffect;
    public AudioSource healthSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healthAmount);
                // Put sound or effect here 
                healthSound.Play();
                healthEffect.Play();
                Destroy(gameObject); 
            }
        }
    }
}
