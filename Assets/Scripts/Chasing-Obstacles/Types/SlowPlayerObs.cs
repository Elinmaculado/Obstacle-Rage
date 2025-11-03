using UnityEngine;

[CreateAssetMenu(fileName = "SlowPlayerObs", menuName = "ChasingObstacles/SlowPlayer")]
public class SlowPlayerObs : Base_ChasingObs
{
    [Header("Slow Settings")]
    public float slowRange = 5f;
    [Tooltip("0.5 = 50% of base speed.")]
    public float slowMultiplier = 0.5f;

    public override void OnUpdate(ChasingObstacle obstacle, GameObject player)
    {
        base.OnUpdate(obstacle, player);

        if (player == null || obstacle == null) return;

        float distance = Vector3.Distance(obstacle.transform.position, player.transform.position);
        var speedMod = player.GetComponent<PlayerSpeedModifier>();

        if (speedMod == null)
            speedMod = player.AddComponent<PlayerSpeedModifier>();

        int obsId = obstacle.GetInstanceID();

        if (distance <= slowRange)
        {
            speedMod.AddOrUpdateMultiplier(obsId, slowMultiplier);
        }
        else
        {
            speedMod.RemoveMultiplier(obsId);
        }
    }

    public override void OnLeavePlayer(ChasingObstacle obstacle, GameObject player)
    {
        if (player == null || obstacle == null) return;

        var speedMod = player.GetComponent<PlayerSpeedModifier>();
        if (speedMod != null)
        {
            speedMod.RemoveMultiplier(obstacle.GetInstanceID());
            Debug.Log($"[{obstacle.name}] Player left range — restored speed.");
        }
    }

    public override void OnDespawn(ChasingObstacle obstacle, GameObject player)
    {
        if (player == null || obstacle == null) return;

        var speedMod = player.GetComponent<PlayerSpeedModifier>();
        if (speedMod != null)
        {
            speedMod.RemoveMultiplier(obstacle.GetInstanceID());
            Debug.Log($"[{obstacle.name}] despawned — restored player speed.");
        }
    }

}
