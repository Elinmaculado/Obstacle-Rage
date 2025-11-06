using UnityEngine;

[CreateAssetMenu(fileName = "KillPlayerObs", menuName = "ChasingObstacles/KillPlayer")]
public class KillPlayerObs : Base_ChasingObs
{
    private float contactTime = 0f;

    public override void OnStayTouch(ChasingObstacle obstacle, GameObject player, float deltaTime)
    {
        Debug.Log("KillPlayerObs touched Player  =  " + contactTime);
        contactTime += deltaTime;
        if (contactTime >= 1.5f)
        {
            GameObject.Destroy(player);
            contactTime = 0f;
        }
    }

    public override void OnLeavePlayer(ChasingObstacle obstacle, GameObject player)
    {
        contactTime = 0f;
        Debug.Log("KillPlayerObs left  =  " + contactTime);
    }

}
