using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color highlightColor;
    [SerializeField] private float resetDelay =.25f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool isStartButton = false;
    [SerializeField] private bool isColorButton = false;

    private MeshRenderer meshRenderer;
    private Color failureColor = Color.red;

    private MemoryGameManager memoryManager;
    private SequenceManager sequenceManager;

    private void Start()
    {
        memoryManager = GameObject.FindGameObjectWithTag("MemoryManager").GetComponent<MemoryGameManager>();
        sequenceManager = GameObject.FindGameObjectWithTag("MemoryManager").GetComponent<SequenceManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
        ResetColor();
    }

    //TODO: change with hand model
    private void OnMouseDown()
    {
        if (!sequenceManager.isSequencePlaying)
        {
            Highlight();
            if (isStartButton)
            {
                memoryManager.PlaySequence();
            }
            else if (isColorButton)
            {
                memoryManager.CheckSequence(this);
            }
        }
    }

    public void Highlight()
    {
        audioSource.Play();
        meshRenderer.material.color = highlightColor;
        Invoke("ResetColor", resetDelay);
    }

    private void ResetColor()
    {
        meshRenderer.material.color = defaultColor;
    }

    public void ShowFailureColor()
    {
        meshRenderer.material.color = failureColor;
        Invoke("ResetColor", resetDelay);
    }

}
