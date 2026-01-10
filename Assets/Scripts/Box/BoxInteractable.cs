using UnityEngine;

public class BoxInteractable : MonoBehaviour
{
    private BoxManager boxManager;
    
    void Start()
    {
        // 씬에서 BoxManager 찾기
        boxManager = FindObjectOfType<BoxManager>();
        
        if (boxManager == null)
        {
            Debug.LogError("BoxManager를 찾을 수 없습니다!");
        }
    }
    
    /// <summary>
    /// 플레이어가 박스와 상호작용 시 호출
    /// </summary>
    public void Interact()
    {
        if (boxManager != null)
        {
            boxManager.OnBoxInteracted(gameObject);
            
            // 박스의 아이템들을 인벤토리에 추가
            var items = boxManager.GetBoxItems(gameObject);
            
            // 플레이어 인벤토리에 아이템 추가 로직
            AddItemsToPlayerInventory(items);
            
            // 박스 제거
            boxManager.DestroyBox(gameObject);
        }
    }
    
    /// <summary>
    /// 플레이어 인벤토리에 아이템 추가
    /// </summary>
    private void AddItemsToPlayerInventory(System.Collections.Generic.List<InventorySlot> items)
    {
        // 플레이어 인벤토리 참조 (추후 구현)
        InventoryManager playerInventory = FindObjectOfType<InventoryManager>();
        
        if (playerInventory == null)
        {
            Debug.LogWarning("플레이어 인벤토리를 찾을 수 없습니다!");
            return;
        }
        
        foreach (InventorySlot slot in items)
        {
            playerInventory.AddItem(slot.slotItemData, slot.amount);
        }
        
        Debug.Log("박스의 모든 아이템이 인벤토리에 추가되었습니다!");
    }
}
