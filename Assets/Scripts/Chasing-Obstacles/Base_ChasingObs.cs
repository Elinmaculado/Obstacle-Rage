using UnityEngine;
using UnityEngine.AI;

public abstract class Base_ChasingObs : ScriptableObject
{
    [Header("General Settings")]
    public string obstacleName = "New Chasing Obstacle";
    public float moveSpeed = 3f;
    public float lifeTime = 5f; // seconds
    public Color obstacleColor = Color.white;

    public virtual void OnSpawn(ChasingObstacle obstacle, GameObject player)
    {
        obstacle.SetColor(obstacleColor);
        obstacle.Agent.speed = moveSpeed;
        obstacle.StartCoroutine(obstacle.DespawnAfterTime(lifeTime));
    }

    public virtual void OnUpdate(ChasingObstacle obstacle, GameObject player)
    {
        if (obstacle.Agent != null && player != null)
            obstacle.Agent.SetDestination(player.transform.position);
    }

    public virtual void OnTouchPlayer(ChasingObstacle obstacle, GameObject player) { }

    public virtual void OnStayTouch(ChasingObstacle obstacle, GameObject player, float deltaTime) { }

    public virtual void OnLeavePlayer(ChasingObstacle obstacle, GameObject player) { }

    public virtual void OnDespawn(ChasingObstacle obstacle, GameObject player) { }
}
