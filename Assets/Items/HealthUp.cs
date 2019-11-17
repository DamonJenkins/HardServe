using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D _collision)
	{
		Player plr = _collision.gameObject.GetComponent<Player>();

		if (plr != null)
		{
			plr.Heal(1);
			Destroy(gameObject);
		}
	}
}
