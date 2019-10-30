using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //Enemy Stats
    //Change public to private later
    public float maxHitPoint;
    public float hitPoint;
    public float speed;
    public float dmg;

    // Start is called before the first frame update
    void Start()
    {
        hitPoint = maxHitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AImove()
    {

    }

    void DoMove()
    {

    }
}
