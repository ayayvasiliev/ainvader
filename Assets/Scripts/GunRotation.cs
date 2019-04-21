using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour {


    public Transform owner;
    private Vector3 mouse_pos;
    public Transform target;
    private Vector3 object_pos;
    private float angle;

    private float radius = 0.3f;
    private Vector3 desiredPosition;

	
	// Update is called once per frame
	void Update () {

        //transform.position = new Vector3(owner.position.x - 0.1f, owner.position.y, transform.position.z);
        //transform.position = desiredPosition;

        mouse_pos = Input.mousePosition;
        //mouse_pos.x = 10.23f;
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x -= object_pos.x;
        mouse_pos.y -= object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        mouse_pos.Normalize();
        mouse_pos *= radius;
        transform.position = new Vector3(owner.position.x + mouse_pos.x, owner.position.y + mouse_pos.y, (angle < 0 || angle > 180) ? 1 : -1);
    }
}
