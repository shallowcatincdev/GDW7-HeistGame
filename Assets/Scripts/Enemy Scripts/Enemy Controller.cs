using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    //TODO: call array position for enemies to go to player, 

    public GameObject enemyPrefab;
    public GameObject[] enemies;
    public GameObject[] spawnPoints;

    private Enemy enemyScript;
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

    }

    public void NearEnemies(float distance) //distance to other enemies
    {
        foreach (var obj in enemies)
        {
            if (Vector3.Distance(enemyScript.transform.position, obj.transform.position) < distance)
            {
                //enemyScript.GoToPlayer();
                //for (int i = 0; i < enemyController.enemies.Length; i++)
                {
                    Debug.Log(enemies + " has detected the player");

                    enemyScript.agent.SetDestination(player.transform.position);
                    enemyScript.hasDestination = true;
                    enemyScript.GoToPlayer();
                    //enemyScript.loud = true;
                    
                    if (player.detectionRate <= 10)
                    {
                        enemyScript.hasDestination = false;

                    }
                }
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1f);
        
        foreach (var t in spawnPoints)
        {
            Instantiate(enemyPrefab, t.transform.position, Quaternion.identity);
            //Debug.Log($"Enemy: {enemies[i].transform.position}");
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //gets the enemies and puts them into the array
    }
}
