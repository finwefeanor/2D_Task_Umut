using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayController : MonoBehaviour
{
    public GameObject inventoryCanvas;  // Assign this in the Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {  // Check if 'I' is pressed
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);  // Toggle the visibility of the inventory
        }
    }
}
