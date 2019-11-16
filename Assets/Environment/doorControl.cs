using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorControl : MonoBehaviour
{

    private levelGenScript levelGenScript;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        levelGenScript = GameObject.FindGameObjectWithTag("level").GetComponent<levelGenScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && levelGenScript.finished())
        {
            levelGenScript.LoadLevel(levelGenScript.moveLevelPos(direction));
            collision.gameObject.transform.position -= new Vector3(direction.x * 13, direction.y * 5, 0.0f);
        }
    }

    public void setDirection(Vector2 v){
        direction = v;
    }
}
