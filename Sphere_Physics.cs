using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_Physics : MonoBehaviour
{
    public Vector3 velocity;
    Vector3 acceleration;
    // Start is called before the first frame update
    void Start()
    {
        acceleration = 9.8f * Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        velocity += acceleration * Time.deltaTime;

        transform.position += velocity*Time.deltaTime;

        if(transform.position.y <= 0.5)
        {
            velocity = -velocity;
        }
    }
}
