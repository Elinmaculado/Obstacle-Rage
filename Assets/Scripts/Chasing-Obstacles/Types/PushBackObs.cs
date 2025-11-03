using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "PushBackObs", menuName = "ChasingObstacles/PushBackPlayer")]
public class PushBackObs : Base_ChasingObs
{
    [Header("Push Settings")]
    public float pushForce = 10f;

    public override void OnTouchPlayer(ChasingObstacle obstacle, GameObject player)
    {
        Vector3 direction = (player.transform.position - obstacle.transform.position).normalized;
        obstacle.StartCoroutine(PushBack(player, direction));
        Debug.Log("PushBackObs touched Player");
    }

    private IEnumerator PushBack(GameObject player, Vector3 direction)
    {
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            float timer = 0.3f;
            while (timer > 0)
            {
                controller.Move(direction * pushForce * Time.deltaTime);
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }

}
