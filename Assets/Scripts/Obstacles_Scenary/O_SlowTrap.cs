using UnityEngine;

public class O_SlowTrap : MonoBehaviour
{
    [Header("Configuración del efecto")]
    public float slowMultiplier = 0.5f;  
    public float slowDuration = 2f;     

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                Debug.Log("Jugador entró en la trampa");
                player.StartCoroutine(ApplySlow(player));
            }
        }
    }

    private System.Collections.IEnumerator ApplySlow(PlayerMovement player)
    {
        float originalSpeed = player.speed;

        player.speed *= slowMultiplier;
        Debug.Log("Velocidad reducida");

        yield return new WaitForSeconds(slowDuration);

        player.speed = originalSpeed;
        Debug.Log("Velocidad restaurada");
    }
}
