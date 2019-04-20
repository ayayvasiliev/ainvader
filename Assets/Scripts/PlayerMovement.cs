using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Animator animator;
    private Vector3 lastVector, movement;
    public int speed;

    private bool left = false;
    private bool right= false;
    private bool top = false;
    private bool bottom = false;

    private void Start()
    {
        lastVector.x = 0;
        lastVector.y = -1;
    }

    void OnGUI() {

        {
            Event e = Event.current;
            if (e.type
                == EventType.KeyDown)
            {
                switch (e.keyCode)
                {
                    case KeyCode.W:
                        top = true;
                        break;
                    case KeyCode.A:
                        left = true;
                        break;
                    case KeyCode.S:
                        bottom = true;
                        break;
                    case KeyCode.D:
                        right = true;
                        break;
                }
            }
            else if (e.type
                == EventType.KeyUp)
            {
                switch (e.keyCode)
                {
                    case KeyCode.W:
                        top = false;
                        break;
                    case KeyCode.A:
                        left = false;
                        break;
                    case KeyCode.S:
                        bottom = false;
                        break;
                    case KeyCode.D:
                        right = false;
                        break;
                }
            }
        }


    }

    private void Update()
    {
        movement = new Vector3((left ? -1 : 0) + (right ? 1 : 0), (top ? 1 : 0) + (bottom ? -1 : 0), 0);
        movement.Normalize();


        if (Input.anyKey != false)
        {
            lastVector.y = movement.y;
            lastVector.x = movement.x;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        if (movement.magnitude < 0.001)
        {
            animator.SetFloat("Horizontal", lastVector.x);
            animator.SetFloat("Vertical", lastVector.y);

        }

        transform.position = transform.position + movement * Time.deltaTime * speed;
    }
}
