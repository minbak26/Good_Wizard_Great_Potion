using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxUIManger : MonoBehaviour
{
    public static BoxUIManger Instance;

    [Header("UI Objects")]
    public GameObject interactionTextUI; 
    public GameObject boxInventoryUI;    
    public Image[] uiSlots;              

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        CloseBoxUI();
        interactionTextUI.SetActive(false);

        // // 💡 [해결 2] 시작할 때 버튼 클릭 이벤트를 자동으로 연결해 줌
        // for (int i = 0; i < uiSlots.Length; i++)
        // {
        //     int index = i; // 클로저(Closure) 문제 방지를 위해 지역 변수로 복사
        //     Button btn = uiSlots[i].GetComponent<Button>();
        //     
        //     if (btn != null)
        //     {
        //         // 기존 연결을 비우고 새로 연결
        //         btn.onClick.RemoveAllListeners();
        //         btn.onClick.AddListener(() => OnSlotClick(index));
        //     }
        //     else
        //     {
        //         Debug.LogWarning($"⚠️ 슬롯 {i}번에 Button 컴포넌트가 없습니다! 인스펙터에서 Add Component로 Button을 추가해주세요.");
        //     }
        // }
    }

    public void SetInteractionText(bool isActive)
    {
        interactionTextUI.SetActive(isActive);
    }

    private List<ItemData> currentOpenedItems; 
    
    public void OpenBoxUI(List<ItemData> items)
    {
        
        
        currentOpenedItems = items; 
        boxInventoryUI.SetActive(true);
        interactionTextUI.SetActive(false); 

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

    public void CloseBoxUI()
    {
        boxInventoryUI.SetActive(false);
    }
    
    // 일단 클릭을 했을때 해당 슬롯의 정보를 알아야 한다.
    // 아는지 확인가능 한 디버깅 로그를 만들어 보자.
    
    public void OnSlotClick(int index)
    {
        Debug.Log("<color=red>버튼 클릭 감지됨!</color>"); // 이 로그가 찍히는지 확인
        // 1. 안전 장치: 데이터가 없거나 인덱스가 범위를 벗어나면 무시
        if (currentOpenedItems == null || index >= currentOpenedItems.Count) 
        {
            Debug.LogWarning($"⚠️ 슬롯 {index}번에 데이터가 없습니다.");
            return;
        }

        ItemData selectedItem = currentOpenedItems[index]; 
        Debug.Log($"<color=cyan>✨ 클릭 성공!</color> 선택된 아이템: <b>{selectedItem.name}</b> \n 선택된 아이템 개수.: {selectedItem.itemAmount}");

        // 2. [중요] 인벤토리가 아직 없으므로 로그만 남깁니다.
        Debug.Log($"{selectedItem.name}이(가) 가상의 인벤토리로 이동했습니다.");
        // 이제 인벤토리로 옮기는 작업을 해봅시다.
        // 어떻게 할까. seletceditem을 보내야하나.
        
        InventoryManager.Instance.AddItem(selectedItem);

        // 3. 박스 데이터에서 해당 아이템 삭제
        currentOpenedItems.RemoveAt(index);

        // 4. UI 새로고침 (아이템이 사라진 상태로 다시 그려짐)
        OpenBoxUI(currentOpenedItems);
    }
    
    public bool IsBoxUIOpen()
    {
        return boxInventoryUI.activeSelf;
    }
}