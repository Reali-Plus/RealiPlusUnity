using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;
    [SerializeField] float resetDelay =.25f;
    [SerializeField] protected MemoryGameManager memoryManager;
    [SerializeField] protected SequenceManager sequenceManager;
    [SerializeField] AudioSource audioSource;
    private MeshRenderer meshRenderer;
    private Color failureColor = Color.red;

    private void Start()
    {
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

    public void ResetColor()
    {
        meshRenderer.material.color = defaultColor;
    }

    public void ShowFailureColor()
    {
        meshRenderer.material.color = failureColor;
        Invoke("ResetColor", resetDelay);
    }

}
