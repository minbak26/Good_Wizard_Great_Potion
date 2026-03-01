using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static StageManager instance;
    
    public ExitArea exitArea;
    // TODO: 새롭게 시작할수있도록 플레이어, 박스등을 새롭게 로드할수 있어야 할듯.. 일단 
    
    
    void Awake()
    {
        instance = this;
        exitArea = new ExitArea();
    }

    private bool isExitTimerRunning = false;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isExitTimerRunning)
        {
            Debug.Log("ExitTimer 시작!");
            isExitTimerRunning = true;
            
            var ct = this.GetCancellationTokenOnDestroy();
            
            exitArea.ExitTimerAsync(ct).Forget();
        }
    }

    void OnTriggerExit(Collider other)
    {
        isExitTimerRunning = false;
        
    }
}

// 탈출 지역.
public class ExitArea
{
  
    public async UniTask ExitTimerAsync(CancellationToken ct)
    {
        // 5초기다리기.
        Debug.Log("5초 타이머 시작");
        try
        {
            await UniTask.Delay(5000, cancellationToken: ct);
            InventoryManager.Instance.AllItemAddtoStorage();
            // GameOver 함수는  게임다시 시작, 종료, 제작 할수있는 제작대 UI 띄운다.
            Debug.Log("Exit & ShowLobby!");
            ShowLobbyUI();
        }
        catch (System.OperationCanceledException)
        {
            Debug.Log("타이머가 취소되었습니다 (플레이어가 나갔거나 씬이 바뀌었음)");
        }
     
       
    }

  
    SceneLoader SceneLoader;

    public void ShowLobbyUI()
    {
        SceneLoader = new SceneLoader();
        SceneLoader.LoadSceneAsync(SceneType.Lobby); 
        
    }
}
