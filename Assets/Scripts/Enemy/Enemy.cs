using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Magic magic = other.GetComponent<Magic>();

        if (magic != null)
        {
            TakeDamage(magic.magicDamge);
            //탄환 제거.
            Destroy(other.gameObject);
        }
    }
    
    public void TakeDamage(float damage)
    {
        
        CurrentHp -= damage;
        Debug.Log("Taking damage! Current Hp:" + CurrentHp);
        if(CurrentHp <= 0)
            Destroy(this.gameObject);
    }

    private static float MaxHp = 100f;
    private float CurrentHp = MaxHp;
    // private float MoveSpeed = 5f; 현재움직이지 않으므로 일단 주석처리.
    
}



