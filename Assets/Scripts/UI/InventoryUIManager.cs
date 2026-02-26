using System.Collections.Generic;
using Unity.VisualScripting;
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

    // 현재 인벤토리 아이템.
    private List<ItemData> currentInventoryItems;
    
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
            OpenInventoryUI(currentInventoryItems);
        }
   
    }

    // 어떻게 UI 업데이트를 할가?
    // 슬롯 전부 등록하고, 순서대로 채워가기 만약 같은 아이템이 잇다면 하위의 숫자++하기.
    int uiSlotIndex = 0;
    public void UpdateInventoryUI(List<ItemData> items)
    {
        currentInventoryItems = items;
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].gameObject.SetActive(true);

            if (i < items.Count)
            {
                // 💡 [해결 1] 아이템이 있는 경우: 이미지 할당 + 불투명하게 표시 + 클릭 가능
                uiSlots[i].sprite = items[i].icon;
                uiSlots[i].color = new Color(1, 1, 1, 1);
                uiSlots[i].raycastTarget = true;

                Debug.Log($"✅ 슬롯 {i}번에 {items[i].name} 아이콘 등록 완료");
            }
            else
            {
                // 💡 [해결 1] 아이템이 없는 경우: 이미지 제거 + 투명하게 숨김 + 클릭 불가
                uiSlots[i].sprite = null;
                uiSlots[i].color = new Color(1, 1, 1, 0);
                uiSlots[i].raycastTarget = false;
            }
        }

    }

    public bool IsInventoryOpen()
    {
        OpenInventoryUI(currentInventoryItems);
        return inventoryUI.activeSelf;
    }
    
    
    
    
    
}
