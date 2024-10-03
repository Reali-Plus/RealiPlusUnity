using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;
    [SerializeField] float resetDelay =.25f;
    [SerializeField] SimonManager simonManager;
    [SerializeField] SequenceManager sequenceManager;
    [SerializeField] AudioSource audioSource;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        ResetButton();
    }

    private void OnMouseDown()
    {
        if (!sequenceManager.isSequencePlaying)
        {
            Highlight();
            simonManager.CheckSequence(this);  // Signale au SimonManager quel cube a été cliqué
        }
    }

    public void Highlight()
    {
        audioSource.Play();
        meshRenderer.material.color = highlightColor;
        Invoke("ResetButton", resetDelay);
    }

    public void ResetButton()
    {
        meshRenderer.material.color = defaultColor;
    }
}
