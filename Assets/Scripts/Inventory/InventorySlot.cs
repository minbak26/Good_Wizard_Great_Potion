using System;
using System.Collections.Generic;using UnityEditor.Timeline.Actions;
using UnityEngine;
// ItemData는 데이터를 정의만하고, 여기 InventorySlot에서 인벤토리 슬롯이
// 분리되어야 한다. -> 데이터를 활용하기 위해서?
public class InventorySlot
{
    public ItemData slotItemData; // 아이템 데이터를 참조하기위한 변수.
    public int Amount { get; private set; }
    private int maxSlot = 28;// 최대 슬롯 수.
    public int amount;

    public ItemData SlotItemData
    {
        get { return slotItemData; }
        set { slotItemData = value; }
    }
    
    // 생성자
    public InventorySlot(ItemData itemData, int amount = 1)
    {
        SlotItemData = itemData;
        Amount = amount;
    }
    
    // 개수 증가 메서드 (캡슐화)
    public void AddAmount(int count)
    {
        Amount = Mathf.Min(Amount + count, maxSlot);
    }
    
    // 개수 감소 메서드
    public bool RemoveAmount(int count)
    {
        if (Amount >= count)
        {
            Amount -= count;
            return true;
        }
        return false;
    }

    public bool IsEmpty()
    {
        throw new NotImplementedException();
    }
}


