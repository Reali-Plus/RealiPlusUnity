using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float fallSpeed = 2f;
    public Material defaultMaterial;
    public Material hitMaterial;
    public Material failMaterial;

    private string noteText;
    private Renderer noteRenderer;
    private bool hasCollided = false;
    void Start()
    {
        noteRenderer = GetComponent<Renderer>();
        noteRenderer.material = defaultMaterial;
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    public void setNoteText(string text)
    {
        noteText = text;
        GetComponentInChildren<TextMesh>().text = noteText;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitZone"))
        {
            hasCollided = true;
            noteRenderer.material = hitMaterial;
            CheckKeyPress();
        }
    }

    void CheckKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HitZone"))
        {
            noteRenderer.material = defaultMaterial;
            hasCollided = false;
        }
    }
}
