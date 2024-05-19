using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 30;
    public int attackDamage = 10;
    public float attackRange = 1.0f;
    public float attackRate = 1.0f;
    public LayerMask playerLayer;
    public AudioSource enemyGetHitSound;
    public AudioSource enemyAttackSound;
    public ParticleSystem enemyAttackParticle;
    //public ParticleSystem enemyAttackParticle2;
    public AudioSource enemyDieSound;

    private float nextAttackTime = 0f;


    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

            foreach (Collider2D player in hitPlayers)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                    if (enemyAttackSound != null) 
                        enemyAttackSound.Play();
                    enemyAttackParticle.Play();
                    //enemyAttackParticle2.Play();
                }
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    public void TakeDamage(int damage) //updated from PlayerAttack.cs
    {
        health -= damage;
        Debug.Log("Enemy health: " + health);
        if (enemyGetHitSound != null) enemyGetHitSound.Play();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        enemyDieSound.Play();
        // Implement enemy death logic (e.g., play animation, destroy object)
        Destroy(gameObject);
        Debug.Log("Enemy died: " + gameObject.name);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


}
