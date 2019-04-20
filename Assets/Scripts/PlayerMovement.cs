using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Animator animator;
    private Vector3 lastVector;


    private void Start()
    {
        lastVector.x = 0;
        lastVector.y = -1;
    }

    // Update is called once per frame
    void Update () {

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        if (Input.anyKey != false)
        {
            lastVector.x = movement.x;
            lastVector.y = movement.y;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        if (movement.magnitude < 0.001)
        {
            animator.SetFloat("Horizontal", lastVector.x);
            animator.SetFloat("Vertical", lastVector.y);

            Debug.Log(lastVector.x + " " + lastVector.y + " | " + animator.GetFloat("Horizontal") + " " + animator.GetFloat("Vertical"));
        }

        

        transform.position = transform.position + movement * Time.deltaTime * 3;   

    }
}
