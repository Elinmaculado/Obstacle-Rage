using System.Collections.Generic;
using UnityEngine;

public class Obstacles_Base : MonoBehaviour
{
    [Header("Configuración del spawn")]
    public GameObject[] obstaclePrefabs;
    public int maxActiveObstacles = 5;
    public float spawnInterval = 2f;      
    public float spawnRange = 15f;      
    public float despawnRange = 25f;

    [Header("Referencias")]
    public Transform player;
    
    public List<GameObject> activeObstacles = new List<GameObject>();
    public Queue<GameObject> pooledObstacles = new Queue<GameObject>();

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        if (player == null) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            TrySpawnObstacle();
            timer = spawnInterval;
        }

        CheckObstacleDistance();
    }

    void TrySpawnObstacle()
    {
        if (activeObstacles.Count >= maxActiveObstacles) return;

        Vector3 spawnPos = GetRandomPositionAroundPlayer();
        GameObject obstacle = GetObstacleFromPool();
        obstacle.transform.position = spawnPos;
        obstacle.SetActive(true);

        activeObstacles.Add(obstacle);
    }

    Vector3 GetRandomPositionAroundPlayer()
    { 
        Vector2 randomCircle = Random.insideUnitCircle * spawnRange;
        Vector3 spawnPos = player.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        return spawnPos;
    }

    GameObject GetObstacleFromPool()
    {
        if (pooledObstacles.Count > 0)
        {
            return pooledObstacles.Dequeue();
        }
        else
        {
            int index = Random.Range(0, obstaclePrefabs.Length);
            GameObject newObstacle = Instantiate(obstaclePrefabs[index]);
            newObstacle.SetActive(false);
            return newObstacle;
        }
    }

    void CheckObstacleDistance()
    {
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = activeObstacles[i];
            float distance = Vector3.Distance(player.position, obstacle.transform.position);

            if (distance > despawnRange)
            {
                obstacle.SetActive(false);
                activeObstacles.RemoveAt(i);
                pooledObstacles.Enqueue(obstacle);
            }
        }
    }
}

