using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    
    public float nexWayPointDistance = 3f;

    public float maxHitPoint;
    public float hitPoint;
    public float speed;
    public float dmg;
    bool isSlowed = false;
    public float slowTime = 0.0f;
    public float slowTimeMax = 4.0f;
    public float normalspeed;
    public float slowedSpeed;
    bool isAlive = true;
    //Path the AI following
    Path path;
    //Setup Astar pathfinding
    public Transform target;
    public AstarPath AstarObj;
    int currentWaypoint = 0;
    bool reachedEnd = false;

    Seeker seeker;
    Rigidbody2D rb;
    
    public Transform enemyImg;
    float enemyScaleX;
    float enemyScaleY;

    

    GridNode randomNode;
    Vector2 WanderPoint;
    // Start is called before the first frame update
    public void SetupVarible()
    {
        enemyScaleX = transform.localScale.x;
        enemyScaleY = transform.localScale.y;
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        //InvokeRepeating("UpdatePathGen",0f,0.5f);

    }

    void seekFunc()
    {
        InvokeRepeating("UpdatePathGen", 0f, 0.5f);
    }

    void WanderFunc()
    {
        
    }

    //func to keep updating A* path
    public void UpdatePathGen()
    {
        if (seeker.IsDone())
        {
            AstarObj.Scan();
            seeker.StartPath(rb.position, target.position, PathComplete);
        }
    }

    public void UpdateRandGen()
    {
        if (seeker.IsDone())
        {
            AstarObj.Scan();
            seeker.StartPath(rb.position, WanderPoint, PathComplete);
        }
    }
    // Update is called once per frame
    public void SeekerFixedUpdate()
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

        if(isSlowed == false)
        {
            speed = normalspeed;
        }
        else
        {
            speed = slowedSpeed;
        }

        if(slowTime >0.0f)
        {
            slowTime -= Time.deltaTime;
        }
        else
        {
            isSlowed = false;
            slowTime = slowTimeMax;
        }

        Vector2 dir =((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;

        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nexWayPointDistance)
        {
            currentWaypoint++;
        }

        //Swap Img direction
        if(rb.velocity.x>= 0.01f)
        {
            enemyImg.localScale = new Vector3(-enemyScaleX, enemyScaleY, 1f);
        }

        if (rb.velocity.x <= -0.01f)
        {
            enemyImg.localScale = new Vector3(enemyScaleX, enemyScaleY, 1f);
        }
    }


    public void WanderFixedUpdate()
    {
        if (path == null)
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

        if (isSlowed == false)
        {
            speed = normalspeed;
        }
        else
        {
            speed = slowedSpeed;
        }

        if (slowTime > 0.0f)
        {
            slowTime -= Time.deltaTime;
        }
        else
        {
            isSlowed = false;
            slowTime = slowTimeMax;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;

        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nexWayPointDistance)
        {
            currentWaypoint++;
        }

        //Swap Img direction
        if (rb.velocity.x >= 0.01f)
        {
            enemyImg.localScale = new Vector3(-enemyScaleX, enemyScaleY, 1f);
        }

        if (rb.velocity.x <= -0.01f)
        {
            enemyImg.localScale = new Vector3(enemyScaleX, enemyScaleY, 1f);
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

   public void slowdown(float val)
    {
        isSlowed = true;
        slowedSpeed = val;
    }

    public void receiveDmg(float Dmgval)
    {
        hitPoint -= Dmgval;
        if(hitPoint <= 0)
        {
            hitPoint = 0;
        }
    }

    public void checkDead()
    {
        if(hitPoint == 0)
        {
            isAlive = false;
            deadFunc();
        }
    }

    public void deadFunc()
    {
        //do something when dead
        print("Oof one of the enemies just died");
    }

    public void MakeRandNode()
    {
        var grid = AstarPath.active.data.gridGraph;
        randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
        
        while(randomNode.Walkable == false)
        {
            randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
        }
        Vector2 tempVec;
        tempVec.x = randomNode.position.x;
        tempVec.y = randomNode.position.y;

        WanderPoint = tempVec;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //do something when collided with other stuff
    }

}
