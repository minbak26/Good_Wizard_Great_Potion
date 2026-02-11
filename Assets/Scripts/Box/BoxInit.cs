using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;


public class BoxInit : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<ItemData> itemDatas = new List<ItemData>();

    private void Awake()
    {
        GetRandomItems();
    }
    
    public void GetRandomItems()
    {
        // 1. 모든 ItemData로드하기.
        ItemData[] allItems = Resources.LoadAll<ItemData>("Ingredient");

        if (allItems.Length == 0)
        {
            Debug.LogError("No Ingredient found");
            return;
        }

        itemDatas.Clear();
        
        // 2. 박스 아이템 랜덤개수.
        int spawnCount = Random.Range(2, 6);

        for (int i = 0; i < spawnCount; i++)
        {
            // 3. 등급 결정
            Rarity selectedRarity = RollRarity();
            
            // 4. 정해진 등급의 아이템 필터링.
            List<ItemData> filteredItemDatas = allItems.Where(x => x.rarity == selectedRarity).ToList();
            // 5. 필터링된 아이템 중에 하나 랜덤 추출.
            itemDatas.Add(filteredItemDatas[Random.Range(0, filteredItemDatas.Count)]);
            
            BoxInteration boxInteraction = GetComponent<BoxInteration>();
            if (boxInteraction != null)
            {
                // BoxInteraction에 있는 리스트(예: myItems)에 결과 전달
                boxInteraction.myItems = new List<ItemData>(this.itemDatas);
            }
 
        }
        
        
        // 결과 출력.
        foreach(var item in itemDatas) Debug.Log($"획득 : [ {item.rarity}] {item.name}" );
    }

    // 등급 결정함수. 0~69 Common, 
    private Rarity RollRarity()
    {
        int roll = Random.Range(0, 100);
        if (roll < 69) return Rarity.Common;
        else if (roll < 95 ) return Rarity.Rare;
        else  return Rarity.Epic;
    }


    
    
}
