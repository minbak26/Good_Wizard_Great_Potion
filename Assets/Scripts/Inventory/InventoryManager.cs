using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    //인벤토리에 필요한 기능
    // 이전에 만들었던 InventorySlot을 참조해서 리스트 생성.
    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    int maxSlot = 28;

     public bool AddItem(ItemData itemData, int amount = 1)
    {
        // 1. 유효성 검사
        if (itemData == null || amount <= 0)
        {
            Debug.LogWarning("유효하지 않은 아이템입니다.");
            return false;
        }
        
        // 2. 같은 아이템이 있는지 찾기 (스택 가능한 경우)
        if (itemData.IsStackable)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                // 같은 아이템 ID를 찾음
                if (slot.slotItemData.ItemID == itemData.ItemID)
                {
                    slot.AddAmount(amount);
                    Debug.Log($"{itemData.ItemName} {amount}개 추가됨. (현재: {slot.amount}개)");
                    return true;
                }
            }
        }
        
        // 3. 새로운 슬롯 생성
        int maxSlots = 0;
        if (inventorySlots.Count < maxSlots)
        {
            InventorySlot newSlot = new InventorySlot(itemData, amount);
            inventorySlots.Add(newSlot);
            Debug.Log($"{itemData.ItemName} {amount}개 추가됨. (슬롯: {inventorySlots.Count})");
            return true;
        }
        
        // 4. 인벤토리 가득 참
        Debug.LogWarning("인벤토리가 가득 찼습니다!");
        return false;
    }
    
    // ==================== 아이템 제거 ====================
    public bool RemoveItem(int itemID, int amount = 1)
    {
        // 1. 해당 아이템 찾기
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.slotItemData.ItemID == itemID)
            {
                // 2. 개수 감소 시도
                if (slot.RemoveAmount(amount))
                {
                    Debug.Log($"아이템 제거됨. (남은 개수: {slot.amount}개)");
                    
                    // 3. 빈 슬롯 제거
                    if (slot.IsEmpty())
                    {
                        inventorySlots.Remove(slot);
                        Debug.Log("빈 슬롯이 제거되었습니다.");
                    }
                    
                    return true;
                }
                else
                {
                    Debug.LogWarning("제거할 개수가 부족합니다!");
                    return false;
                }
            }
        }
        
        Debug.LogWarning("해당 아이템을 찾을 수 없습니다!");
        return false;
    }
    
    // ==================== 슬롯 인덱스로 제거 ====================
    public bool RemoveItemAtSlot(int slotIndex, int amount = 1)
    {
        if (slotIndex < 0 || slotIndex >= inventorySlots.Count)
        {
            Debug.LogWarning("유효하지 않은 슬롯입니다!");
            return false;
        }
        
        InventorySlot slot = inventorySlots[slotIndex];
        
        if (slot.RemoveAmount(amount))
        {
            if (slot.IsEmpty())
            {
                inventorySlots.RemoveAt(slotIndex);
            }
            return true;
        }
        
        return false;
    }
    
    // ==================== 전체 인벤토리 조회 ====================
    public void PrintInventory()
    {
        Debug.Log("=== 인벤토리 ===");
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Debug.Log($"[슬롯 {i}] {slot.slotItemData.ItemName} x {slot.amount}");
        }

        string maxSlots = null;
        Debug.Log($"사용 슬롯: {inventorySlots.Count} / {maxSlots}");
    }
    
    // ==================== 특정 아이템 개수 확인 ====================
    public int GetItemCount(int itemID)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.slotItemData.ItemID == itemID)
            {
                return slot.amount;
            }
        }
        return 0;
    }
   
    
    
    
}

