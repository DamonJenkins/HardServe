using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGenScript : MonoBehaviour
{

    [SerializeField]
    private Transform wallParent, groundParent, holeParent;

    [SerializeField]
    private GameObject wallStraight, wallCorner, groundTile, hole;

    [SerializeField]
    private int levelRadiusH = 4, levelRadiusW = 9;

    // Start is called before the first frame update
    void Start()
    {
        bool[,] tiles = new bool[levelRadiusW * 2, levelRadiusH * 2];
        Instantiate(wallCorner, new Vector3(-levelRadiusW, - levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 180.0f), wallParent);
        Instantiate(wallCorner, new Vector3(1 + levelRadiusW, 1 - levelRadiusH), Quaternion.Euler(0.0f, 0.0f, -90.0f), wallParent);
        Instantiate(wallCorner, new Vector3(levelRadiusW, 2 + levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 0.0f), wallParent);
        Instantiate(wallCorner, new Vector3(-1 - levelRadiusW, 1 + levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 90.0f), wallParent);
        for (int x = 0; x < levelRadiusW * 2; x++)
        {

            Instantiate(wallStraight, new Vector3(1 + x - levelRadiusW,  - levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 180.0f), wallParent);
            for (int y = 0; y < levelRadiusH * 2; y++)
            {
                tiles[x, y] = Random.Range(0.0f, 1.0f) < 0.8f;
                Instantiate(
                    tiles[x, y] ? groundTile : hole,
                    new Vector3(x - levelRadiusW, levelRadiusH + 1 - y),
                    Quaternion.identity,
                    tiles[x, y] ? groundParent : holeParent
                );
            }
            Instantiate(wallStraight, new Vector3(x - levelRadiusW, 2 + levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 0.0f), wallParent);
        }
        for (int y = 0; y < levelRadiusH * 2; y++)
        {
            Instantiate(wallStraight, new Vector3(-1 - levelRadiusW, levelRadiusH - y), Quaternion.Euler(0.0f, 0.0f, 90.0f), wallParent);
            Instantiate(wallStraight, new Vector3(1 + levelRadiusW, levelRadiusH + 1 - y), Quaternion.Euler(0.0f, 0.0f, -90.0f), wallParent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
