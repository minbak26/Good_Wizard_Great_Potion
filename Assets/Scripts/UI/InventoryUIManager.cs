using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    // 인벤토리, UI모두 싱글톤으로.
    public static InventoryUIManager instance;
    
    [Header("Inventory UI")]
    public GameObject inventoryUI;
    public Image[] uiSlots;

    // 현재 아이템.
    private List<ItemData> currentOpenedItems;
    
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        // 초기에는 꺼지다가.
        inventoryUI.SetActive(false);
    }

    public void OpenInventoryUI(List<ItemData> items)
    {
        inventoryUI.SetActive(true);
        
        
        
    }

    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            OpenInventoryUI(currentOpenedItems);
        }
        
        
    }

    // 어떻게 UI 업데이트를 할가?
    // 슬롯 전부 등록하고, 순서대로 채워가기 만약 같은 아이템이 잇다면 하위의 숫자++하기.
    int uiSlotIndex = 0;
    public void UpdateInventoryUI(ItemData items)
    {
        // 순회후 없다면 새로운 인덱스에 추가하도록
        for (int i = 0; i < uiSlots.Length; i++)
        {
            //  근데 처음엔 굳이 순회를 할필요가 없음..
            // 일단 슬롯의 이름과 비교를 해보자.
            // 근데 또 처음엔 아무것도 이름이 없는데 흠..
            if (uiSlots[i].name == items.itemName)
            {
                
            }
        }
        uiSlots[uiSlotIndex].sprite = items.icon;
        items.itemAmount++;
    }

    public bool IsInventoryOpen()
    {
        OpenInventoryUI(currentOpenedItems);
        return inventoryUI.activeSelf;
    }
    
    
    
    
    
}
