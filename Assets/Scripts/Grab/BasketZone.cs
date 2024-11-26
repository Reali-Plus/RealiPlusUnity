using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketZone : MonoBehaviour
{
    // private bool canRelease = true;

    struct GrabbableInZone
    {
        public Grabbable grabbable;
        public Collider collider;
        public bool isEntering;
        public bool isInside;

        public GrabbableInZone(Grabbable grabbable, Collider collider, bool isEntering)
        {
            this.grabbable = grabbable;
            this.isEntering = isEntering;
            this.isInside = false;
            this.collider = collider;
        }
        public GrabbableInZone(Grabbable grabbable, Collider collider, bool isEntering, bool isInside)
        {
            this.grabbable = grabbable;
            this.isEntering = isEntering;
            this.isInside = isInside;
            this.collider = collider;
        }
    }

    [SerializeField]
    private bool isGrabZone = true; // if not it is a release zone

    private bool isEnteringZone = false; // collided with the zone but it is not completely in it
    private bool enteredZone = false; // completely in the zone
    private Collider colliderEntering;

    private BoxCollider boxCollider;

    [SerializeField]
    private Dictionary<Grabbable, GrabbableInZone> grabbablesInZone = new Dictionary<Grabbable, GrabbableInZone>();
    public static event Action<bool> OnBasketZoneChange; // true is enter, false is exit


    public bool IsGrabbableInZone(Grabbable grabbable)
    {
        return grabbablesInZone.ContainsKey(grabbable) && grabbablesInZone[grabbable].isInside;
    }

    public bool IsReleaseZone() => !isGrabZone;
    public bool IsGrabZone() => isGrabZone;

    public void MakeGrabZone()
    {
        Debug.Log("Makes grab zone");
        isGrabZone = true;
    }

    public void MakeReleaseZone()
    {
        Debug.Log("Makes release zone");
        isGrabZone = false;
    }


    /*public void ToogleZone()
    {
        isGrabZone = !isGrabZone;
        otherZone.ToogleZone();
    }*/

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        /*if (isEnteringZone)
        {*/
            // check if the object is completely in the zone
            // check each bounds of the collider

            /*List<Vector3> bounds = new List<Vector3>();
            bounds.Add(new Vector3(colliderEntering.bounds.min.x, colliderEntering.bounds.min.y, colliderEntering.bounds.min.z));
            bounds.Add(new Vector3(colliderEntering.bounds.min.x, colliderEntering.bounds.min.y, colliderEntering.bounds.max.z));
            bounds.Add(new Vector3(colliderEntering.bounds.min.x, colliderEntering.bounds.max.y, colliderEntering.bounds.min.z));
            bounds.Add(new Vector3(colliderEntering.bounds.min.x, colliderEntering.bounds.max.y, colliderEntering.bounds.max.z));
            bounds.Add(new Vector3(colliderEntering.bounds.max.x, colliderEntering.bounds.min.y, colliderEntering.bounds.min.z));
            bounds.Add(new Vector3(colliderEntering.bounds.max.x, colliderEntering.bounds.min.y, colliderEntering.bounds.max.z));
            bounds.Add(new Vector3(colliderEntering.bounds.max.x, colliderEntering.bounds.max.y, colliderEntering.bounds.min.z));
            bounds.Add(new Vector3(colliderEntering.bounds.max.x, colliderEntering.bounds.max.y, colliderEntering.bounds.max.z));

            bool isInside = true;
            for (int i = 0; i < bounds.Count; ++i)
            {
                if (!IsInsideCollider(bounds[i]))
                {
                    isInside = false;
                    return;
                }
            }*/

        var keys = new List<Grabbable>(grabbablesInZone.Keys);
        for (int i = 0; i < keys.Count; ++i)
        {
            var grabbable = keys[i];
            var zone = grabbablesInZone[keys[i]];

            if (zone.isEntering)
            {
                if (CheckIfColliderIsInside(zone.collider))
                {

                    Debug.Log("Inside zone");
                    AddOrUpdateGrabbableInZone(grabbable, zone.collider, false, true);

                    if (IsReleaseZone())
                    {
                        Debug.Log("Releases from hand");
                        grabbable.Release();
                        OnBasketZoneChange?.Invoke(true); // enter
                    }
                }
            }
        }

            /*foreach (var grabbable in grabbablesInZone)
            {
                if(CheckIfColliderIsInside(grabbable.Value.collider))
                {
                    Debug.Log("Inside zone");
                    grabbablesInZone[grabbable.Key] = new GrabbableInZone(grabbable.Key, grabbable.Value.collider, false, true);
                    if (IsReleaseZone())
                    {
                        Debug.Log("Releases from hand");
                        grabbable.Key.Release();
                        OnBasketZoneChange?.Invoke(true); // enter
                    }
                }
            }*/

            /*if (isInside)
            {
                isEnteringZone = false;
                enteredZone = true;
                Debug.Log("Entered zone");
                Grabbable grabbable = colliderEntering.GetComponent<Grabbable>();
                grabbablesInZone.Add(grabbable);
                if (IsReleaseZone())
                {
                    Debug.Log("Releases from hand");
                    grabbable.Release();
                    OnBasketZoneChange?.Invoke(true); // enter
                }
            }*/
            /*if (boxCollider.bounds.Contains(colliderEntering.bounds.center))
            {
                isEnteringZone = false;
                enteredZone = true;
                Grabbable grabbable = colliderEntering.GetComponent<Grabbable>();
                grabbablesInZone.Add(grabbable);
                if (IsReleaseZone())
                {
                    Debug.Log("Releases from basket zone");
                    grabbable.Release();
                    OnBasketZoneChange?.Invoke(true); // enter
                }
            }*/
        /*}*/
    }

    private void AddOrUpdateGrabbableInZone(Grabbable grabbable, Collider collider, bool isEntering, bool isInside)
    {
        if (grabbablesInZone.ContainsKey(grabbable))
            grabbablesInZone[grabbable] = new GrabbableInZone(grabbable, collider, isEntering, isInside);
        else
            grabbablesInZone.Add(grabbable, new GrabbableInZone(grabbable, collider, isEntering, isInside));
    }

    private bool CheckIfColliderIsInside(Collider collider)
    {
        List<Vector3> bounds = new List<Vector3>
        {
            new Vector3(collider.bounds.min.x, collider.bounds.min.y, collider.bounds.min.z),
            new Vector3(collider.bounds.min.x, collider.bounds.min.y, collider.bounds.max.z),
            new Vector3(collider.bounds.min.x, collider.bounds.max.y, collider.bounds.min.z),
            new Vector3(collider.bounds.min.x, collider.bounds.max.y, collider.bounds.max.z),
            new Vector3(collider.bounds.max.x, collider.bounds.min.y, collider.bounds.min.z),
            new Vector3(collider.bounds.max.x, collider.bounds.min.y, collider.bounds.max.z),
            new Vector3(collider.bounds.max.x, collider.bounds.max.y, collider.bounds.min.z),
            new Vector3(collider.bounds.max.x, collider.bounds.max.y, collider.bounds.max.z)
        };

        for (int i = 0; i < bounds.Count; ++i)
        {
            if (!IsInsideCollider(bounds[i]))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsInsideCollider(Vector3 position)
    {
        return boxCollider.bounds.Contains(position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Grabbable grabbable))
        {
            //isEnteringZone = true;
            //colliderEntering = other;

            AddOrUpdateGrabbableInZone(grabbable, other, true, false);

            /*if (!grabbablesInZone.ContainsKey(grabbable))
                grabbablesInZone.Add(grabbable, new GrabbableInZone(grabbable, other, true));
            else
                grabbablesInZone[grabbable] = new GrabbableInZone(grabbable, other, true);
*/
            Debug.Log("entering ..........");
            /*grabbablesInZone.Add(grabbable);
            if (IsReleaseZone())
            {
                Debug.Log("Releases from basket zone");
                grabbable.Release();
                OnBasketZoneChange?.Invoke(true); // enter
            }*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isEnteringZone = false;
        if (other.TryGetComponent(out Grabbable grabbable))
        {
            grabbablesInZone.Remove(grabbable);
            OnBasketZoneChange?.Invoke(false); // exit
        }
    }
}
