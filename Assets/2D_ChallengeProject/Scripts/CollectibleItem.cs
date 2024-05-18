using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public Sprite itemSprite;
    public int sellPrice;
    public InventoryItem.ItemType itemType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add to player's inventory
            other.GetComponent<PlayerInventory>().AddItemToInventory(itemSprite, sellPrice, itemType);
            gameObject.SetActive(false);  // Disable the collectible object
        }
    }
}

