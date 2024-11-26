using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketZoneManager : MonoBehaviour
{
    [SerializeField] 
    private BasketZone[] basketZones;

    private void OnEnable()
    {
        BasketZone.OnBasketZoneChange += BasketZoneChange;
    }

    private void OnDisable()
    {
        BasketZone.OnBasketZoneChange -= BasketZoneChange;
    }

    private void BasketZoneChange(bool enteredZone)
    {
        if (enteredZone)
        {
            foreach (var basketZone in basketZones)
            {
                basketZone.MakeGrabZone();
            }
        }
        else // exited zone
        {
            foreach (var basketZone in basketZones)
            {
                basketZone.MakeReleaseZone();
            }
        }
    }

    // return true if the object is in a grab zone, false if in a release zone
    public bool IsObjectInGrabZone(Grabbable grabbable) 
    {
        foreach (var basketZone in basketZones)
        {
            if (basketZone.IsGrabbableInZone(grabbable))
            {
                return basketZone.IsGrabZone();
            }
        }

        return false;
    }
}
