using Cysharp.Threading.Tasks;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private ExitArea exitArea;
    
    void Start()
    {
        exitArea = new ExitArea();
    }

    private bool isExitTimerRunning = false;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isExitTimerRunning)
        {
            Debug.Log("ExitTimer 시작!");
            isExitTimerRunning = true;
            exitArea.ExitTimerAsync().ContinueWith(()=> isExitTimerRunning = false);
        }
    }
}

// 탈출 지역.
public class ExitArea
{
    public async UniTask ExitTimerAsync()
    {
        // 5초기다리기.
        Debug.Log("5초 타이머 시작");
        await UniTask.Delay(5000);
        Debug.Log("GameOver!");
        // GameOver 함수는  게임다시 시작, 종료, 제작 할수있는 제작대 UI 띄운다.
        ShowLobbyUI();
    }
    SceneLoader SceneLoader;

    private SceneLoader ShowLobbyUI()
    {
        SceneLoader = new SceneLoader();
        SceneLoader.LoadSceneAsync(SceneType.Lobby);
        return SceneLoader;
    }
}
