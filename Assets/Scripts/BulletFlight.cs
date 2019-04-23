using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlight : MonoBehaviour {


    public float bulletSpeed;
    public float bulletDamage;
    
    private GameObject topBorder = null, leftBorder = null, rightBorder = null, bottomBorder = null;
    
    void Start() {
        topBorder = GameObject.Find("TopBorder");
        leftBorder = GameObject.Find("LeftBorder");
        rightBorder = GameObject.Find("RightBorder");
        bottomBorder = GameObject.Find("BottomBorder");
    }

	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
        if ((this.transform.position.x < leftBorder.transform.position.x) ||
            (this.transform.position.x > rightBorder.transform.position.x) ||
            (this.transform.position.y > topBorder.transform.position.y) ||
            (this.transform.position.y < bottomBorder.transform.position.y))
            Destroy(this.gameObject, 0.0f);
	}
}
