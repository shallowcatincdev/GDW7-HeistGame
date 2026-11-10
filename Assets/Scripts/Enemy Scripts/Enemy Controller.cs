using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    //TODO: call array position for enemies to go to player, 

    public GameObject enemyPrefab;
    public List<Enemy> enemies = new List<Enemy>();
    private GameObject[] spawnPoints;

    private Enemy enemyScript;
    private EnemyView enemyView;
    private PlayerController player;
    
    
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");
        enemyScript = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        
        StartCoroutine(SpawnEnemies());
        
    }

    // Update is called once per frame
    void Update()
    {
        AlertLevel1();
    }

    public void NearEnemies(float distance) //distance to other enemies
    {
        foreach (Enemy obj in enemies)
        {
            if (obj == null)
            {
                
            }
            else if (Vector3.Distance(player.transform.position, obj.transform.position) <= distance)
            {
                //obj.GoToPlayer();
                //for (int i = 0; i < enemyController.enemies.Length; i++)

                //Debug.Log(enemies + " has detected the player");

                obj.agent.SetDestination(player.transform.position);
                obj.hasDestination = true;
                obj.GoToPlayer();
                //obj.loud = true;

                if (player.detectionRate <= 10)
                {
                    obj.hasDestination = false;

                }
                
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(0.1f);
        
        foreach (var t in spawnPoints)
        {
            GameObject temp = Instantiate(enemyPrefab, t.transform.position, Quaternion.identity);
            enemies.Add(temp.GetComponent<Enemy>());
            //Debug.Log($"Enemy: {enemies[i].transform.position}");
        }
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //gets the enemies and puts them into the array
    }
    private void AlertLevel1()
    {
        if (player.detectionRate is >= 30 and < 60)
        {
            NearEnemies(100f);
        }
        
    }
    private void AlertLevel2()
    {
        if (player.detectionRate is >= 60 and < 80)
        {
            NearEnemies(25f);
        }
    }

    private void AlertLevel3()
    {
        if (player.detectionRate >= 80)
        {
            NearEnemies(1000f);
        }
    }
}
