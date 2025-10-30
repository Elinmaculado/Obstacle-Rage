using UnityEngine;
using UnityEngine.AI;

public abstract class Base_ChasingObs : ScriptableObject
{
    [Header("General Settings")]
    public string obstacleName = "Chasing Obstacle";
    public float chaseSpeed = 3f;
    public float lifeTime = 5f;

    [Header("Detection")]
    public float detectionRadius = 2f;

    public virtual void OnSpawn(ChasingObstacle obstacle, Transform player) { }

    public virtual void OnUpdate(ChasingObstacle obstacle, Transform player)
    {
        if (!player) return;
        NavMeshAgent agent = obstacle.Agent;
        if (agent && agent.enabled)
        {
            agent.SetDestination(player.position);
        }
    }

    public abstract void OnPlayerDetected(ChasingObstacle obstacle, PlayerMovement player);

    public virtual void OnDespawn(ChasingObstacle obstacle)
    {
        GameObject.Destroy(obstacle.gameObject);
    }
}
