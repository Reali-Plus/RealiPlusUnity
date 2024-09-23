using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField]Color defaultColor;
    [SerializeField]Color highlightColor;
    [SerializeField]float resetDelay =.25f;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ResetButton();
    }

    private void OnMouseDown()
    {
        GetComponent<MeshRenderer>().material.color = highlightColor;
        Invoke("ResetButton", resetDelay);
    }

    void ResetButton()
    {
        GetComponent<MeshRenderer>().material.color = defaultColor;
    }

}