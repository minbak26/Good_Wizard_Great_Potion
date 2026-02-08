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
    public bool IsStackable;
    public int maxStackAmount;
    
    
}

[System.Serializable]
public class ItemStack
{
    public ItemData data;
    public int amount;
}
