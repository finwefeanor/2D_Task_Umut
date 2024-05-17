using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public string itemName;
    public int price;
    public Sprite itemIcon; // Icon for UI
    public Sprite itemSprite; // Sprite for character
    public GameObject player; // Reference to the player

    private ShopManager shopManager;
    private bool isPurchased = false;

    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
    }

    public void BuyItem()
    {
        if (!isPurchased && shopManager.playerGold >= price)
        {
            shopManager.playerGold -= price;
            shopManager.UpdatePlayerGold();
            EquipItem();
            isPurchased = true;
            player.GetComponent<PlayerInventory>().AddItemToInventory(itemSprite, price);
            ShowEquipUnequipButtons();
        }
    }

    //public void AddShopItem()
    //{
    //    itemButton.transform.Find("Icon").GetComponent<Image>().sprite = icon;
    //}

    void EquipItem()
    {
        player.GetComponent<PlayerInventory>().EquipItem(itemSprite);
    }

    void ShowEquipUnequipButtons()
    {
        Button equipButton = player.GetComponent<PlayerInventory>().equipButton;
        Button unequipButton = player.GetComponent<PlayerInventory>().unequipButton;
        equipButton.gameObject.SetActive(true);
        unequipButton.gameObject.SetActive(true);
    }
}
