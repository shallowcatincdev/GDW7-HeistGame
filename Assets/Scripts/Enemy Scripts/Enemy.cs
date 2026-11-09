using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    //Wander Script from:
    //https://discussions.unity.com/t/solved-random-wander-ai-using-navmesh/581895/2 

    private PlayerController player;
    private EnemyController enemyController;
    
    public NavMeshAgent agent;
    
    public float wanderRadius = 10.0f;
    public float wanderTimer = 300.0f;

    public bool hasDestination;
    public bool loud;

    private Transform target;
    private float timer;

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent> ();
        timer = wanderTimer;
        player = GameObject.Find ("Player").GetComponent<PlayerController> ();
        enemyController = GameObject.Find("Enemy Controller").GetComponent<EnemyController>();
    }
  
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if (timer >= wanderTimer && hasDestination == false && loud == false) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        if (player.detectionRate <=10)
        {
            hasDestination = false;
        }

    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public void GoToPlayer()
    {
        agent.destination = player.transform.position;
    }
    

}
