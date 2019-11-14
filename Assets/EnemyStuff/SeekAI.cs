using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class SeekAI : EnemyAI
{
    public bool isBigBoi = false;
    public float[] CurrfireCD = { 0.0f, 0.0f, 0.0f,0.0f };
    public float[] MaxfireCD = { 2.5f, 2.5f, 2.5f, 2.5f };
    public BigBoiBullet BigBoiBull;
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
        if(isBigBoi)
        {
            //control firing CD
            for(int i = 0; i<=3;i++)
            {
                if(CurrfireCD[i] > 0)
                {
                    CurrfireCD[i] -= Time.deltaTime;
                    if(CurrfireCD[i] <0)
                    {
                        CurrfireCD[i] = 0;
                    }
                    
                }
                else
                {
                    fireFunc(i);
                    CurrfireCD[i] = MaxfireCD[i];
                }
            }
            


        }
    }
    private void FixedUpdate()
    {
        SeekerFixedUpdate();
    }


    void fireFunc(int fireSlot)
    {
        //from 0 to 3: up,right,down,left
        //             0   1     2    3
        //float rolledDmg = Random.Range(70.0f, 150.0f);
        float bullDmg = 90.0f;
        Vector2 direction = Vector2.zero;
        switch (fireSlot)
        {
            case 0:
                direction = Vector2.up;
                break;
            case 1:
                direction = Vector2.right;
                break;
            case 2:
                direction = Vector2.down;
                break;
            case 3:
                direction = Vector2.left;
                break;
            default:
                break;
        }
        Vector2 finalVel;
        //float rolledSpd = Random.Range(5.0f, 15.0f);
        float bullspd = 7.0f;
        finalVel = direction * bullspd;



        //GameObject.Instantiate(BigBoiBull, transform.position, Quaternion.identity).Initialize(rolledDmg, finalVel);
        GameObject.Instantiate(BigBoiBull, transform.position, Quaternion.identity).Initialize(bullDmg, finalVel);
    }
}
