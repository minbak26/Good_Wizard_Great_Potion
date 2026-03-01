using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
     public static StorageManager instance;
     
     public List<ItemData> storageData = new List<ItemData>();
     void Awake()
     {
         if (instance == null)
         {
             instance = this;
             DontDestroyOnLoad(gameObject);
         }
         else
         {
             Destroy(gameObject);
         }
         
     }

    // Update is called once per frame
    void AddItem(ItemData item)
    {
        storageData.Add(item);
    }
}
