using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI playerGoldText; // 
    public int playerGold = 100;
    public GameObject itemButtonPrefab; // Prefab for the item button
    public Transform shopContent; // Content parent for the shop items

    // These might be missing or not initialized correctly
    public Sprite itemIcon; // Example sprite for icon, should be assigned in inspector
    public Sprite itemSprite; // Example sprite for item, should be assigned in inspector
    public Sprite hatIcon;
    public Sprite shirtIcon;
    public Sprite hatItemSprite;
    public Sprite shirtItemSprite;
    public Sprite axeIcon;
    public Sprite axeItemSprite;


    void Start()
    {
        UpdatePlayerGold();
        PopulateShop();
    }

    public void UpdatePlayerGold()
    {
        playerGoldText.text = "Your Gold: " + playerGold.ToString();
    }

    void PopulateShop()
    {
        // Example items
        AddShopItem("Hat", 20, hatIcon, hatItemSprite);
        AddShopItem("Armor", 30, shirtIcon, shirtItemSprite);
        AddShopItem("Axe", 20, axeIcon, axeItemSprite);
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
        UpdatePlayerGold();
    }

    void AddShopItem(string name, int price, Sprite icon, Sprite sprite)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab, shopContent);
        ShopItem shopItem = itemButton.GetComponent<ShopItem>();
        shopItem.itemName = name;
        shopItem.price = price;
        shopItem.itemIcon = icon;
        shopItem.itemSprite = sprite;
        shopItem.player = FindObjectOfType<PlayerController>().gameObject;

        // Correctly find and set the image
        Image itemImage = itemButton.GetComponentInChildren<Image>(); // Make sure this references the correct Image component
        if (itemImage != null)
        {
            itemImage.sprite = icon;
        }
        else
        {
            Debug.LogError("No Image component found on the item button prefab!");
        }

        // Set the text
        TextMeshProUGUI itemText = itemButton.GetComponentInChildren<TextMeshProUGUI>();
        if (itemText != null)
        {
            itemText.text = name + " - " + price + "G";
        }
        else
        {
            Debug.LogError("No TextMeshProUGUI component found on the item button prefab!");
        }

        // Add the BuyItem method to the button's onClick event
        Button button = itemButton.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(shopItem.BuyItem);
        }
        else
        {
            Debug.LogError("No Button component found on the item button prefab!");
        }
    }
}
