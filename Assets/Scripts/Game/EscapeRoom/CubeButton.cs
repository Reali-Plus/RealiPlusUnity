using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color highlightColor;
    [SerializeField] private float resetDelay =.25f;
    [SerializeField] private AudioSource audioSource;

    private MeshRenderer meshRenderer;
    private Color failureColor = Color.red;

    protected MemoryGameManager memoryManager;
    protected SequenceManager sequenceManager;

    private void Start()
    {
        memoryManager = GameObject.FindGameObjectWithTag("MemoryManager").GetComponent<MemoryGameManager>();
        sequenceManager = GameObject.FindGameObjectWithTag("MemoryManager").GetComponent<SequenceManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
        ResetColor();
    }

    private void OnMouseDown()
    {
        if (!sequenceManager.isSequencePlaying)
        {
            Highlight();
            if (gameObject.CompareTag("StartButton"))
            {
                memoryManager.PlaySequence();
            }
            else if (gameObject.CompareTag("ColorButton"))
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
