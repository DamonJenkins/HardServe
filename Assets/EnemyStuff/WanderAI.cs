using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class WanderAI : EnemyAI
{
    public float chargingCooldown = 3.5f;
    public float currentCooldown = 3.5f;
    public bool isCharging = false;
    // Start is called before the first frame update
    void Start()
    {
        SetupVarible();
        WanderFunc();
    }

    // Update is called once per frame
    private void Update()
    {
        checkDead();
        if(isCharging)
        {
            currentCooldown -= Time.deltaTime;
            if(currentCooldown <= 0)
            {
                isCharging = false;
                currentCooldown = chargingCooldown;
                WanderFunc();
            }
        }
        else if(checkXYdifference())
        {
            ChargeToPlayer();
            isCharging = true;
        }
        
        //print("Distance to player: " + getDistToPlayer());

    }
    private void FixedUpdate()
    {
        if (isCharging == false)
        {
            WanderFixedUpdate();
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
