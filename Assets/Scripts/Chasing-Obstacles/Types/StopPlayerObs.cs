using UnityEngine;

[CreateAssetMenu(fileName ="StopPlayerObs", menuName ="ChasingObstacles/StopPlayer")]
public class StopPlayerObs : Base_ChasingObs
{
    public override void OnPlayerDetected(ChasingObstacle obstacle, PlayerMovement player)
    {
        Debug.Log("Player Stops!!!");
    }
}
