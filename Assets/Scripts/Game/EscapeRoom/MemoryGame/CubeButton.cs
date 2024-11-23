using System.Collections;
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
    
    //FOR TESTING WITH MOUSE
    private void OnMouseDown()
    {
        Interact(transform.position);
    }

    public override void Interact(Vector3 position)
    {
        if (!CanInteract || sequenceManager.isSequencePlaying) return;

        Highlight();

        if (memoryManager.IsStartButton(this))
        {
            memoryManager.PlaySequence();
        }
        else
        {
            CanInteract = false;
            memoryManager.CheckSequence(this);
            StartCoroutine(ReactivateInteraction());
        }
    }

    IEnumerator ReactivateInteraction()
    {
        yield return new WaitForSeconds(0.5f);
        CanInteract = true;
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
