using UnityEngine;

public class O_PushObstacle : MonoBehaviour
{
    public float pushForce = 15f;  
    public float pushDuration = 0.2f; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                Vector3 oppositeDir = -player.transform.forward.normalized;
                Debug.Log("El jugador esta en rango");
                player.StartCoroutine(ApplyPush(player, oppositeDir));
            }
        }
    }

    private System.Collections.IEnumerator ApplyPush(PlayerMovement player, Vector3 direction)
    {
        float timer = 0f;
        while (timer < pushDuration)
        {
            player.ApplyExternalForce(direction * pushForce);
            timer += Time.deltaTime;
            yield return null;
        }

        Debug.Log("El jugador fue empujado");
        player.ApplyExternalForce(Vector3.zero);
    }
}
