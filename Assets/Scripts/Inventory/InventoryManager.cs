using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;

    // 인벤토리에 담긴 아이템 리스트
    public List<ItemData> playerItems = new List<ItemData>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // 아이템 추가 함수
    public void AddItem(ItemData item)
    {
        playerItems.Add(item);
        Debug.Log($"🎒 인벤토리 추가: {item.itemName} (현재 총 {playerItems.Count}개)");
    }
}
