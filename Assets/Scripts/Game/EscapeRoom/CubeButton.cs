using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField]Color defaultColor;
    [SerializeField]Color highlightColor;
    [SerializeField]float resetDelay =.25f;
    private MeshRenderer meshRenderer;
    SimonManager simonManager;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        simonManager = FindObjectOfType<SimonManager>();
        ResetButton();
    }

    private void OnMouseDown()
    {
        GetComponent<MeshRenderer>().material.color = highlightColor;
        simonManager.CheckSequence(this);  // Signale au SimonManager quel cube a été cliqué
        Invoke("ResetButton", resetDelay);
    }

    public void Highlight()
    {
        GetComponent<MeshRenderer>().material.color = highlightColor;
        Invoke("ResetButton", resetDelay);
    }

    void ResetButton()
    {
        GetComponent<MeshRenderer>().material.color = defaultColor;
    }

}
