using UnityEngine;
using TMPro; // Make sure you have TextMeshPro package installed

public class NPCTextHandler : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI npcText;
    public float detectionRadius = 3.0f;
    public string message; // The message to display

    private void Start()
    {
        // Ensure the text is hidden at the start
        npcText.text = message;
        npcText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Show or hide the text based on the player's proximity
        if (Vector3.Distance(player.transform.position, transform.position) <= detectionRadius)
        {
            Debug.Log("Player is near");
            npcText.gameObject.SetActive(true);
        }
        else
        {
            npcText.gameObject.SetActive(false);
        }
    }
}
