using System;
using System.Collections;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public PlayerController player;
    private Transform playerTransform;
    public bool isDetected;

    [SerializeField] private float timeBeforeAlert = 5.0f;

    private RaycastHit hit;

    private Enemy enemy;
    //public EnemyController enemyController;

    //private enum alertlevel {Level1, Level2, Level3 };

    //TODO: Add raycast from enemy to player's position - make sure the enemy can actually see the player
    //When investigating the player move to the raycast position
    //Alert nearby enemies to the raycast position if the player is still there
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //enemyController = GameObject.Find("Enemy Controller");
        //enemyController = FindAnyObjectByType<EnemyController>();

    }

    void Update()
    {
        if (player.detectionRate < 0)
        {
            player.detectionRate = 0;
        }

        if (player.detectionRate > 100)
        {
            player.detectionRate = 100;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //raycast to player from enemy once the player enters the hitbox of the enemy view
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            Vector3 normalizedDirection = directionToPlayer.normalized;
            if (Physics.Raycast(transform.position, normalizedDirection, out hit))
            {
                if (hit.transform == playerTransform)
                {
                    //Debug.Log("Player is visible!");
                    StartCoroutine(Investigate(timeBeforeAlert));
                }
                else
                {
                    //Debug.Log($"Line of sight blocked by: {hit.transform.name}");
                }
            }
            isDetected = true;
            StopCoroutine(DecreaseDetectionRating());
            StartCoroutine(IncreaseDetectionRating());
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDetected = false;
            StopCoroutine(IncreaseDetectionRating());
            StartCoroutine(DecreaseDetectionRating());
        }
    }

    IEnumerator IncreaseDetectionRating()
    {
        while (isDetected)
        {
            if (player.isCrouched && player.isInBush == false)// is crouched
            {
                yield return new WaitForSeconds(1.0f);
                //increase by 1
                player.detectionRate += 1;
            }

            if (player.isProned && player.isInBush == false)//is prone
            {
                yield return new WaitForSeconds(2.0f);
                player.detectionRate += 1;
            }

            if (player.isSprinting && player.isInBush == false)//is sprinting
            {
                yield return new WaitForSeconds(0.25f);
                player.detectionRate += 1;
            }

            if (player.isCrouched == false && player.isProned == false && player.isSprinting == false && player.isInBush == false)//no movement modifier
            {
                yield return new WaitForSeconds(0.5f);
                player.detectionRate += 1;
            }
            


        }
        
    }

    IEnumerator DecreaseDetectionRating()
    {
        while (isDetected == false && player.detectionRate > 1)
        {
            if (player.isInBush == false)
            {
                yield return new WaitForSeconds(3.5f);
                player.detectionRate -= 1;
            }

            if (player.isInBush)
            {
                yield return new WaitForSeconds(0.25f);
                player.detectionRate -= 5;
            }

        }
        
    }

    IEnumerator Investigate(float time)
    {
        time = timeBeforeAlert;
        //this makes the enemy go to where it saw the player
        if (hit.collider.tag == "Player" && enemy.loud == false)
        {
            enemy.hasDestination = true;
            //go to the raycast point
            enemy.agent.SetDestination(hit.transform.position);
            yield return new WaitForSeconds(time);
            enemy.hasDestination = false;
            StopCoroutine(Investigate(timeBeforeAlert));
        }
    }
        
        /*if the player is seen with a detection rating of higher than 80, alert3 is started and get all enemies to the player's current position (heist goes loud)

            GameObject[] nearByEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var obj in nearByEnemies)
            {
                if (Vector3.Distance(enemy.transform.position, obj.transform.position) < 30000f)
                {
                    for (int i = 0; i < nearByEnemies.Length; i++)
                    {
                        
                        Debug.Log(nearByEnemies[i] + " is detected");

                        nearByEnemies[i].GetComponent<Enemy>().agent.SetDestination(player.transform.position);
                        nearByEnemies[i].GetComponent<Enemy>().hasDestination = true;
                        nearByEnemies[i].GetComponent<Enemy>().loud = true;
                        nearByEnemies[i].GetComponent<Enemy>().GoToPlayer();
                    }
                }
            }
        }
        */
}
