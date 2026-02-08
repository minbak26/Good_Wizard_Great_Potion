
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class BoxInteration : MonoBehaviour
{
    // UI 프리팹 : F키보드 모양 만들기.
    [SerializeField] private GameObject interactionUI;
    [FormerlySerializedAs("BoxUI")] [SerializeField] public GameObject boxUI;
    private bool isPlayerRange = false;

    private InputAction interaction;

    // UI처리는 싱글톤으로1
    public static BoxInteration instance;
    private void Awake()
    {
        // UI처리는 싱글톤으로2
        if (instance == null)
        {
            instance = this;
        }
        interactionUI.SetActive(false);
        boxUI.SetActive(false);
    }

    
    // 박스 요소 이미지 처리.
    public Image[] uiSlots;
    
    public void UpdateBoxUI(List<ItemData> selectedItems)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            if (i < selectedItems.Count) // 뽑힌 아이템 개수 안쪽일 때
            {
                uiSlots[i].sprite = selectedItems[i].icon; // ScriptableObject의 스프라이트 할당
                uiSlots[i].gameObject.SetActive(true);        // 슬롯 켜기
            }
            else // 남는 슬롯
            {
                uiSlots[i].gameObject.SetActive(false);       // 슬롯 끄기
            }
        }
    }


    private PlayerMove playerBoxInteration = new PlayerMove();
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BoxInteration: OnTriggerEnter");
        if (other.tag == "Player")
        {
            isPlayerRange = true;
            interactionUI.SetActive(true);
        
          

        }
        

       
    }

    

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("BoxInteration: OnTriggerExit");
        isPlayerRange = false;
        interactionUI.SetActive(false);
        boxUI.SetActive(false);
    }

    void Update()
    {
        if (!isPlayerRange) return;
        
        if (Keyboard.current.fKey.wasPressedThisFrame && isPlayerRange)
        {
            Debug.Log("Fkey was pressed");
            boxUI.SetActive(!boxUI.activeSelf);
        }
       
        
      
    }
    
    
}
