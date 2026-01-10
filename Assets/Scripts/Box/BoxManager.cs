using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

// 박스 상호작용, 박스열기등 박스 기능들 모음.
public class BoxManager : MonoBehaviour
{
     [SerializeField] private ItemData[] allAvailableItems;  // 모든 아이템 데이터
    
    private List<BoxData> boxes = new List<BoxData>();
    private Dictionary<GameObject, BoxData> boxInstanceMap = new Dictionary<GameObject, BoxData>();
    
    private int nextBoxID = 0;
    
    void Start()
    {
        // BoxSpawner가 Start에서 SpawnBoxes를 호출하므로
        // 여기서는 특별히 할 일 없음
    }
    
    /// <summary>
    /// 박스 등록 및 아이템 생성
    /// </summary>
    public void RegisterBox(GameObject boxInstance, int pointIndex, int boxIndex)
    {
        // 박스 데이터 생성
        BoxData boxData = new BoxData(nextBoxID++, Random.Range(3, 11));
        
        // 박스에 랜덤 아이템 배치
        boxData.GenerateRandomItems(allAvailableItems);
        
        // 리스트에 등록
        boxes.Add(boxData);
        boxInstanceMap[boxInstance] = boxData;
        
        Debug.Log($"박스 {boxData.boxID} 등록 완료: {boxData.items.Count}개 아이템");
    }
    
    /// <summary>
    /// 박스와 상호작용 (박스 열기)
    /// </summary>
    public void OnBoxInteracted(GameObject boxInstance)
    {
        if (!boxInstanceMap.TryGetValue(boxInstance, out BoxData boxData))
        {
            Debug.LogWarning("등록되지 않은 박스입니다!");
            return;
        }
        
        Debug.Log($"박스 {boxData.boxID} 열음!");
        ShowBoxContents(boxData);
    }
    
    /// <summary>
    /// 박스 내용 표시 (디버그용)
    /// </summary>
    private void ShowBoxContents(BoxData boxData)
    {
        Debug.Log($"\n========== 박스 {boxData.boxID} 내용 ==========");
        
        for (int i = 0; i < boxData.items.Count; i++)
        {
            InventorySlot slot = boxData.items[i];
            Debug.Log($"[슬롯 {i}] {slot.slotItemData.ItemName} x {slot.amount} ({slot.slotItemData.rarity})");
        }
        
        Debug.Log("=====================================\n");
    }
    
    /// <summary>
    /// 박스에서 모든 아이템 획득
    /// </summary>
    public List<InventorySlot> GetBoxItems(GameObject boxInstance)
    {
        if (!boxInstanceMap.TryGetValue(boxInstance, out BoxData boxData))
        {
            Debug.LogWarning("등록되지 않은 박스입니다!");
            return new List<InventorySlot>();
        }
        
        return boxData.items;
    }
    
    /// <summary>
    /// 박스 제거 (아이템 획득 후)
    /// </summary>
    public void DestroyBox(GameObject boxInstance)
    {
        if (boxInstanceMap.ContainsKey(boxInstance))
        {
            boxInstanceMap.Remove(boxInstance);
            Destroy(boxInstance);
            Debug.Log("박스 제거됨");
        }
    }
    
    /// <summary>
    /// 모든 박스 정보 출력 (테스트용)
    /// </summary>
    public void PrintAllBoxes()
    {
        Debug.Log($"\n========== 모든 박스 ({boxes.Count}개) ==========");
        
        foreach (BoxData box in boxes)
        {
            Debug.Log($"박스 {box.boxID}: {box.items.Count}개 슬롯");
            for (int i = 0; i < box.items.Count; i++)
            {
                InventorySlot slot = box.items[i];
                Debug.Log($"  - {slot.slotItemData.ItemName} x {slot.amount}");
            }
        }
        
        Debug.Log("================================\n");
    }
    
    
}
