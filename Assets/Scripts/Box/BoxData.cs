using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


// 박스에 대한 데이터정리, 
public class BoxData
{
    // 박스 아이디.
    public int boxID;
    // 박스내부 아이템 슬롯 개수.
    public int slotCount;
    // 모든 아이템 배열가져오기.
    public ItemData[] allItems;
    // 아이템 저장소.
    public List<InventorySlot> items;

    public BoxData(int id, int slots)
    {
        boxID = id;
        slotCount = slots;
        items = new List<InventorySlot>();
    }
    
    
    // 박스생성시 슬롯 개수 정하기(3~10)
    // 슬롯 개수 정하기.
    public void InitializeSlots()
    {
        slotCount = Random.Range(3, 11);
        items.Clear();
    }
    
    // 런타임에 아이템 적용하기 위해 함수 분리.
    public void GenerateRandomItems(ItemData[] allItems)
    {
        if (allItems == null || allItems.Length == 0)
        {
            Debug.LogWarning("사용 가능한 아이템이 없습니다!");
            return;
        }
        
        items.Clear();
        
        // 각 슬롯을 아이템으로 채우기
        for (int i = 0; i < slotCount; i++)
        {
            ItemData randomItem = GetRandomItemByRarity(allItems);
            
            if (randomItem != null)
            {
                // 스택 가능 아이템은 1~5개, 스택 불가는 1개
                int amount = randomItem.IsStackable ? Random.Range(1, 6) : 1;
                InventorySlot slot = new InventorySlot(randomItem, amount);
                items.Add(slot);
            }
        }
        
        Debug.Log($"박스 {boxID}: {items.Count}개의 슬롯에 아이템 배치됨");
    }
    
    private ItemData GetRandomItemByRarity(ItemData[] allItems)
    {
        int rarityRoll = Random.Range(0, 100);
        Rarity targetRarity;
        
        if (rarityRoll < 70)
            targetRarity = Rarity.Common;
        else if (rarityRoll < 90)
            targetRarity = Rarity.Rare;
        else
            targetRarity = Rarity.Epic;
        
        // 해당 레어도의 아이템 찾기
        var candidates = allItems.Where(item => item.rarity == targetRarity).ToArray();
        
        if (candidates.Length == 0)
        {
            Debug.LogWarning($"{targetRarity} 레어도의 아이템이 없습니다!");
            return null;
        }
        
        return candidates[Random.Range(0, candidates.Length)];
    }
    
}
    
    

