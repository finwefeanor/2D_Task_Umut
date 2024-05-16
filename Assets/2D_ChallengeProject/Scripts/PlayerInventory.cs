using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public SpriteRenderer bodyRenderer; // Reference to the player's body sprite renderer
    public SpriteRenderer clothesRenderer; // Reference to the clothing sprite renderer
    public Sprite defaultSprite; // Default sprite when no item is equipped
    public Animator animator; // Reference to the player's Animator

    void Start()
    {
        

        // Set the default sprite initially
        bodyRenderer.sprite = defaultSprite;
        // Ensure the CharWithCloth layer is initially inactive
        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 0);

        clothesRenderer.gameObject.SetActive(false);
    }

    public void EquipItem(Sprite newItem)
    {
        // Set the new item sprite to the clothing renderer
        clothesRenderer.sprite = newItem;
        clothesRenderer.gameObject.SetActive(true);
        // Activate the CharWithCloth layer
        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 1); // Activate the clothing layer
    }

    public void UnequipItem()
    {
        // Clear the clothing sprite
        clothesRenderer.sprite = null;
        clothesRenderer.gameObject.SetActive(false);
        //bodyRenderer.sprite = defaultSprite;
        animator.SetLayerWeight(animator.GetLayerIndex("CharWithCloth"), 0); // Deactivate the clothing layer
    }
}
