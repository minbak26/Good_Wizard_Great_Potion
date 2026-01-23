using UnityEngine;
using UnityEngine.InputSystem; // 추가
using UnityEngine.UI; // UI 인벤토리를 위한 추가.


public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 5f;
    private Vector3 moveDirection;
    private Vector2 mouseScreenPos;
    
    private PlayerMoveAction input; // inputSystem설정에서 C# 클래스로 생성.
    private InputAction moveAction;
    private InputAction mouseAimAction;
    // 플레이어 상호작용
    private InputAction BoxInteraction;
    // 플레이어 상호작용 : 인벤토리 오픈.
    private InputAction InventoryOpenAction;
    
    
    // 스킬슬롯
	[SerializeField] GameObject Skill1;
	[SerializeField] GameObject Skill2;

	

    [Header("Shooting")] 
    public Transform shootPoint;
    public float shootCooldown = 0.1f;
    private float lastShootTime;
    
    void Awake()
    {
        input = new PlayerMoveAction();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        input.Enable();
        // "Player" 맵의 "Move" 액션 사용 (이름은 에디터에서 만든 이름과 동일해야 함)
        moveAction = input.PlayerMovement.Move;
        mouseAimAction = input.PlayerMovement.MouseAim;
        BoxInteraction = input.PlayerMovement.Interact;
        InventoryOpenAction = input.PlayerMovement.InventoryOpen;
        
        // 콜백 등록.
        BoxInteraction.performed += OnBoxInteract;
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        // 이동 입력: 월드 기준 (캐릭터 회전에 영향 받지 않음)
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        // 마우스 에임으로 방향 결정 → 회전 적용
        Vector2 mouseScreenPos = mouseAimAction.ReadValue<Vector2>();
        float distance = Mathf.Abs(Camera.main.transform.position.y - transform.position.y);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPos.x, mouseScreenPos.y, distance)
        );
        Vector3 lookDir = mouseWorldPos - transform.position;
        lookDir.y = 0f;

        if (lookDir.sqrMagnitude > 0.001f)
            transform.forward = lookDir.normalized;
        
        // 투사체 발사.
        if (Mouse.current.leftButton.wasPressedThisFrame && 
            Time.time - lastShootTime > shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
       

    }

    void FixedUpdate()
    {
        Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
    
    // 음? 이미 Shoot이 Magic에 있는데 여기 왜또 쓰지?
    // ReSharper disable Unity.PerformanceAnalysis
    void Shoot()
    {
        Magic magic = MagicPool.Instance.GetMagic();
        magic.Shoot(transform.forward, shootPoint.position);
    }
    
    // 박스 상호작용.
    private void OnBoxInteract(InputAction.CallbackContext context)
    {
       // boxManager.HandlePlayerInteraction(transform.position);

    }

    private void OnInventoryOpen(InputAction.CallbackContext context)
    {
        
    }

    
    // 플레이어 기본정보.
    [SerializeField] private float HP = 100;
    private float currentHP;

    
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
    void TakeDamage(float damage)
    {
        
        if (HP > 0)
        {
            currentHP -= damage;
            HP -= damage;
            Debug.Log("currentHP : "+currentHP);
            if (currentHP <= 0)
            {
                Debug.Log("player die!");
                Destroy(gameObject);
            }
        }
    }

}