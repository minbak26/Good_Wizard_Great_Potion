using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;

    // 플레이어가 보유한 아이템 리스트
    public List<ItemData> playerItems = new List<ItemData>();
    
    // 인벤토리 UI (나중에 만들어서 연결할 예정)
    

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        // 씬이 바뀌어도 유지하고 싶다면 주석 해제
         DontDestroyOnLoad(gameObject);
         
    }

    // 아이템 추가 함수
    public void AddItem(ItemData item)
    {
        if (item == null) return;

        playerItems.Add(item);
        Debug.Log($"📦 인벤토리 추가: {item.name} (현재 총 {playerItems.Count}개)");

        // UI 업데이트 호출 (나중에 UI 완성 후 연결)
        //inventoryUI;
    }

    // 아이템 삭제 함수 (버리거나 조합할 때 사용)
    // 아마 조합할때만 사용할듯.
    public void RemoveItem(ItemData item)
    {
        if (playerItems.Contains(item))
        {
            Debug.Log($"인벤토리 삭제 : {item.name} (현재 총 {{playerItems.Count}}개)");
            playerItems.Remove(item);
            //InventoryUIManager.instance.UpdateInventoryUI();
        }
    }
    
    // 사망시 모든 아이템 삭제
    public void AllItemDelete()
    {
        playerItems.Clear();
    }
    
}
