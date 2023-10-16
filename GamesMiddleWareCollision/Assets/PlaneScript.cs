using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour, iCollidable
{
 

    public bool isColliding(iCollidable otherObject)
    {
        //if(otherObject is Sphere_Physics)
        // {
        //     return otherObject.isColliding(this);
        // }
        // return false;
        return false;
    }
    public void resolvedVelocityForCollisonWith(iCollidable otherObject)
    {
        //if (otherObject is Sphere_Physics)
        //{
        //    otherObject.resolvedVelocityForCollisonWith(this);
        //}
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}