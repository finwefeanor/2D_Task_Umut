using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public enum ItemType
    {
        Clothes,
        Hat,
        Weapon,
        // Add more later
    }

    public Sprite sprite;
    public int purchasePrice;
    public ItemType type;

    public InventoryItem(Sprite sprite, int purchasePrice, ItemType type)
    {
        this.sprite = sprite;
        this.purchasePrice = purchasePrice;
        this.type = type;
    }
}

