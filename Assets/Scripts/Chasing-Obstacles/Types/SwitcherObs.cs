using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Switcher", menuName = "ChasingObstacles/Switcher")]
public class SwitcherObs : Base_ChasingObs
{
    [Header("Switcher Settings")]
    public List<Base_ChasingObs> possibleBehaviors;
    public float switchInterval = 3f;

    private Base_ChasingObs currentBehavior;
    private float switchTimer;

    public override void OnSpawn(ChasingObstacle obstacle, GameObject player)
    {
        base.OnSpawn(obstacle, player);
        SwitchBehavior(obstacle, player);
    }

    public override void OnUpdate(ChasingObstacle obstacle, GameObject player)
    {
        base.OnUpdate(obstacle, player);

        switchTimer += Time.deltaTime;
        if (switchTimer >= switchInterval)
        {
            SwitchBehavior(obstacle, player);
            switchTimer = 0;
        }

        currentBehavior?.OnUpdate(obstacle, player);
    }

    public override void OnTouchPlayer(ChasingObstacle obstacle, GameObject player)
    {
        currentBehavior?.OnTouchPlayer(obstacle, player);
        Debug.Log("SwitcherObs touched Player");
    }

    public override void OnStayTouch(ChasingObstacle obstacle, GameObject player, float deltaTime)
    {
        currentBehavior?.OnStayTouch(obstacle, player, deltaTime);
    }

    public override void OnLeavePlayer(ChasingObstacle obstacle, GameObject player)
    {
        currentBehavior?.OnLeavePlayer(obstacle, player);
    }

    private void SwitchBehavior(ChasingObstacle obstacle, GameObject player)
    {
        if (possibleBehaviors == null || possibleBehaviors.Count == 0) return;

        if (currentBehavior != null)
        {
            currentBehavior.OnDespawn(obstacle, player);
            Debug.Log($"[{obstacle.name}] Cleaned up previous behavior: {currentBehavior.name}");
        }

        currentBehavior = possibleBehaviors[Random.Range(0, possibleBehaviors.Count)];
        Debug.Log($"[{obstacle.name}] switched to behavior: {currentBehavior.name}");

        if (currentBehavior is StopPlayerObs)
            obstacle.SetColor(Color.yellow);
        else if (currentBehavior is KillPlayerObs)
            obstacle.SetColor(Color.red);
        else if (currentBehavior is PushBackObs)
            obstacle.SetColor(Color.green);
        else if (currentBehavior is SlowPlayerObs)
            obstacle.SetColor(Color.cyan);
        else
            obstacle.SetColor(Color.white);
    }

    public override void OnDespawn(ChasingObstacle obstacle, GameObject player)
    {
        currentBehavior?.OnDespawn(obstacle, player);
        Debug.Log($"[{obstacle.name}] Switcher despawned — cleaned current behavior.");
    }
}
