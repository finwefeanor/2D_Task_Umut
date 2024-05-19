using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackRange = 1.0f;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public AudioSource attackSound;
    public AudioSource attackAxeSound;
    public ParticleSystem attackEffect;
    public ParticleSystem attackAxeEffect;
    public int axeBonusDamage = 5;
    public PlayerInventory playerInventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        int baseDamage = 10; // Base damage without any weapons

        if (playerInventory.CurrentlyEquippedWeapon != null)
        {
            baseDamage += axeBonusDamage; // Add extra damage if a weapon is equipped
            if (attackAxeSound != null)
                attackAxeSound.Play(); // sound for axe attacks
            if (attackAxeEffect != null)
                attackAxeEffect.Play(); // Play effect  for axe attacks
        }
        else
        {
            // Play default attack sound and effect if no weapon is equipped
            if (attackSound != null)
                attackSound.Play();
            if (attackEffect != null)
                attackEffect.Play();
        }

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Debug.Log("Enemies detected: " + hitEnemies.Length);

        // Apply damage to each enemy
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Damaging enemy: " + enemy.name);
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
                Debug.Log("Enemy took damage: " + attackDamage);
            }
            else
            {
                Debug.Log("No Enemy component found on: " + enemy.name);
            }
        }
    }


    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        // Apply damage to the enemy
    //        Enemy enemy = other.GetComponent<Enemy>();
    //        if (enemy != null)
    //        {
    //            enemy.TakeDamage(attackDamage);
    //        }
    //    }
    //}

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
