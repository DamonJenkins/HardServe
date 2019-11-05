using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public AstarPath AstarObj;
    public float speed;
    public float nexWayPointDistance = 3f;

    //Path the AI following
    Path path;
    int currentWaypoint = 0;
    bool reachedEnd = false;

    Seeker seeker;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePathGen",0f,0.5f);
        
    }

    //func to keep updating A* path
    void UpdatePathGen()
    {
        if (seeker.IsDone())
        {
            AstarObj.Scan();
            seeker.StartPath(rb.position, target.position, PathComplete);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        Vector2 dir =((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;

        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nexWayPointDistance)
        {
            currentWaypoint++;
        }



    }

    void PathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
