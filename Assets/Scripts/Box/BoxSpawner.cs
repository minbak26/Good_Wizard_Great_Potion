using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;          // 박스 프리팹
    [SerializeField] private Transform[] spawnPoints;       // 박스 생성 위치들
    [SerializeField] private int minBoxCount = 3;
    [SerializeField] private int maxBoxCount = 8;
    
    private BoxManager boxManager;
    
    void Start()
    {
        boxManager = GetComponent<BoxManager>();
        SpawnBoxes();
    }
    
    /// <summary>
    /// 지정된 위치에 박스 생성
    /// </summary>
    private void SpawnBoxes()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("생성 위치(SpawnPoints)가 설정되지 않았습니다!");
            return;
        }
        
        // 각 생성 위치에서 랜덤 개수의 박스 생성
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int boxCountAtPoint = Random.Range(minBoxCount, maxBoxCount + 1);
            
            for (int j = 0; j < boxCountAtPoint; j++)
            {
                SpawnSingleBox(spawnPoints[i], i, j);
            }
        }
    }
    
    /// <summary>
    /// 단일 박스 생성
    /// </summary>
    private void SpawnSingleBox(Transform spawnPoint, int pointIndex, int boxIndex)
    {
        // 박스 인스턴스 생성
        GameObject boxInstance = Instantiate(
            boxPrefab,
            spawnPoint.position + new Vector3(boxIndex * 2f, 0, 0),  // 박스들을 약간 떨어뜨림
            Quaternion.identity,
            transform
        );
        
        // BoxCollider 컴포넌트 추가 (상호작용용)
        BoxCollider collider = boxInstance.GetComponent<BoxCollider>();
        if (collider == null)
        {
            collider = boxInstance.AddComponent<BoxCollider>();
            collider.isTrigger = true;
        }
        
        // BoxInteractable 컴포넌트 추가 (상호작용 처리)
        BoxInteractable interactable = boxInstance.GetComponent<BoxInteractable>();
        if (interactable == null)
        {
            interactable = boxInstance.AddComponent<BoxInteractable>();
        }
        
        // BoxManager에 박스 등록
        boxManager.RegisterBox(boxInstance, pointIndex, boxIndex);
        
        Debug.Log($"박스 생성: SpawnPoint[{pointIndex}] - Box[{boxIndex}] at {boxInstance.transform.position}");
    }
}
