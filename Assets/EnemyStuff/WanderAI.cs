using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class WanderAI : EnemyAI
{
    Rigidbody2D wanderRB;
    public float chargingCooldown = 3.5f;
    public float currentCooldown = 0f;
    public bool isCharging = false;
    public bool chargingX = false;
    public bool chargingY = false;
    // Start is called before the first frame update
    void Start()
    {
        wanderRB = GetComponent<Rigidbody2D>();
        SetupVarible();
        WanderFunc();
        //print("start() called wanderfunc");
    }

    // Update is called once per frame
    private void Update()
    {
        checkDead();
        //if(isCharging)
        //{
        //    currentCooldown -= Time.deltaTime;
        //    if(currentCooldown <= 0)
        //    {
        //        isCharging = false;
        //        currentCooldown = chargingCooldown;
        //        WanderFunc();
        //    }
        //}
        //isCharging = getChargingStatus();

        if(isCharging)
        {
            Vector2 tempVeloctiy;
            tempVeloctiy = wanderRB.velocity;
            if(Mathf.Abs(tempVeloctiy.x)>Mathf.Abs(tempVeloctiy.y))
            {
                chargingX = true;
                chargingY = false;
            }

            if (Mathf.Abs(tempVeloctiy.x) < Mathf.Abs(tempVeloctiy.y))
            {
                chargingX = false;
                chargingY = true;
            }
        }
        else
        {
            chargingX = false;
            chargingY = false;
        }
        if((isCharging == false) && (currentCooldown >0) )
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0)
            {
                currentCooldown = 0;
               // WanderFunc();
               // print("update called wanderfunc");
            }
        }
            
        if(checkXYdifference() && (isCharging == false) && (currentCooldown == 0))
        {
            ChargeToPlayer();
            isCharging = true;
        }
        
        //print("Distance to player: " + getDistToPlayer());

    }
    private void FixedUpdate()
    {
        if ((isCharging == false) && (currentCooldown >= 0))
        {
            WanderFixedUpdate();
        }
        else
        {
            //keep charging
            keepCharging();
        }
       
    }

   

    //Use this not trigger
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag != "Default" && isCharging)
        {
            if(collision.gameObject.tag == "Wall")
            {

                if(chargingX)
                {
                    if(wanderRB.velocity.x > 0)
                    {
                        if(collision.gameObject.transform.localPosition.x > wanderRB.transform.localPosition.x)
                        {
                            print("collided with something!");
                            isCharging = false;
                            currentCooldown = chargingCooldown;
                            WanderFunc();
                        }
                    }
                    else
                    {
                        if (collision.gameObject.transform.localPosition.x < wanderRB.transform.localPosition.x)
                        {
                            print("collided with something!");
                            isCharging = false;
                            currentCooldown = chargingCooldown;
                            WanderFunc();
                        }
                    }
                    
                }

                if (chargingY)
                {
                    if (wanderRB.velocity.y > 0)
                    {
                        if (collision.gameObject.transform.localPosition.y > wanderRB.transform.localPosition.y)
                        {
                            print("collided with something!");
                            isCharging = false;
                            currentCooldown = chargingCooldown;
                            WanderFunc();
                        }
                    }
                    else
                    {
                        if (collision.gameObject.transform.localPosition.y < wanderRB.transform.localPosition.y)
                        {
                            print("collided with something!");
                            isCharging = false;
                            currentCooldown = chargingCooldown;
                            WanderFunc();
                        }
                    }

                }
            }else
            {
                print("collided with something!");
                isCharging = false;
                currentCooldown = chargingCooldown;
                WanderFunc();
            }

            
            
        }
    }


}



    //public void MakeRandNode()
    //{
    //    if (!Wai.pathPending && (Wai.reachedEndOfPath || !Wai.hasPath))
    //    {
    //        Wai.destination = PickRandomPoint();
    //        Wai.SearchPath();
    //    }
    //}

    //Vector2 PickRandomPoint()
    //{
    //    var point = Random.insideUnitSphere * WanderRadius;

    //    point.z = 0;
    //    point += transform.position;
    //    return point;
