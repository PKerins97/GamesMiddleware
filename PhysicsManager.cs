using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PhysicsManager : MonoBehaviour
{
    public GameObject S1;
    public GameObject S2;
    public GameObject Plane;

    private Sphere_Physics ball_class;
    private PlaneScript plane_class;

    Vector3 smallV;

    Vector3 norm;

    Vector3 V;

    // Start is called before the first frame update
    void Start()
    {
        ball_class = S1.GetComponent<Sphere_Physics>();
        ball_class = S2.GetComponent<Sphere_Physics>();
        plane_class = FindAnyObjectByType<PlaneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        norm = plane_class.transform.up;
        smallV = plane_class.transform.position - ball_class.transform.position;
        //position += getPerpendicular(smallV, norm) - getParallel(smallV, norm);

        Vector3 bigV = getParallel(smallV, norm);

        if (bigV.magnitude < 0.5)
        {
            ball_class.velocity = -ball_class.velocity;
        }

       
    }

    Vector3 getParallel(Vector3 smallV, Vector3 norm)
    {
        Vector3 parallel = Vector3.Dot(smallV, norm) * norm;

        return parallel;

    }
    Vector3 getPerpendicular(Vector3 smallV, Vector3 norm)
    {
        Vector3 perpendicular = smallV - getParallel(smallV, norm);
        return perpendicular;
    }
}


