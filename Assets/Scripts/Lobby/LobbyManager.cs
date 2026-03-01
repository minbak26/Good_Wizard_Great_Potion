using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

    private void Awake()
    {
        if(instance == null)
            instance= this;
        //DontDestroyOnLoad(gameObject);
    }

    private GameObject playButton;
    private GameObject inventoryButton;
    private GameObject craftButton;
    private GameObject storageButton;
    private GameObject quitButton;
    
    // 전부 OnClick으로 등록할 예정
    // InGame씬으로 이동할 함수.
    public void startGame()
    {
        SceneLoader.Instance.LoadScene(SceneType.InGame);
    }
    
    
    
    // 인벤토리 버튼 활성화 비활성화.
    // Craft 창 활성화, 비활성화.
    // Storage창 활성화, 비활성화.
    // 하나의 함수로 모든 창을 제어합니다.
    public void ToggleWindow(GameObject targetWindow)
    {
        if (targetWindow != null)
        {
            // 전달받은 대상의 활성화 상태를 반전시킵니다.
            bool isActive = targetWindow.activeSelf;
            targetWindow.SetActive(!isActive);
        }
    }
    
    
    // 게임종료.
    public void quitButtonAction()
    {
        #if UNITY_EDITOR
            // 유니티 에디터에서 실행 중일 때 재생 모드를 종료합니다.
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // 빌드된 실제 게임(PC, 모바일 등)에서 앱을 종료합니다.
            Application.Quit();
        #endif
    }

}
