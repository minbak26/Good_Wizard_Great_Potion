using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{ 
    private int health = 100;
public void TakeDamage(int damage)
{
    health -= damage;

    if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
}
private void DestroyEnemy()
{
    Destroy(gameObject);
}


    
}



