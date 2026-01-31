using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // 로고
    public Animation LogoAnim;
    public TextMeshProUGUI LogoTxt;
    
    //타이틀
    public GameObject Title;
    public Slider LoadingSlider;
    public TextMeshProUGUI LoadingProgressTxt;

    private AsyncOperation m_AsyncOperation;
    private void Awake()
    {
        LogoAnim.gameObject.SetActive(true);
        Title.SetActive(false);
    }

    private void Start()
    {
        // 유저데이터 로드
        UserDataManager.Instance.LoadUserData();
        // 작성된 유저데이터가 없으면 기본값으로 세팅후 저장.
        if (!UserDataManager.Instance.ExistsSavedData)
        {
            UserDataManager.Instance.SetDefaultUserData();
            UserDataManager.Instance.SaveUserData();
        }
        
        StartCoroutine(LoadGameCo());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator LoadGameCo()
    {
        Logger.Log($"{GetType()}::LoadGameCo");
        
        LogoAnim.Play();
        yield return new WaitForSeconds(LogoAnim.clip.length);
        
        LogoAnim.gameObject.SetActive(false);
        Title.SetActive(true);

        m_AsyncOperation = SceneLoader.Instance.LoadSceneAsync(SceneType.Lobby);
        if (m_AsyncOperation == null)
        {
            Logger.Log("Lobby async loading error");
            yield break;
        }
        
        m_AsyncOperation.allowSceneActivation = false;

        LoadingSlider.value = 0.5f;
        LoadingProgressTxt.text = $"{(int)(LoadingSlider.value * 100)}%";
        yield return new WaitForSeconds(0.5f);
        
        // 로딩이 끝나기 전까지 프로그래스바 업데이트.
        while (!m_AsyncOperation.isDone)
        {
            LoadingSlider.value = m_AsyncOperation.progress < 0.5f ? 0.5f : m_AsyncOperation.progress;
            LoadingProgressTxt.text = $"{(int)(LoadingSlider.value * 100)}%";
            if (m_AsyncOperation.progress >= 0.9f)
            {
                m_AsyncOperation.allowSceneActivation = true;
                yield break;
            }
            yield return null;
        }
    }
}
