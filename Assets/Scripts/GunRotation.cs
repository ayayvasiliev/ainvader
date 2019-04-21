using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour {


    public Animator animator;

    public Transform owner;
    private Vector3 mouse_pos;
    public Transform target;
    private Vector3 object_pos;
    private float angle;

    private float radius = 0.35f;
    private float additionalAngle;


    private SpriteRenderer tempRend;


    public float Angle {
        get { return angle; }
    }


    void Start ()
    {
        tempRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {


        mouse_pos = Input.mousePosition;
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x -= object_pos.x;
        mouse_pos.y -= object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        


        if (angle >= 67.5 && angle < 112.5)
        {
            // up
            animator.SetFloat("X_offset", 0);
            animator.SetFloat("Y_offset", 1);
            additionalAngle = -90;
        }
        else if (angle >= 112.5 && angle < 157.5)
        {
            // left up
            animator.SetFloat("X_offset", -0.5f);
            animator.SetFloat("Y_offset", 0.5f);
            additionalAngle = -135;
        }
        else if (angle >= 157.5 || angle < -157.5) //!!!!!
        {
            // left
            animator.SetFloat("X_offset", -1);
            animator.SetFloat("Y_offset", 0);
            additionalAngle = 195;
        }
        else if (angle >= -157.5 && angle < -112.5)
        {
            // left down
            animator.SetFloat("X_offset", -0.5f);
            animator.SetFloat("Y_offset", -0.5f);
            additionalAngle = 135;
        }

        else if (angle >= -112.5f && angle < -67.5)
        {
            // down
            animator.SetFloat("X_offset", 0);
            animator.SetFloat("Y_offset", -1);
            additionalAngle = 90;
        }
        else if (angle >= -67.5f && angle < -22.5)
        {
            // right down
            animator.SetFloat("X_offset", 0.5f);
            animator.SetFloat("Y_offset", -0.5f);
            additionalAngle = 45;
        }
        else if (angle >= -22.5f && angle < 22.5)
        {
            // right
            animator.SetFloat("X_offset", 1);
            animator.SetFloat("Y_offset", 0);
            additionalAngle = -5;
        }
        else
        {
            // right up
            animator.SetFloat("X_offset", 0.5f);
            animator.SetFloat("Y_offset", 0.5f);
            additionalAngle = -45;
        }


        transform.rotation = Quaternion.Euler(0, 0, angle + additionalAngle);

        mouse_pos.Normalize();
        mouse_pos *= radius;
        transform.position = new Vector3(owner.position.x + mouse_pos.x, owner.position.y + mouse_pos.y, (angle < 0 || angle > 180) ? 1 : -1);

        if (angle > 0 && angle < 270)
        {
            tempRend.sortingLayerName = "Background";
        }
        else
        {
            tempRend.sortingLayerName = "Foreground";
        }

      
    }
}
