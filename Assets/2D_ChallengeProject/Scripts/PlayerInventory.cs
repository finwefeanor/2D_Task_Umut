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
    public Button equipButton; // Reference to the Equip button
    public Button unequipButton; // Reference to the Unequip button

    void Start()   {
        

        // Set the default sprite initially
        bodyRenderer.sprite = defaultSprite;
        // Ensure the CharWithCloth layer is initially inactive
        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 0);
        // Deactivate the clothes renderer initially
        clothesRenderer.gameObject.SetActive(false);
        // Hide the Equip and Unequip buttons initially
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(false);
    }

    public void EquipItem(Sprite newItem)
    {
        // Set the new item sprite to the clothing renderer
        clothesRenderer.sprite = newItem;
        clothesRenderer.gameObject.SetActive(true);

        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 1); // Activates the clothing layer
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
