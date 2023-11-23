using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator PlayerAnimator;
    float velocity = 3.0f;
    public float acceleration = 0.1f;
    private float current_speed = 2;
    public AudioSource src;
    public AudioClip shooting;

    private float currentHorizontalValue;
    private float currentVerticalValue;
    [SerializeField] float valueSmoothing;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        float targetSpeedVertical = Input.GetAxis("Vertical");
        float targetSpeedHorizontal = Input.GetAxis("Horizontal");



        bool forward = Input.GetKey("w");
        bool back = Input.GetKey("s");
        bool left = Input.GetKey("a");
        bool right = Input.GetKey("d");
        bool attack = Input.GetKey("p");

        currentHorizontalValue = Mathf.Lerp(currentHorizontalValue, targetSpeedHorizontal, Time.deltaTime * valueSmoothing);
        currentVerticalValue = Mathf.Lerp(currentVerticalValue, targetSpeedVertical, Time.deltaTime * valueSmoothing);

        PlayerAnimator.SetFloat("Y", currentVerticalValue);
        PlayerAnimator.SetFloat("X", currentHorizontalValue);

        if (forward)
        {
            transform.position += current_speed * transform.forward * Time.deltaTime;
        }
        if (back)
        {
            transform.position -= current_speed * transform.forward * Time.deltaTime;
        }
        if (left)
        {
            transform.position -= current_speed * transform.right * Time.deltaTime;
        }
        if (right)
        {
            transform.position += current_speed * transform.right * Time.deltaTime;
        }
        if (attack)
        {
            PlayerAnimator.SetTrigger("Attacking");
            src.clip = shooting;
            src.PlayDelayed(0.6f);
        }

    }
}
