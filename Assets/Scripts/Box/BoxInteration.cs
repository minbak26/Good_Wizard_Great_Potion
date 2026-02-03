using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static Cysharp.Threading.Tasks.UniTask;

public class BoxInteration : MonoBehaviour
{
    // UI 프리팹 : E키보드 모양 만들기.
    [SerializeField] private GameObject interactionUI;
    
    private bool isPlayerRange = false;

    private void Awake()
    {
        interactionUI.SetActive(false);
    }

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
        
    }

    void Update()
    {
        if (!isPlayerRange) return;
        
    }
    
    
}
