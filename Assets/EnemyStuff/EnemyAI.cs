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
    public float normalspeed;
    float slowAmount = 0.0f;
	float slowScalar = 1.25f;

	public float dmg;
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

   
    // Start is called before the first frame update
    public void SetupVarible()
    {
        enemyScaleX = transform.localScale.x;
        enemyScaleY = transform.localScale.y;
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

		target = GameObject.Find("PlayerPrefab").GetComponent<Transform>();
		AstarObj = GameObject.Find("A_ Obj").GetComponent<AstarPath>();
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

		speed = normalspeed - (slowAmount * Mathf.Log(slowScalar));

		slowAmount -= Time.deltaTime;
		slowAmount = slowAmount <= 0.0f ? 0.0f : slowAmount;

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

		speed = normalspeed - (slowAmount * Mathf.Log(slowScalar));

		slowAmount -= Time.deltaTime;
		slowAmount = slowAmount <= 0.0f ? 0.0f : slowAmount;

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

   public void SlowDown(float val)
    {
		slowAmount += val;
    }

    public void receiveDmg(float Dmgval)
    {
        hitPoint -= Dmgval;
        if(hitPoint <= 0.0f)
        {
			Destroy(gameObject);
		}
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
        Vector2 force;

		force = dir * speed * Time.deltaTime * 180.0f;

		rb.AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
		Player hitPlayer = _collision.gameObject.GetComponent<Player>();
		if (hitPlayer != null)
		{
			hitPlayer.Damage(1);
		}
	}

	public void OnDestroy()
	{
		if (Random.Range(0, 8) < 1)
		{
			Instantiate(Resources.Load("Prefabs/ItemPrefab"), transform.position, Quaternion.identity);
		}
		else if(Random.Range(0, 5) < 1)
		{
			Instantiate(Resources.Load("Prefabs/HealthUp"), transform.position, Quaternion.identity);
		}
	}
}
