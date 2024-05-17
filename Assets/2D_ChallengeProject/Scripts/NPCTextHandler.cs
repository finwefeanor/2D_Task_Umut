using UnityEngine;
using TMPro; // Make sure you have TextMeshPro package installed

public class NPCTextHandler : MonoBehaviour
{
    //public GameObject playerGO;
    public TextMeshProUGUI npcText;
    public float detectionRadius = 3.0f;
    public string message; // The message to display
    private bool isPlayerInRange = false;
    private Transform player;

    private void Start()
    {
        // Ensure the text is hidden at the start
        npcText.text = message;
        npcText.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assumes player has the tag "Player"
        npcText.gameObject.SetActive(false); // Ensure the text is hidden initially
    }

    void FixedUpdate()
    {
        // Check player distance less frequently in FixedUpdate
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= detectionRadius && !isPlayerInRange)
        {
            Debug.Log("Player is near");
            npcText.gameObject.SetActive(true);
            isPlayerInRange = true;
        }
        else if (distance > detectionRadius && isPlayerInRange)
        {
            npcText.gameObject.SetActive(false);
            isPlayerInRange = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the Scene view for debugging purposes
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
