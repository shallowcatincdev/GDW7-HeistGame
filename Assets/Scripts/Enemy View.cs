using System;
using System.Collections;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public PlayerController player;

    public bool isDetected;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerController>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isDetected = true;
            StopCoroutine(DecreaseDetectionRating());
            StartCoroutine(IncreaseDetectionRating());
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
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
            if (player.isCrouched && player.isInBush == false)
            {
                yield return new WaitForSeconds(0.40f);
                //increase by 1
                player.detectionRate += 1;
            }

            if (player.isProned && player.isInBush == false)
            {
                yield return new WaitForSeconds(1.0f);
                player.detectionRate += 1;
            }

            if (player.isSprinting && player.isInBush == false)
            {
                yield return new WaitForSeconds(0.10f);
                player.detectionRate += 1;
            }

            if (player.isCrouched == false && player.isProned == false && player.isSprinting == false && player.isInBush == false)
            {
                yield return new WaitForSeconds(0.25f);
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
                yield return new WaitForSeconds(0.25f);
                player.detectionRate -= 1;
            }

            if (player.isInBush)
            {
                yield return new WaitForSeconds(0.25f);
                player.detectionRate -= 5;
            }

        }

    }
}
