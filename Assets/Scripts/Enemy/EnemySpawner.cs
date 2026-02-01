using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab; // 소환할 적 프리팹
    [SerializeField] private bool spawnOnStart = true;

    void Start()
    {
        if (spawnOnStart)
        {
            // 비동기로 스폰 실행 (필요 시 지연 시간을 줄 수도 있음)
            SpawnEnemy().Forget();
        }
    }

    private async UniTaskVoid SpawnEnemy()
    {
        // 프리팹이나 위치 설정이 안 되어 있으면 방지
        if (enemyPrefab == null)
        {
            Debug.LogWarning("스포너에 Enemy 프리팹이 할당되지 않았습니다!");
            return;
        }

        // 지정된 위치(스포너의 위치)와 회전값으로 적 생성
        // Instantiate는 동기 함수지만, UniTask 흐름 내에서 호출 가능합니다.
        GameObject spawnedEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        
        // 생성 후 스포너의 역할을 다했으므로 알림 (선택 사항)
        Debug.Log($"{gameObject.name}: 적 생성 완료. 스폰 기능을 종료합니다.");

        // 만약 소환 시 이펙트를 넣고 싶다면 여기에 await UniTask.Delay 등을 쓸 수 있습니다.
        await UniTask.CompletedTask;
    }

    // 에디터에서 스포너 위치를 잘 볼 수 있게 구체 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawIcon(transform.position + Vector3.up, "CharacterGizmo", true);
    }
}
