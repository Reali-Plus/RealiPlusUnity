using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        print("COLLISIONS with: "+ gameObject.name);

        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    print("COLLISIONS:"+ contact);
        //}
    }
}
