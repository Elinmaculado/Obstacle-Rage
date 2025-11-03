using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "StopPlayerObs", menuName = "ChasingObstacles/StopPlayer")]
public class StopPlayerObs : Base_ChasingObs
{
    [Header("Stop Settings")]
    public float stopDuration = 1.5f;

    public override void OnTouchPlayer(ChasingObstacle obstacle, GameObject player)
    {
        if (player == null) return;

        obstacle.StartCoroutine(FreezePlayer(obstacle, player));
        Debug.Log($"[{obstacle.name}] StopPlayerObs touched player.");
    }

    private IEnumerator FreezePlayer(ChasingObstacle obstacle, GameObject player)
    {
        var movement = player.GetComponent<PlayerMovement>();
        var rb = player.GetComponent<Rigidbody>();
        var cc = player.GetComponent<CharacterController>();

        if (movement != null)
        {
            movement.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        if (cc != null)
        {
            yield return null;
        }

        Debug.Log($"[{obstacle.name}] Player frozen for {stopDuration} seconds.");
        yield return new WaitForSeconds(stopDuration);

        if (movement != null)
        {
            movement.enabled = true;
        }

        if (rb != null)
        {
            rb.isKinematic = false;
        }

        Debug.Log($"[{obstacle.name}] Player unfrozen.");
    }
}
