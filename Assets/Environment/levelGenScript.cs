using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGenScript : MonoBehaviour
{

    [SerializeField]
    private Transform wallParent, groundParent, holeParent, stoolParent;

    [SerializeField]
    private GameObject wallStraight, wallCorner, groundTile, hole, stool;

    [SerializeField]
    private const int levelRadiusH = 4, levelRadiusW = 9;

    private static Vector2 levelPos = new Vector2(0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        Random.State oldState = Random.state;
        Random.InitState(Mathf.FloorToInt(hash21(levelPos) * 100000.0f));

        bool[,] tiles = new bool[levelRadiusW * 2, levelRadiusH * 2];

        //Corners
        Instantiate(wallCorner, new Vector3(-levelRadiusW, - levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 180.0f), wallParent);
        Instantiate(wallCorner, new Vector3(1 + levelRadiusW, 1 - levelRadiusH), Quaternion.Euler(0.0f, 0.0f, -90.0f), wallParent);
        Instantiate(wallCorner, new Vector3(levelRadiusW, 2 + levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 0.0f), wallParent);
        Instantiate(wallCorner, new Vector3(-1 - levelRadiusW, 1 + levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 90.0f), wallParent);
        //Fill
        for (int x = 0; x < levelRadiusW * 2; x++)
        {
            //Bottom wall
            Instantiate(wallStraight, new Vector3(1 + x - levelRadiusW,  - levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 180.0f), wallParent);

            for (int y = 0; y < levelRadiusH * 2; y++)
            {
                //Setting tile existence
                tiles[x, y] = Random.Range(0.0f, 1.0f) < 0.8f || x == 0 || y == 0 || y == levelRadiusH * 2 - 1 || x == levelRadiusW * 2 - 1;

                Instantiate(
                    tiles[x, y] ? groundTile : hole,
                    new Vector3(x - levelRadiusW, levelRadiusH + 1 - y),
                    Quaternion.identity,
                    tiles[x, y] ? groundParent : holeParent
                );

                //Stools
                if( tiles[x,y] && Random.value > 0.85f && !(x == 0 || y == 0 || y == levelRadiusH * 2 - 1 || x == levelRadiusW * 2 - 1)) Instantiate(
                     stool,
                     new Vector3(x - levelRadiusW, levelRadiusH + 1 - y),
                     Quaternion.identity,
                     stoolParent
                 );

            }
            //Top Wall
            Instantiate(wallStraight, new Vector3(x - levelRadiusW, 2 + levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 0.0f), wallParent);
        }
        for (int y = 0; y < levelRadiusH * 2; y++)
        {
            //Left wall
            Instantiate(wallStraight, new Vector3(-1 - levelRadiusW, levelRadiusH - y), Quaternion.Euler(0.0f, 0.0f, 90.0f), wallParent);
            //Right Wall
            Instantiate(wallStraight, new Vector3(1 + levelRadiusW, levelRadiusH + 1 - y), Quaternion.Euler(0.0f, 0.0f, -90.0f), wallParent);
        }
        Random.state = oldState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float hash21(Vector2 p)
    {
        p *= new Vector2(234.34f, 435.345f);
        p = new Vector2(p.x - Mathf.Floor(p.y), p.x - Mathf.Floor(p.y));
        p += new Vector2(Vector2.Dot(p, p + new Vector2(34.23f, 34.23f)), Vector2.Dot(p, p + new Vector2(34.23f, 34.23f)));
        float retVal = p.x * p.y;
        return retVal - Mathf.Floor(retVal);
    }

}
