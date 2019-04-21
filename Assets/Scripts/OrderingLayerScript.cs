using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingLayerScript : MonoBehaviour {

    private SpriteRenderer tempRend;

	// Use this for initialization
	void Start () {
        tempRend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        tempRend.sortingOrder = (int)Camera.main.WorldToScreenPoint(tempRend.bounds.min).y * -1;
	}
}
