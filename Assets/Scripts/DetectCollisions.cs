using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        print("COLLISIONS with: "+ gameObject.name);
        Debug.DrawRay(collision.contacts[0].point, Vector3.up, Color.blue, 10.0f);

        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    print("COLLISIONS:"+ contact);
        //}
    }
}
