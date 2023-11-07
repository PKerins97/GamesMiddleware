using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator PlayerAnimator;
    float velocity = 3.0f;
    public float acceleration = 0.1f;
    private float current_speed = 2;
    private float BACKWARDS_SPEED = 2, RUNNING_SPEED = 5;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimator.SetFloat("Y", Input.GetAxis("Vertical"));
        PlayerAnimator.SetFloat("X", Input.GetAxis("Horizontal"));

        bool forward = Input.GetKey("w");
        bool back = Input.GetKey("s");
        bool left = Input.GetKey("a");
        bool right = Input.GetKey("d");
        if (forward)
        {
            transform.position += current_speed * transform.forward * Time.deltaTime;
        }
        if (back)
        {
            transform.position -= BACKWARDS_SPEED * transform.forward * Time.deltaTime;
        }
        if (left)
        {
            transform.position -= BACKWARDS_SPEED * transform.right * Time.deltaTime;
        }
        if (right)
        {
            transform.position += BACKWARDS_SPEED * transform.right * Time.deltaTime;
        }


    }
}
