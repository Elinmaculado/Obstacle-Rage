using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public class ChasingObstacle : MonoBehaviour
{
    public Base_ChasingObs obstacleData;
    private GameObject player;
    [HideInInspector] public NavMeshAgent Agent { get; private set; }
    private Renderer _renderer;

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        _renderer = GetComponentInChildren<Renderer>();
        GetComponent<Collider>().isTrigger = true;
    }

    public void Initialize(Base_ChasingObs data, GameObject playerObj)
    {
        obstacleData = data;
        player = playerObj;
        obstacleData.OnSpawn(this, player);
    }

    void Update()
    {
        if (obstacleData != null && player != null)
            obstacleData.OnUpdate(this, player);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            obstacleData.OnTouchPlayer(this, player);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
            obstacleData.OnStayTouch(this, player, Time.deltaTime);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            obstacleData.OnLeavePlayer(this, player);
    }

    public IEnumerator DespawnAfterTime(float t)
    {
        yield return new WaitForSeconds(t);

        if (obstacleData != null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                obstacleData.OnDespawn(this, playerObj);
        }

        Destroy(gameObject);
    }


    public void SetColor(Color color)
    {
        if (_renderer != null)
            _renderer.material.color = color;
    }
}
