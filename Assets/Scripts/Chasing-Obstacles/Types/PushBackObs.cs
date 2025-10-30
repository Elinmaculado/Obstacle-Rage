using UnityEngine;

[CreateAssetMenu(fileName = "PushBackObs", menuName = "ChasingObstacles/PushBackPlayer")]
public class PushBackObs : Base_ChasingObs
{
    public override void OnPlayerDetected(ChasingObstacle obstacle, PlayerMovement player)
    {
        Debug.Log("Player Pushed.");
    }
}
