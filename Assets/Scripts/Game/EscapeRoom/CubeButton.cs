using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;
    [SerializeField] float resetDelay =.25f;
    [SerializeField] SimonManager simonManager;
    //[SerializeField] AudioSource audioSource;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        ResetButton();
    }

    private void OnMouseDown()
    {
        if (!simonManager.isSequencePlaying)
        {
            //audioSource.Play();
            Highlight();
            simonManager.CheckSequence(this);  // Signale au SimonManager quel cube a �t� cliqu�
        }
    }

    public void Highlight()
    {
        meshRenderer.material.color = highlightColor;
        Invoke("ResetButton", resetDelay);
    }

    void ResetButton()
    {
        meshRenderer.material.color = defaultColor;
    }
}
