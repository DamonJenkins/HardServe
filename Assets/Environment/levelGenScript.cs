using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGenScript : MonoBehaviour
{

    [SerializeField]
    private Transform wallParent, groundParent, holeParent, stoolParent, doorParent;

    [SerializeField]
    private GameObject wallStraight, wallCorner, groundTile, hole, stool, door;

    private const int levelRadiusH = 4, levelRadiusW = 8;

    private Vector2 levelPos = new Vector2(0.0f, 0.0f);
    private List<GameObject> doors;
    private List<Vector2> vistedRooms;

    private float timeSinceLevelLoad = 0.0f;

    public Vector2 moveLevelPos(Vector2 v)
    {
        return levelPos += v;
    }

    public void LoadLevel(Vector2 lp)
    {

        if (!vistedRooms.Contains(lp))
        {
            //spawn enemies
        }

        print(levelPos);

        foreach (Transform child in wallParent  ) Destroy(child.gameObject);
        foreach (Transform child in groundParent) Destroy(child.gameObject);
        foreach (Transform child in holeParent  ) Destroy(child.gameObject);
        foreach (Transform child in stoolParent ) Destroy(child.gameObject);
        doors = new List<GameObject>();
        foreach (Transform child in doorParent) Destroy(child.gameObject);

        Random.State oldState = Random.state;
        Random.InitState(Mathf.FloorToInt(hash21(lp) * 1000000.0f));

        bool[,] tiles = new bool[levelRadiusW * 2, levelRadiusH * 2];

        //Corners
        Instantiate(wallCorner, new Vector3(-levelRadiusW, -levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 180.0f), wallParent);
        Instantiate(wallCorner, new Vector3(1 + levelRadiusW, 1 - levelRadiusH), Quaternion.Euler(0.0f, 0.0f, -90.0f), wallParent);
        Instantiate(wallCorner, new Vector3(levelRadiusW, 2 + levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 0.0f), wallParent);
        Instantiate(wallCorner, new Vector3(-1 - levelRadiusW, 1 + levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 90.0f), wallParent);
        //Fill
        for (int x = 0; x < levelRadiusW * 2; x++)
        {
            //Bottom wall
            Instantiate(wallStraight, new Vector3(1 + x - levelRadiusW, -levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 180.0f), wallParent);

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
                if (tiles[x, y] && Random.value > 0.85f && !(x == 0 || y == 0 || y == levelRadiusH * 2 - 1 || x == levelRadiusW * 2 - 1)) Instantiate(
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

        foreach(Transform child in groundParent)
        {
            child.gameObject.GetComponent<SpriteRenderer>().sortingOrder = - 5 - levelRadiusH - (int)child.position.y;
        }

        if (hash21(levelPos + new Vector2(0.5f, 0.0f)) > 0.2f)
        {
            doors.Add(Instantiate(door, new Vector3(1 + levelRadiusW, 1.5f), Quaternion.Euler(0.0f, 0.0f, -90.0f), doorParent));
            doors[doors.Count - 1].GetComponent<doorControl>().setDirection(new Vector2(1.0f, 0.0f));
        }
        if (hash21(levelPos + new Vector2(-0.5f, 0.0f)) > 0.2f)
        {
            doors.Add(Instantiate(door, new Vector3(-1 - levelRadiusW, 0.5f), Quaternion.Euler(0.0f, 0.0f, 90.0f), doorParent));
            doors[doors.Count - 1].GetComponent<doorControl>().setDirection(new Vector2(-1.0f, 0.0f));
        }
        if (hash21(levelPos + new Vector2(0.0f, 0.5f)) > 0.2f)
        {
            doors.Add(Instantiate(door, new Vector3(-0.5f, 2 + levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 0.0f), doorParent));
            doors[doors.Count - 1].GetComponent<doorControl>().setDirection(new Vector2(0.0f, 1.0f));
        }
        if (hash21(levelPos + new Vector2(0.0f, -0.5f)) > 0.2f)
        {
            doors.Add(Instantiate(door, new Vector3(0.5f, -levelRadiusH), Quaternion.Euler(0.0f, 0.0f, 180.0f), doorParent));
            doors[doors.Count - 1].GetComponent<doorControl>().setDirection(new Vector2(0.0f, -1.0f));
        }

        Random.state = oldState;
        timeSinceLevelLoad = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        vistedRooms = new List<Vector2>();
        levelPos = new Vector2(Random.value, Random.value);
        LoadLevel(levelPos);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLevelLoad += Time.deltaTime;
    }

    float fract(float f)
    {
        return f - Mathf.Floor(f);
    }

    Vector2 fract(Vector2 v)
    {
        return new Vector2(fract(v.x), fract(v.y));
    }

    float hash21(Vector2 p)
    {
        return fract(Mathf.Sin(Vector2.Dot(p, new Vector2(12.9898f, 78.233f) * 43758.5453123f)));
    }

    public bool finished()
    {
        return timeSinceLevelLoad > 10.0f;
    }

}
