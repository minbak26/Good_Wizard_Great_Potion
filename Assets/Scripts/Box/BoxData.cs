using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


// 박스에 대한 데이터정리, 
public class BoxData
{
    // 박스내부 아이템 슬롯 개수.
    public int slotCount;
    // 모든 아이템 배열가져오기.
    public ItemData[] allItems;
    // 아이템 저장소.
    public List<ItemData> items = new List<ItemData>();
    
    // 박스 개수(런타임에 아이템 적용을 위해 박스 생성시에는 박스의 슬롯 개수만 보관)
    public int[] boxCount;
    
    // 슬롯 개수 정하기.
    public void GenerateSlots()
    {
        slotCount = Random.Range(3, 11);
        
        items.Clear();
    
    }

    // 런타임에 아이템 적용하기 위해 함수 분리.
    public void GenerateItems(int slotCount)
    {
        // Common 70%, Rare 20%, Epic 10% 
        for (int i = 0; i < slotCount; i++)
        {
            int RarityNum = Random.Range(0, 9);
            ItemData item = null;
            
            if (RarityNum >= 0 && RarityNum <= 6)
            {
                var candidates = allItems.Where(item => item.rarity == Rarity.Common).ToArray();
 
            }
            else if (RarityNum >= 7 && RarityNum <= 8)
            {
                var candidates = allItems.Where(item => item.rarity == Rarity.Rare).ToArray();
                
            }
            else if (RarityNum == 9)
            {
                var candidates = allItems.Where(item => item.rarity == Rarity.Epic).ToArray();
                
            }
            items.Add(item);
        }
    }
    
    private ItemData GetRandomItemByRarity(Rarity rarity)
    {
        var candidates = allItems.Where(item => item.rarity == rarity).ToArray();
        if (candidates.Length == 0) return null;
        return candidates[Random.Range(0, candidates.Length)];
    }
    
    
}
