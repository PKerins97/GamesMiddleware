using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_Physics : MonoBehaviour, iCollidable
{


    //public float assignRadius;

    Vector3 acceleration;
    [SerializeField]
    internal Vector3 velocity;
    [SerializeField] internal float mass = 1;
    public Vector3 previous_Position;
    internal float Radius { get { return transform.localScale.x / 2f; } set { transform.localScale = 2 * value * Vector3.one; } }
    [SerializeField] private float coeffecientOfRestitution = 0.75f;
    [SerializeField] internal float assignRadius;

    // Start is called before the first frame update
    void Start()
    {
        acceleration = 9.8f * Vector3.down;
        Radius = assignRadius;
    }

    // Update is called once per frame
    void Update()
    {
        previous_Position = transform.position;

        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        if (transform.position.y <= 0.5)
        {
            //velocity = -velocity;
        }
    }

    public bool isColliding(iCollidable otherObject)
    {
        if (otherObject is Sphere_Physics)
        {
            Sphere_Physics otherSphere = otherObject as Sphere_Physics;
            float distance = Vector3.Distance(transform.position,
              otherSphere.transform.position);
            return distance < Radius + otherSphere.Radius;
        }
        else if (otherObject is PlaneScript)
        {
            PlaneScript plane = otherObject as PlaneScript;
            Vector3 sphereToPlane = transform.position - plane.transform.position;
            Vector3 normal = plane.transform.up;

            float distance = PhysicsManager.getParallel(sphereToPlane, normal).magnitude;

            return distance < Radius;
        }
        return false;
    }

    public void resolvedVelocityForCollisonWith(iCollidable otherObject)
    {
        if (otherObject is Sphere_Physics)
        {
            Vector3 toiPosition;
            Vector3 toiOtherObjPostion;
            Vector3 toiVelocity;
            Vector3 toiOtherObjVelocity;
            Vector3 sphereVelocity;
            Vector3 spherePosition;
            Sphere_Physics otherSphere = (Sphere_Physics)(otherObject);

            float d0 = (Vector3.Distance(previous_Position, otherSphere.previous_Position)) - Radius - otherSphere.Radius;
            float d1 = (Vector3.Distance(transform.position, otherSphere.transform.position)) - Radius - otherSphere.Radius;

            float time_Of_Impact = d1 * (Time.deltaTime / (d1 - d0));

            toiPosition = transform.position - velocity * time_Of_Impact;
            toiOtherObjPostion = otherSphere.transform.position - otherSphere.velocity * time_Of_Impact;

            toiVelocity = velocity - acceleration * time_Of_Impact;
            toiOtherObjVelocity = otherSphere.velocity - otherSphere.acceleration * time_Of_Impact;

            Vector3 normal = (toiPosition - toiOtherObjPostion).normalized;

            Vector3 u1 = PhysicsManager.getParallel(toiVelocity, normal);
            Vector3 u2 = PhysicsManager.getParallel(toiOtherObjVelocity, normal);

            Vector3 s1 = PhysicsManager.getPerpendicular(toiVelocity, normal);
            Vector3 s2 = PhysicsManager.getPerpendicular(toiOtherObjVelocity, normal);

            float M1 = mass;
            float M2 = otherSphere.mass;


            Vector3 v1 = ((M1 - M2) / (M1 + M2)) * u1 + ((2 * M2) / (M1 + M2)) * u2;
            Vector3 v2 = ((2 * M1) / (M1 + M2)) * u1 + ((M2 - M1) / (M1 + M2)) * u2;

            v1 *= coeffecientOfRestitution;
            v2 *= otherSphere.coeffecientOfRestitution;

            v1 += acceleration * time_Of_Impact;
            v2 += otherSphere.acceleration * time_Of_Impact;

            velocity = v1 + s1;
            sphereVelocity = v2 + s2;

            transform.position = toiPosition + velocity * time_Of_Impact;
            spherePosition = toiOtherObjPostion + sphereVelocity * time_Of_Impact;


            otherSphere.velocity = sphereVelocity;

            otherSphere.transform.position = spherePosition;
        }
        else if (otherObject is PlaneScript)
        {
            PlaneScript plane = otherObject as PlaneScript;
            Vector3 normal = plane.transform.up;
            Vector3 vTime = velocity * Time.deltaTime;

            transform.position += PhysicsManager.getPerpendicular(vTime, normal) - PhysicsManager.getParallel(vTime, normal);

            velocity = PhysicsManager.getPerpendicular(velocity, normal) - (coeffecientOfRestitution * PhysicsManager.getParallel(velocity, normal));
            //Vector3 parrellelV = PhysicsManager.getParallel(this.velocity, normal);
            //Vector3 perpendicularV = PhysicsManager.getPerpendicular(this.velocity, normal);
            //velocity = perpendicularV - parrellelV;
        }
    }
}