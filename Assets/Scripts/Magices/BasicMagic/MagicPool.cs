using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicPool : MonoBehaviour
{
    public static MagicPool Instance;

    [Header("Pool Settings")] 
    [SerializeField] private GameObject MagicPrefab;

    [SerializeField] private int initialPoolSize = 50;

    private Queue<Magic> pool = new Queue<Magic>();

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Magic bullet = CreateMagic();
            pool.Enqueue(bullet);
        }
    }

    Magic CreateMagic()
    {
        GameObject MagicObj = Instantiate(MagicPrefab);
        Magic magic = MagicObj.GetComponent<Magic>();
        magic.SetPool(this);
        MagicObj.SetActive(false);
        return magic;
    }

    public Magic GetMagic()
    {
        if (pool.Count == 0)
        {
            return CreateMagic();
        }

        return pool.Dequeue();
    }

    public void ReturnMagic(Magic magic)
    {
        magic.gameObject.SetActive(false);
        pool.Enqueue(magic);
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}
