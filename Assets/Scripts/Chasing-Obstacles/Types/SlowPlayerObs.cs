using UnityEngine;

[CreateAssetMenu(fileName = "SlowPlayerObs", menuName = "ChasingObstacles/SlowPlayer")]
public class SlowPlayerObs : Base_ChasingObs
{
    public override void OnPlayerDetected(ChasingObstacle obstacle, PlayerMovement player)
    {
        Debug.Log("Player Has Been Slowed!!!!");
    }
}
