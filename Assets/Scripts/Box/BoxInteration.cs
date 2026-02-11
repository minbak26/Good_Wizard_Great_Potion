
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class BoxInteration : MonoBehaviour
{
    public List<ItemData> myItems;
    
    private bool isPlayerRange = false;
    
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BoxInteration: OnTriggerEnter");
        if (other.tag == "Player")
        {
            isPlayerRange = true;
            BoxUIManger.Instance.SetInteractionText(true);
        
          

        }
        

       
    }

    

    private void OnTriggerExit(Collider other)
    {
        isPlayerRange = false;
        // 매니저야, 안내 문구랑 박스 창 다 꺼줘
        BoxUIManger.Instance.SetInteractionText(false);
        BoxUIManger.Instance.CloseBoxUI();
    }

    void Update()
    {
        if (!isPlayerRange) return;

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            // 이미 열려있으면 닫고, 닫혀있으면 연다
            if (BoxUIManger.Instance.IsBoxUIOpen())
            {
                BoxUIManger.Instance.CloseBoxUI();
                BoxUIManger.Instance.SetInteractionText(true); // 다시 안내 문구 켜기
            }
            else
            {
                // ★ 매니저에게 내 아이템 리스트(myItems)를 넘겨줍니다!
                BoxUIManger.Instance.OpenBoxUI(myItems);
            }
        }
        }
       
        
      
    }
    
    

