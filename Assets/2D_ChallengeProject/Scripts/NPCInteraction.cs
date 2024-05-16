using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    private bool isPlayerInRange;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    private PlayerInventory playerInventory;


    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();

        if (playerInventory == null) //is somehow we are not there 
        {
            Debug.LogError("PlayerInventory component not found in the scene.");
        }
    }


    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.SetActive(true);

            if (playerInventory != null)
            {
                if (playerInventory.HasClothesEquipped())
                {
                    dialogText.text = "You have nice clothes!";
                }
                else
                {
                    dialogText.text = "Go buy some clothes!";
                }
                Debug.Log("Dialog text set: " + dialogText.text);
            }
            else
            {
                dialogText.text = "PlayerInventory not found!";
                Debug.LogError("PlayerInventory not found!");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
