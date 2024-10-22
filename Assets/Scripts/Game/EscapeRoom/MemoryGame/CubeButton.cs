using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : Interactable
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color highlightColor;
    [SerializeField] private float resetDelay =.25f;
    [SerializeField] private AudioSource audioSource;

    private Color failureColor = Color.red;
    private MeshRenderer meshRenderer;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (CanInteractable && collision.collider.CompareTag("Ball") && !sequenceManager.isSequencePlaying)
        {
            Interact(collision.transform.position);
        }
    }

    public override void Interact(Vector3 position)
    {
        Highlight();

        if (memoryManager.IsStartButton(this))
        {
            memoryManager.PlaySequence();
        }
        else
        {
            memoryManager.CheckSequence(this);
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
