
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
        if (BoxUIManger.Instance.IsBoxUIOpen())
        {
            return;
        }
        
        Debug.Log("BoxInteration: OnTriggerEnter");
        if (other.tag == "Player")
        {
            isPlayerRange = true;
            BoxUIManger.Instance.SetInteractionText(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (BoxUIManger.Instance.IsBoxUIOpen())
        {
            return;
        }
        isPlayerRange = false;
        // 매니저야, 안내 문구랑 박스 창 다 꺼줘
        BoxUIManger.Instance.SetInteractionText(false);
        BoxUIManger.Instance.CloseBoxUI();
    }

    void Update()
    {
        if (!isPlayerRange) return;

        // F키를 눌렀을 때만 작동하게 함
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            // UI가 꺼져있다면 연다
            if (!BoxUIManger.Instance.IsBoxUIOpen())
            {
                BoxUIManger.Instance.OpenBoxUI(myItems);
            }
            // UI가 켜져있다면 닫는다 (오직 여기서만 Close가 실행되어야 함)
            else
            {
                BoxUIManger.Instance.CloseBoxUI();
                BoxUIManger.Instance.SetInteractionText(true);
            }
        }
    }
       
        
      
    }
    
    

