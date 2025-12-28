using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

// 박스 상호작용, 박스열기등 박스 기능들 모음.
public class BoxManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public List<int> boxes = new List<int>();
    void Awake()
    {
        // box 생성.
        // world에서 boxspawner확인후. boxspawner만큼 생성.
        // world에서 tag확인해서 수 가져오는 방법 없을까?
        
     ;
        
        // 일시적 랜덤수만큼 박스안 칸의 개수 미리 정하기.
        int boxspawnerCount = Random.Range(5, 10);
        for (int i = 0; i < boxspawnerCount; i++)
        {
            int inboxCount = Random.Range(3, 10);
            boxes.Add(inboxCount);
        }
        
    }

    //Unitask 활용.
    public async void OnInteract()
    {

        await UniTask.Delay(100);
        //ShowBoxUI();
    }

    // Update is called once per frame
    void Update()
    {
        // 테스트용.
        TestGenerateItems().Forget();
        
    }
    
            
     // UniTask 테스트!
            
    async UniTaskVoid TestGenerateItems() 
    {
        
    }

   
    
    
}
