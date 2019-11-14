using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoiBullet : MonoBehaviour
{
    public float dmg;
    public Vector2 vel;
    public float maxLiveTime = 2.5f;
    public float currLiveTime = 2.5f;
    // Start is called before the first frame update
    public void Initialize(float rolledDmg, Vector2 rolledVel)
    {
        dmg = rolledDmg;
        vel = rolledVel;
        GetComponent<Rigidbody2D>().velocity = vel;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currLiveTime -= Time.deltaTime;
        if(currLiveTime <=0)
        {
            Destroy(gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //deal damage to player
            Destroy(gameObject);

        }
        else if(collision.gameObject.tag != "Default" && collision.gameObject.tag != "Enemies")
        {
            Destroy(gameObject);
        }
    }
}
