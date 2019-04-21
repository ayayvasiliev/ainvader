using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlight : MonoBehaviour {


    public float bulletSpeed;
    public float bulletDamage;

    
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
	}
}
