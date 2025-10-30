using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class ChasingObstacle : MonoBehaviour
{
    public Base_ChasingObs obstacleData;
    private Transform player;
    private float lifeTimer;
    private bool playerDetected;

    public NavMeshAgent Agent { get; private set; }

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (!player)
        {
            Debug.LogError("Player not found in scene. Make sure the Player has tag 'Player'.");
            return;
        }

        Agent.speed = obstacleData.chaseSpeed;
        lifeTimer = obstacleData.lifeTime;
        obstacleData.OnSpawn(this, player);
        StartCoroutine(LifeCountdown());
    }

    void Update()
    {
        if (!player) return;
        obstacleData.OnUpdate(this, player);

        if (!playerDetected)
        {
            float dist = Vector3.Distance(transform.position, player.position);
            if (dist <= obstacleData.detectionRadius)
            {
                PlayerMovement pm = player.GetComponent<PlayerMovement>();
                if (pm != null)
                {
                    playerDetected = true;
                    obstacleData.OnPlayerDetected(this, pm);
                }
            }
        }
    }

    IEnumerator LifeCountdown()
    {
        yield return new WaitForSeconds(obstacleData.lifeTime);
        obstacleData.OnDespawn(this);
    }
}
