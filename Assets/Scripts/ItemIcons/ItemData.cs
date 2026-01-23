using UnityEngine;
public enum Rarity  { Common, Rare, Epic
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public int itemID;
    public int itemAmount;
    public Rarity rarity; // 희귀도.
    public Sprite icon;
    public bool IsStackable { get; set; }
    public string ItemName { get; set; }
    public int ItemID { get; set; }
}
