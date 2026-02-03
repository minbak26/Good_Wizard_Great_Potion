using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;





public class EnemyAI : MonoBehaviour
{
    // 상태 정의
    private enum State {Patrol, Chase, Attack}
    
    // 기본상태는 Patrol
    [Header("Current State")]
    private State currentState = State.Patrol;

    [Header("Settings")] [SerializeField] private float patrolSpeed = 5f;
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float detectionAngle = 60f; // 시야각 (좌우 60도).
    [SerializeField] private float detectionDistance = 15f; // 시각적 감지 거리.
    
    
    private NavMeshAgent navAgent; // 적의 다리 역할.
    private Transform playerTarget; // 쫓아갈 대상(플레이어).
    private CancellationTokenSource cts; //오브젝트 파괴시 태스크 종료용.
     
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // 현재 부착되어있는 NavMeshAgent 가져오기.
        navAgent = GetComponent<NavMeshAgent>();
        // 게임 시작 시 "Player"태그를 가진 오브젝트를 찾아서 타겟으로 설정.
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        // 처음에 플레이어의 위치를 가져왔는데.. 이럼 바로 쫓아올수 있지 않나?
        if (playerObj != null)
        {
            playerTarget = playerObj.transform;
        }
        
        // 초기 속도 설정.
        navAgent.speed = patrolSpeed;
        
        cts = new CancellationTokenSource();
        
        // FSM 구동 시작.
        RunFSM(cts.Token);


    }

    private void OnDestroy()
    {
        // 적이 죽거나 삭제되면 비동기 루프 종료.
        cts?.Cancel();
    }

    private async UniTaskVoid RunFSM(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            switch (currentState)
            {
                case State.Patrol:
                    await PatrolState(token);
                    break;
                case State.Chase:
                    await ChaseState(token);
                    break;
                case State.Attack:
                    await AttackState(token);
                    break;
            }
            await UniTask.Yield();
        }
    }

   
    // 1. 정찰 상태.
    private async UniTask PatrolState(CancellationToken token)
    {
        Debug.Log("Patrol State");
        navAgent.speed = patrolSpeed;

        while (currentState == State.Patrol && !token.IsCancellationRequested)
        {
            // 시야 확인 로직.
            if (CanSeePlayer())
            {
                currentState = State.Chase;
                // 상태가 바뀌면 함수 종료 -> RunFSM에서 다음 상태 실행.
                return;
            }
            // 이건 무슨의미지?
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
    }

    private bool CanSeePlayer()
    {
        Debug.Log("CanSeePlayer");
        if (playerTarget == null) return false;
        Vector3 directionToPlayer = (playerTarget.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer <= detectionDistance)
        {
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle <= detectionAngle * 0.5f)
            {
                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, out RaycastHit hit, detectionDistance))
                {
                    if (hit.transform.CompareTag("Player")) return true;
                }
            }
        }
        return false;
    }

    // 2. 추격 상태.
    private async UniTask ChaseState(CancellationToken token)
    {
        Debug.Log("Chase State");
        // 상태가 변하면 속도 증가.
        navAgent.speed = chaseSpeed;
        while (currentState == State.Chase && !token.IsCancellationRequested)
        {
            navAgent.SetDestination(playerTarget.position);
            
            float distance = Vector3.Distance(transform.position, playerTarget.position);
            
            if (distance <= attackRange)
            {
                currentState = State.Attack;
                return;
            }
            else if (distance > detectionDistance * 1.5f)
            {
                currentState = State.Patrol;
                return;
            }

            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
    }

  
    // 3. 공격 상태.
    private async UniTask AttackState(CancellationToken token)
    {
        Debug.Log("Attack State");
        navAgent.SetDestination(transform.position);

        while (currentState == State.Attack && !token.IsCancellationRequested)
        {
            Debug.Log("플레이어 공격 중 (UniTask)!");

            float distance = Vector3.Distance(transform.position, playerTarget.position);
            if (distance > attackRange)
            {
                currentState = State.Chase;
                return;
            }
        }

        // 공격 쿨타임 등을 줄 때 아주 유용함
            await UniTask.Delay(1000, cancellationToken: token); // 1초에 한 번씩 공격 로직 실행
    }
    
    
}
