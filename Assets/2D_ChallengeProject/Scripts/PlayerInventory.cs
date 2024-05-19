using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public SpriteRenderer bodyRenderer; // Reference to the player's body sprite renderer
    public SpriteRenderer clothesRenderer; // Reference to the clothing sprite renderer
    public Sprite defaultSprite; // Default sprite when no item is equipped
    public Animator animator; // Reference to the player's Animator

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public GameObject inventoryGridPanel;  // Reference to the grid panel in the UI
    public GameObject inventoryItemPrefab;  // Assign this in the Inspector

    private InventoryItem currentlyEquippedItem = null;
    public Transform hatAttachmentPoint; //for placing the hat etc
    public Transform weaponAttachmentPoint;

    private InventoryItem currentlyEquippedClothes;

    public InventoryItem CurrentlyEquippedHat { get; private set; }
    public InventoryItem CurrentlyEquippedWeapon { get; private set; }

    private GameObject currentlyEquippedAccessory;

    public Sprite hatSprite;
    public Sprite armorSprite;
    public Sprite axeSprite;
    private GameObject currentlyEquippedHat = null;
    private GameObject currentlyEquippedWeapon = null;



    public int inventoryCapacity = 4; 
   

    void Start()
    {

        // These items are added to the player's inventory directly for testing purposes or initial setup
        // normally idea is player starts without anything

        //AddItemToInventory(hatSprite, 10, InventoryItem.ItemType.Hat);  // Example price of 10
        //AddItemToInventory(armorSprite, 20, InventoryItem.ItemType.Clothes);  // Example price of 20
        //AddItemToInventory(axeSprite, 15, InventoryItem.ItemType.Weapon);  // Example price of 15


        // Set the default sprite
        bodyRenderer.sprite = defaultSprite;

        // Ensure clothrenderer is off at the beginning, so only player animation layer will work.
        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 0);
        
        // Deactivate the clothes renderer initially
        clothesRenderer.gameObject.SetActive(false);
 
    }


    public void AddItemToInventory(Sprite sprite, int purchasePrice, InventoryItem.ItemType itemType)
    {
        Debug.Log("Adding item to inventory");
        InventoryItem newItem = new InventoryItem(sprite, purchasePrice, itemType);
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

        // Instantiate a prefab clone for each item in the inventory. When we buy stuff or collect
        foreach (InventoryItem item in inventoryItems)
        {
            GameObject itemIcon = Instantiate(inventoryItemPrefab, inventoryGridPanel.transform);
            itemIcon.GetComponent<Image>().sprite = item.sprite; // Set the item sprite

            // Configures the sell button
            Button sellButton = itemIcon.transform.Find("SellButton").GetComponent<Button>();
            if (sellButton != null)
            {
                sellButton.onClick.AddListener(() => SellItem(item));
            }
            else
            {
                Debug.LogError("Sell Button not found in the Inventory Item Prefab!");
            }

            //configures the equip button
           Button equipButton = itemIcon.transform.Find("EquipButton").GetComponent<Button>();
            if (equipButton != null)
            {
                equipButton.onClick.AddListener(() => EquipItem(item));
            }
        }
    }


    public void SellItem(InventoryItem item)
    {
        // later, add a flag here or other checks that player
        // can sell stuff only when he is nearby shopkeeper's OnTriggerEnter

        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
            if (item == currentlyEquippedClothes)
            {
                UnequipClothes();
            }
            else if (item == CurrentlyEquippedHat)
            {
                UnequipAccessory(hatAttachmentPoint);
            }
            else if (item == CurrentlyEquippedWeapon)
            {
                
                UnequipAccessory(weaponAttachmentPoint);
            }
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




    //----------------------------------
    //public void EquipItem(InventoryItem item) //new equip method
    //{
    //    // Set the new item sprite to the clothing renderer
    //    clothesRenderer.sprite = item.sprite;
    //    clothesRenderer.gameObject.SetActive(true);

    //    animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 1); // Activates the clothing layer

    //    Debug.Log("Equipped: " + item.sprite.name);
    //}

    //------------------------------------

    //public void EquipItem(InventoryItem item) //newest equip method working properly
    //{
    //    Debug.Log($"Trying to equip: {item.sprite.name}, Currently equipped: {(currentlyEquippedItem != null ? currentlyEquippedItem.sprite.name : "None")}");

    //    if (currentlyEquippedItem == item)
    //    {
    //        Debug.Log("Unequipping from Inventory");
    //        // Unequip the item
    //        clothesRenderer.gameObject.SetActive(false);
    //        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 0);
    //        currentlyEquippedItem = null;
    //    }
    //    else
    //    {
    //        Debug.Log("Equipping from Inventory");
    //        // Equip the new item
    //        clothesRenderer.sprite = item.sprite;
    //        clothesRenderer.gameObject.SetActive(true);
    //        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 1);
    //        currentlyEquippedItem = item;
    //    }
    //}

    public void EquipItem(InventoryItem item)
    {
        switch (item.type)
        {
            case InventoryItem.ItemType.Clothes:                
                if (currentlyEquippedClothes == item)
                {
                    UnequipClothes();
                }
                else
                {
                    EquipClothes(item);
                }
                break;
            case InventoryItem.ItemType.Hat:
                if (CurrentlyEquippedHat == item)
                {
                    UnequipAccessory(hatAttachmentPoint);
                }
                else
                {
                    EquipAccessory(item, hatAttachmentPoint);
                }
                break;

            case InventoryItem.ItemType.Weapon:
                if (CurrentlyEquippedWeapon == item)
                {
                    UnequipAccessory(weaponAttachmentPoint);
                }
                else
                {
                    EquipAccessory(item, weaponAttachmentPoint);
                }
                break;
        }
    }

    void EquipClothes(InventoryItem item)
    {
        Debug.Log("Equipping clothes: " + item.sprite.name);
        clothesRenderer.sprite = item.sprite;
        clothesRenderer.gameObject.SetActive(true);
        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 1);
        currentlyEquippedClothes = item;
    }

    void UnequipClothes()
    {
        Debug.Log("Unequipping clothes");
        clothesRenderer.gameObject.SetActive(false);
        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 0);
        currentlyEquippedClothes = null;
    }

    void EquipAccessory(InventoryItem item, Transform attachmentPoint)
    {
        GameObject currentlyEquipped = null;
        if (attachmentPoint == hatAttachmentPoint)
        {
            currentlyEquipped = currentlyEquippedHat;
        }
        else if (attachmentPoint == weaponAttachmentPoint)
        {
            currentlyEquipped = currentlyEquippedWeapon;
        }

        // Toggle equip/unequip
        if (currentlyEquipped != null)
        {
            Destroy(currentlyEquipped);
            CurrentlyEquippedWeapon = null; 
            Debug.Log("Unequipping accessory: " + item.sprite.name);
            if (attachmentPoint == hatAttachmentPoint)
            {
                currentlyEquippedHat = null;
                CurrentlyEquippedHat = null;
            }
            else if (attachmentPoint == weaponAttachmentPoint)
            {
                currentlyEquippedWeapon = null;
                CurrentlyEquippedWeapon = null;
                Debug.Log("Weapon unequipped: " + item.sprite.name);
            }
        }
        else
        {
            GameObject accessory = new GameObject(item.sprite.name);
            SpriteRenderer renderer = accessory.AddComponent<SpriteRenderer>();
            renderer.sprite = item.sprite;
            renderer.sortingOrder = 25;
            accessory.transform.SetParent(attachmentPoint);

            accessory.layer = LayerMask.NameToLayer("Player"); //// Set the layer so we can see them inside the castle

            if (attachmentPoint == hatAttachmentPoint)
            {
                accessory.transform.localPosition = new Vector3(0.0f, -0.15f, 1);
                accessory.transform.localScale = new Vector3(0.55f, 0.55f, 1);
                currentlyEquippedHat = accessory;
                CurrentlyEquippedHat = item;
                Debug.Log("Hat equipped: " + item.sprite.name);
            }
            else if (attachmentPoint == weaponAttachmentPoint)
            {
                accessory.transform.localPosition = new Vector3(0.085f, 0.085f, 0);
                accessory.transform.localScale = new Vector3(0.15f, 0.15f, 1);
                currentlyEquippedWeapon = accessory;
                CurrentlyEquippedWeapon = item;
                Debug.Log("Weapon equipped: " + item.sprite.name);
            }
            Debug.Log("Equipping accessory: " + item.sprite.name);
        }
    }


    // Debug.Log("Equipping accessory: " + item.sprite.name);




    void UnequipAccessory(Transform attachmentPoint)
    {
        Debug.Log("Unequipping accessory"); 
        if (attachmentPoint.childCount > 0)
        {
            Destroy(attachmentPoint.GetChild(0).gameObject); // This removes the items on top of character visually
        }

        // Reset specific accessory type based on the attachment point
        if (attachmentPoint == hatAttachmentPoint)
        {
            currentlyEquippedHat = null;   // Reset GameObject tracking for hat
            CurrentlyEquippedHat = null;  // Reset InventoryItem tracking for hat
            Debug.Log("Hat unequipped");
        }
        else if (attachmentPoint == weaponAttachmentPoint)
        {
            //Destroy(currentlyEquippedWeapon);
            CurrentlyEquippedWeapon = null;
            currentlyEquippedWeapon = null;
            Debug.Log("Weapon fully unequipped");
        }
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
