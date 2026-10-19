using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public EnemyView eView;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (eView.isDetected)
        {
            //agent.SetDestination(player.transform.position);
        }
        else
        {
        }
        
    }
}
