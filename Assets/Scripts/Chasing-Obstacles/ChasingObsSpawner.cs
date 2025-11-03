using UnityEngine;

public class ChasingObsSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Base_ChasingObs[] obstacleTypes;
    public GameObject player;
    public float spawnInterval = 3f;
    public float spawnRadius = 10f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnInterval);
    }

    void SpawnObstacle()
    {
        if (obstacleTypes.Length == 0 || player == null) return;

        Vector3 randomPos = player.transform.position + Random.onUnitSphere * spawnRadius;
        randomPos.y = player.transform.position.y;

        GameObject newObs = Instantiate(obstaclePrefab, randomPos, Quaternion.identity);
        var chaser = newObs.GetComponent<ChasingObstacle>();
        chaser.Initialize(obstacleTypes[Random.Range(0, obstacleTypes.Length)], player);
    }
}
