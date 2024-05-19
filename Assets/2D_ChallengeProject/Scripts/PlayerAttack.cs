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

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        int totalDamage = CalculateDamage(); // Initialize total damage

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
                enemyComponent.TakeDamage(totalDamage);
                Debug.Log("Enemy took damage: " + totalDamage);
            }
            else
            {
                Debug.Log("No Enemy component found on: " + enemy.name);
            }
        }
    }

    int CalculateDamage()
    {
        int baseDamage = 10; // Base damage without any weapons
        int totalDamage = baseDamage; // Initialize total damage

        // Check if the player has a weapon equipped
        if (playerInventory.CurrentlyEquippedWeapon != null && playerInventory.CurrentlyEquippedWeapon.type == InventoryItem.ItemType.Weapon)
        {
            totalDamage += axeBonusDamage; // Add extra damage if an axe is equipped
            if (attackAxeSound != null)
            {
                attackAxeSound.Play(); // Play sound for axe attacks
                Debug.Log("Playing Axe Sound");
            }
            if (attackAxeEffect != null)
            {
                attackAxeEffect.Play(); // Play effect for axe attacks
                Debug.Log("Playing Axe Effect");
            }
        }
        else
        {
            if (attackSound != null)
            {
                attackSound.Play(); // Play default attack sound
                Debug.Log("Playing Default Attack Sound");
            }
            if (attackEffect != null)
            {
                attackEffect.Play(); // Play default attack effect
                Debug.Log("Playing Default Attack Effect");
            }
        }

        Debug.Log("Total Damage: " + totalDamage);
        return totalDamage;
    }

    // For visualization in the Editor
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }    


}
