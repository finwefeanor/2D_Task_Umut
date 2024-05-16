using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public string itemName;
    public int price;
    public Sprite itemIcon; // Icon for UI
    public Sprite itemSprite; // Sprite for character
    public GameObject player; // Reference to the player

    private ShopManager shopManager;

    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
    }

    public void BuyItem()
    {
        if (shopManager.playerGold >= price)
        {
            shopManager.playerGold -= price;
            shopManager.UpdatePlayerGold();
            EquipItem();
        }
    }

    void EquipItem()
    {
        player.GetComponent<PlayerInventory>().EquipItem(itemSprite);
    }
}
