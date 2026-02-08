using UnityEngine;
using Cysharp.Threading.Tasks;
public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab;
    private bool spawnOnStart = true;

    void Awake()
    {
        if (spawnOnStart)
        {
            SpawnBoxes().Forget();
        }
        
    }

    private async UniTaskVoid  SpawnBoxes()
    {
        if (boxPrefab == null)
        {
            Debug.LogError("Box Prefab is null");
            return;
        }
        
        GameObject box = Instantiate(boxPrefab, transform.position, transform.rotation);
        await UniTask.CompletedTask;
    }
    
}
