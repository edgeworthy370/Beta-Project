using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover2D : MonoBehaviour
{
    public float Step = 0.2f;
    public Animator animator;

    void Start ()
    {

	}
	
	void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.LeftArrow))

        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - Step, 0, 0);
            transform.localScale = new Vector3(-1, 1, 0);
            animator.SetFloat("Speed", 1);

        }

        else if (Input.GetKey(KeyCode.RightArrow))

        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + Step, 0, 0);
            transform.localScale = new Vector3(1, 1, 0);
            animator.SetFloat("Speed", 1);

        }
        
        else
        {
            animator.SetFloat("Speed", 0);
        }

	}
}

//transform.localScale allows sprite to turn around and face the direction it's moving
// Step = how fast the gameObject is moving
// Speed refers to the animation's parameters (if speed < 0.1, it is idle, otherwise it runs)
