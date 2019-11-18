using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Cherry_Stage2 : MonoBehaviour
{
	Animator myAnimator;

	public bool exploded = false;

    List<GameObject> enemiesInRange = new List<GameObject>();

    float fuseTime = 1.5f;

    //How long the bomb has left until it explodes
    float existenceTimer;

    // Start is called before the first frame update
    void Start()
    {
        existenceTimer = fuseTime;

		myAnimator = GetComponent<Animator>();
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("sfxVolume");

    }

    // Update is called once per frame
    void Update()
    {
		myAnimator.speed = (((fuseTime - existenceTimer) / fuseTime) * 3.0f) + 1.0f;

		if (exploded) Destroy(gameObject);

        existenceTimer -= Time.deltaTime;
		if (existenceTimer <= 0.0f && !exploded) {
			//Explode
			myAnimator.speed = 1.0f;
			GetComponent<Animator>().SetTrigger("Explode");
			foreach (GameObject enemy in enemiesInRange)
			{
				enemy.GetComponent<EnemyAI>().receiveDmg(Shot_Cherry.explosionDamage);
			}
            if(!GetComponent<AudioSource>().isPlaying )GetComponent<AudioSource>().Play();
		}
    }

    private void OnDestroy()
	{
		
	}

    private void OnTriggerEnter2D(Collider2D _collision){
		if (!enemiesInRange.Contains(_collision.gameObject)) {
			enemiesInRange.Add(_collision.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D _collision){
		if (enemiesInRange.Contains(_collision.gameObject)) {
			enemiesInRange.Remove(_collision.gameObject);
		}
	}
}
