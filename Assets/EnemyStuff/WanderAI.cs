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
        if((isCharging == false) && (currentCooldown >0) )
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0)
            {
                currentCooldown = 0;
                WanderFunc();
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
        if ((isCharging == false) && (currentCooldown > 0))
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
            
            print("collided with something!");
            isCharging = false;
            currentCooldown = chargingCooldown;
            WanderFunc();
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
