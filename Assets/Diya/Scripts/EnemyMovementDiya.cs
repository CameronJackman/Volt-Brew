using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementDiya : MonoBehaviour
{
    //Array allows you to keep track of multiple patrol points
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;
    private float pauseTime = 0.5f;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    public float stopChasingDistance;

    // Update is called once per frame
    void Update()
    {
        //If the enemy is chasing player, follow player in all directions (MoveTowards)
        if (isChasing)
        {
            Vector2 moveToPlayer = Vector2.MoveTowards(transform.position, playerTransform.transform.position, moveSpeed * Time.deltaTime);
            transform.position = moveToPlayer;

            //IF enemy is THIS distance away from player set isChasing = false and enemy will go back to patrolling
            if (Vector2.Distance(transform.position, playerTransform.position) > stopChasingDistance)
            {
                isChasing = false;
            }
        }

        else
        {
            //If the player is in range of the enemy set isChasing = true
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }


            if (patrolDestination == 0)
            {
                
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                    {
                        pauseTime -= Time.deltaTime;
                        if (pauseTime <= 0)
                        {
                            patrolDestination = 1;
                            pauseTime = 0.5f;
                        }
                        
                    }
                
            }
            

            if (patrolDestination == 1)
            {
               
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                    {
                        pauseTime -= Time.deltaTime;

                        if (pauseTime <= 0)
                        {
                            patrolDestination = 0;
                            pauseTime = 0.5f;
                        }
                        
                    }
                }
                
            }
            
        }
    }               
