using UnityEngine;

[CreateAssetMenu(fileName = "KillPlayerObs", menuName = "ChasingObstacles/KillPlayer")]
public class KillPlayerObs : Base_ChasingObs
{
    public override void OnPlayerDetected(ChasingObstacle obstacle, PlayerMovement player)
    {
        Debug.Log("Player Killed!");
    }
}
