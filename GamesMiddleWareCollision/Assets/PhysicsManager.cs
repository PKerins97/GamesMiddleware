using System.Collections;
using System.Collections.Generic;
using System.Text;
//using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;

public class PhysicsManager : MonoBehaviour
{
    //public GameObject S1;
    //public GameObject S2;
    //public GameObject Plane;
    //List<Sphere_Physics> spheres;
    //List<PlaneScript> planes;
    //private Sphere_Physics ball_class;
    //private Sphere_Physics ball_class2;
    //private PlaneScript plane_class;
    List<iCollidable> collidableObjects;
    //Vector3 smallV;

    //Vector3 norm;

    //Vector3 V;

    // Start is called before the first frame update
    void Start()
    {
       
        collidableObjects = FindObjectsOfType<MonoBehaviour>().OfType<iCollidable>().ToList();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < collidableObjects.Count - 1; i++)
        {
            
                for (int j = i+1; j < collidableObjects.Count; j++)
                {
                if (collidableObjects[i].isColliding(collidableObjects[j]))
                    collidableObjects[i].resolvedVelocityForCollisonWith(collidableObjects[j]);
                   
                       
                    
                }

            
        }
    }

    public static Vector3 getParallel(Vector3 smallV, Vector3 norm)
    {
        Vector3 parallel = Vector3.Dot(smallV, norm) * norm;

        return parallel;

    }
    public static Vector3 getPerpendicular(Vector3 smallV, Vector3 norm)
    {
        Vector3 perpendicular = smallV - getParallel(smallV, norm);
        return perpendicular;
    }

}

