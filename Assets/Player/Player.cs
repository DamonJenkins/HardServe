using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    int currentWeapon = 0;
    float maxSpeed = 7.0f;

    float shotTimer = 0.0f;
    float fireRate = 1.5f;

    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        shotTimer -= Time.deltaTime;

        Vector2 tempVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        GetComponent<Rigidbody2D>().velocity = tempVelocity.SqrMagnitude() > 1.0f ? tempVelocity.normalized * maxSpeed : tempVelocity * maxSpeed;

        if (CanShoot()) {
            bool didShoot = false;
            if (Input.GetAxis("FireU") > 0.1f){
                Debug.Log("Shot Up");
                didShoot = true;
            }
            else if(Input.GetAxis("FireD") > 0.1f){

                didShoot = true;
            }
            else if(Input.GetAxis("FireL") > 0.1f){

                didShoot = true;
            }
            else if(Input.GetAxis("FireR") > 0.1f){

                didShoot = true;
            }

            if (didShoot) {

                shotTimer = 1.0f / fireRate;
            }
        }

        if (Input.GetButtonDown("SwitchLeft")){
            if (currentWeapon == 0) currentWeapon = 2;
            else currentWeapon -= 1;
        }
        else if (Input.GetButtonDown("SwitchRight")){
            if (currentWeapon == 2) currentWeapon = 0;
            else currentWeapon += 1;
        }
    }

    private bool CanShoot() {
        return shotTimer <= 0.0f;
    }
}
