using UnityEngine;

public class Magic : MonoBehaviour
{
    private MagicPool pool;
    private Vector3 direction;
    private float speed = 30f;
    private float lifeTime = 5f;
    private float elapsedTime;
    
    // Maigd Damage
    public float magicDamge = 5f;
    
    public void SetPool(MagicPool magicPool)
    {
        pool = magicPool;
    }
    
    public void Shoot(Vector3 shootDir, Vector3 startPos)
    {
        transform.position = startPos;
        direction = shootDir.normalized;
        elapsedTime = 0f;
        gameObject.SetActive(true);
    }
    
    void Update()
    {
        // 1. 직선 이동
        transform.position += direction * speed * Time.deltaTime;
        
        // 2. 수명 체크
        elapsedTime += Time.deltaTime;
        if (elapsedTime > lifeTime)
        {
            ReturnToPool();
            return;
        }
        
        // 3. 화면 밖 체크 (옵션)
        if (transform.position.magnitude > 100f)
        {
            ReturnToPool();
        }
    }
    
    void ReturnToPool()
    {
        if (pool != null)
            pool.ReturnMagic(this);
    }
}
