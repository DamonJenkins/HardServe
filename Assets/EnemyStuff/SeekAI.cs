using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class SeekAI : EnemyAI
{
   
    // Start is called before the first frame update
    void Start()
    {
        SetupVarible();
        InvokeRepeating("UpdatePathGen", 0f, 0.5f);
    }

    // Update is called once per frame
    private void Update()
    {
        checkDead();
    }
    private void FixedUpdate()
    {
        SeekerFixedUpdate();
    }
}
