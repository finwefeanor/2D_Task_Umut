using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class InventoryItem
//{
//    public Sprite sprite;
//    public int purchasePrice;

//    public InventoryItem(Sprite sprite, int purchasePrice)
//    {
//        this.sprite = sprite;
//        this.purchasePrice = purchasePrice;
//    }
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}

public class InventoryItem
{
    public enum ItemType
    {
        Clothes,
        Hat,
        Weapon,
        // Add more if needed
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

