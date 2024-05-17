using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public SpriteRenderer bodyRenderer; // Reference to the player's body sprite renderer
    public SpriteRenderer clothesRenderer; // Reference to the clothing sprite renderer
    public Sprite defaultSprite; // Default sprite when no item is equipped
    public Animator animator; // Reference to the player's Animator
    //public Button equipButton; // Reference to the Equip button
    //public Button unequipButton; // Reference to the Unequip button
    //public List<Sprite> inventoryItems = new List<Sprite>(); // Holds inventory items
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public GameObject inventoryGridPanel;  // Reference to the grid panel in the UI
    public GameObject inventoryItemPrefab;  // Assign this in the Inspector


    public int inventoryCapacity = 4; // Example capacity
   

    void Start()
    {
        

        // Set the default sprite initially
        bodyRenderer.sprite = defaultSprite;
        // Ensure the CharWithCloth layer is initially inactive
        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 0);
        // Deactivate the clothes renderer initially
        clothesRenderer.gameObject.SetActive(false);
        // Hide the Equip and Unequip buttons initially
        //equipButton.gameObject.SetActive(false);
        //unequipButton.gameObject.SetActive(false);
    }


    public void AddItemToInventory(Sprite itemSprite, int price)
    {
        Debug.Log("Adding item to inventory");
        InventoryItem newItem = new InventoryItem(itemSprite, price);
        inventoryItems.Add(newItem);
        UpdateInventoryUI();
    }

    public void RemoveItemFromInventory(InventoryItem item)
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {
        // Clear existing inventory items
        foreach (Transform child in inventoryGridPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a prefab for each item in the inventory
        foreach (InventoryItem item in inventoryItems)
        {
            GameObject itemIcon = Instantiate(inventoryItemPrefab, inventoryGridPanel.transform);
            itemIcon.GetComponent<Image>().sprite = item.sprite; // Set the item sprite

            // Configure the sell button
            Button sellButton = itemIcon.transform.Find("SellButton").GetComponent<Button>();
            if (sellButton != null)
            {
                sellButton.onClick.AddListener(() => SellItem(item));
            }
            else
            {
                Debug.LogError("Sell Button not found in the Inventory Item Prefab!");
            }

            //Optionally configure the equip button
           Button equipButton = itemIcon.transform.Find("EquipButton").GetComponent<Button>();
            if (equipButton != null)
            {
                equipButton.onClick.AddListener(() => EquipItem(item));
            }
        }
    }


    public void SellItem(InventoryItem item)
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
            int sellPrice = Mathf.FloorToInt(item.purchasePrice * 0.5f); // Sells item as half of the purchase price.
            FindObjectOfType<ShopManager>().AddGold(sellPrice);
            UpdateInventoryUI();
        }
    }



    //public void EquipItem(Sprite newItem) // old equip method for just armor
    //{
    //    // Set the new item sprite to the clothing renderer
    //    clothesRenderer.sprite = newItem;
    //    clothesRenderer.gameObject.SetActive(true);

    //    animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 1); // Activates the clothing layer
    //}

    public void EquipItem(InventoryItem item) //new equip method
    {
        // Set the new item sprite to the clothing renderer
        clothesRenderer.sprite = item.sprite;
        clothesRenderer.gameObject.SetActive(true);

        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 1); // Activates the clothing layer

        Debug.Log("Equipped: " + item.sprite.name);
    }

    public void EquipAccessory(InventoryItem item, Transform attachmentPoint)
    {
        GameObject accessory = new GameObject(item.sprite.name);
        SpriteRenderer renderer = accessory.AddComponent<SpriteRenderer>();
        renderer.sprite = item.sprite;
        accessory.transform.SetParent(attachmentPoint);
        accessory.transform.localPosition = Vector3.zero; // Adjust this based on exact placement needs
    }



    public void UnequipItem()
    {
        Debug.Log("Unequipping item");
        // Clear the clothing sprite
        clothesRenderer.sprite = null;
        clothesRenderer.gameObject.SetActive(false);
        //bodyRenderer.sprite = defaultSprite;
        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 0); // Deactivate the clothing layer
    }

    public bool HasClothesEquipped()
    {
        Debug.Log("Checking if clothes are equipped: " + clothesRenderer.gameObject.activeSelf);
        return clothesRenderer.gameObject.activeSelf;
    }
}
