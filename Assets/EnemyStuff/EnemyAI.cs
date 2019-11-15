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
    public float currentDmgCd = 0.0f;
    public float dmgCd = 1.5f;
    public bool CanDoDmg = true;
    //for charging enemy
    public Vector2 chargeforce;
    public bool isCharger = false;
    bool ChargerCharging = false;


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
    Animator myAnimator;
    public Transform enemyImg;
    float enemyScaleX;
    float enemyScaleY;

   
    // Start is called before the first frame update
    public void SetupVarible()
    {
        enemyScaleX = transform.localScale.x;
        enemyScaleY = transform.localScale.y;
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        //myAnimator.transform.localScale *= 0.25f;
        //InvokeRepeating("UpdatePathGen",0f,0.5f);

    }

    public void seekFunc()
    {
        InvokeRepeating("UpdatePathGen", 0f, 0.5f);
    }

    public void WanderFunc()
    {
        InvokeRepeating("UpdateRandGen", 0f, 4.5f);
        
        
    }

    public void DisableWanderFunc()
    {
        CancelInvoke("UpdateRandGen");


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
            seeker.StartPath(rb.position, BFSWanderPoint(), PathComplete);
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
    private void Update()
    {
        if(currentDmgCd >0 && CanDoDmg == false)
        {
            currentDmgCd -= Time.deltaTime;
            if(currentDmgCd <= 0)
            {
                currentDmgCd = 0;
                CanDoDmg = true;
            }
        }

        
        
        //myAnimator.SetBool("GoingLeft", rb.velocity.x < 0.0f);
        //myAnimator.SetBool("GoingRight", rb.velocity.x > 0.0f);
        //myAnimator.SetBool("GoingUp", rb.velocity.y > 0.0f);
        //myAnimator.SetBool("GoingDown", rb.velocity.y < 0.0f);
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
        Destroy(gameObject);
    }

    public Vector3 BFSWanderPoint()
    {
        // Get a random point for wander
        var startNode = AstarPath.active.GetNearest(transform.position, NNConstraint.Default).node;
        var nodes = PathUtilities.BFS(startNode, 100);
        var singleRandomPoint = PathUtilities.GetPointsOnNodes(nodes, 1)[0];
        //var multipleRandomPoints = PathUtilities.GetPointsOnNodes(nodes, 100);
        return singleRandomPoint;
    }

    public float getDistToPlayer()
    {
        return Vector3.Distance(target.position, rb.position); 
    }

    public bool checkXYdifference()
    {
        if(Mathf.Abs(target.position.x - rb.position.x) <= 0.3 || Mathf.Abs(target.position.y - rb.position.y) <= 0.3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

   public void ChargeToPlayer()
    {
        DisableWanderFunc();
        rb.velocity.Scale(Vector2.zero);
        
        Vector2 playerLoc;
        playerLoc.x = target.position.x;
        playerLoc.y = target.position.y;
        Vector2 dir = (playerLoc - rb.position).normalized;
        Vector2 force ;
        if (isSlowed)
        {
            force = dir * slowedSpeed * Time.deltaTime * 8.0f;
        }
        else
        {
            force = dir * speed * Time.deltaTime * 8.0f;
        }
        chargeforce = force;
        ChargerCharging = true;
        print("charging to player");

    }

    public void keepCharging()
    {
        if(isCharger)
        {
            rb.AddForce(chargeforce);
        }
    }

    public void cancellCharging()
    {

    }

    public bool getChargingStatus()
    {
        return ChargerCharging;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //do something when collided with other stuff
        

        if (collision.tag == "Player" && CanDoDmg)
        {
            //do damage
            currentDmgCd = dmgCd;
            CanDoDmg = false;
        }

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player" )&& CanDoDmg)
        {
            //do damage
            currentDmgCd = dmgCd;
            CanDoDmg = false;
        }
    }

}
