using UnityEngine;

[CreateAssetMenu(fileName = "Switcher", menuName = "ChasingObstacles/Switcher")]
public class SwitcherObs : Base_ChasingObs
{
    public override void OnPlayerDetected(ChasingObstacle obstacle, PlayerMovement player)
    {
        Debug.Log("Switcher is switching");
    }
}
