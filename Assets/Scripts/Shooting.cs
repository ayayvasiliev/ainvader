using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public float fireRate = 100;

    float timeUntilFire = 0;

    public Transform firePoint;
    public GameObject bullet;

    private GunRotation gunRot = null;

	// Use this for initialization
	void Start () {
        gunRot = GameObject.Find("Gun").GetComponent<GunRotation>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0) && Time.time > timeUntilFire)
        {
            timeUntilFire = Time.time + 1 / fireRate;
            Shoot();
        }
	}

    void Shoot()
    {
        Debug.Log(firePoint.rotation.eulerAngles.ToString());
        Instantiate(bullet, firePoint.position, Quaternion.Euler(0, 0, gunRot.Angle - 90));
    }
}
