using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class BoxUIManger : MonoBehaviour
{
    // 
    public static BoxUIManger Instance;

    [Header("UI Objects")]
    public GameObject interactionTextUI; // "F키를 누르세요" 텍스트
    public GameObject boxInventoryUI;    // 아이템 슬롯이 있는 창
    public Image[] uiSlots;              // 슬롯 이미지들

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // 시작할 때 다 꺼두기
        CloseBoxUI();
        interactionTextUI.SetActive(false);
    }

    // 1. "F키를 누르세요" 켜고 끄기
    public void SetInteractionText(bool isActive)
    {
        interactionTextUI.SetActive(isActive);
    }

    
    private List<ItemData> currentOpenedItems; // 현재 박스에서 보여주고 있는 아이템들
    
    // 2. 박스 내용물 보여주기 (스프라이트 가져오는 핵심 부분)
    public void OpenBoxUI(List<ItemData> items)
    {
        currentOpenedItems = items; // 데이터 보관
        boxInventoryUI.SetActive(true);
        interactionTextUI.SetActive(false); 

        for (int i = 0; i < uiSlots.Length; i++)
        {
            // 1. 슬롯 오브젝트는 항상 켜둡니다 (가로 늘어남 방지 핵심)
            uiSlots[i].gameObject.SetActive(true);

            if (i < items.Count)
            {
                // 아이템이 있는 경우
                uiSlots[i].sprite = items[i].icon; 
            
                Debug.Log($"✅ 슬롯 {i}번에 {items[i].name} 아이콘 등록 완료");
            }
            else
            {
                // 아이템이 없는 경우 (남는 슬롯)
                uiSlots[i].sprite = null;
            
                // 2. 하얀 화면 방지: 이미지를 투명하게(Alpha = 0) 만들어 가립니다.
                uiSlots[i].color = new Color(1, 1, 1, 0); 
                
                Button btn = uiSlots[i].GetComponent<Button>();
                if(btn != null) 
                {
                    Debug.Log($"{i}번째 아이템 클릭.");
                    btn.interactable = false; // 빈 슬롯은 클릭 불가
                }   
            }
        }
    }

    // 3. UI 닫기
    public void CloseBoxUI()
    {
        boxInventoryUI.SetActive(false);
    }
    
    // 4.슬롯 클릭시 실행될 함수.(인스펙터 연결 or 코드로 연결)
    public void OnSlotClick(int index)
    {
        if (currentOpenedItems == null || index >= currentOpenedItems.Count) return;

        ItemData selectedItem = currentOpenedItems[index];

        // 1. 인벤토리에 추가
        InventoryManager.Instance.AddItem(selectedItem);

        // 2. 박스 데이터에서 삭제
        currentOpenedItems.RemoveAt(index);

        // 3. UI 새로고침 (아이템이 하나 사라졌으니 다시 그려줌)
        OpenBoxUI(currentOpenedItems);
    }
    
    // 현재 UI가 켜져있는지 확인용
    public bool IsBoxUIOpen()
    {
        return boxInventoryUI.activeSelf;
    }
}
