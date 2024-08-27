using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ArticulationBody))]
public class PianoKey : MonoBehaviour
{
    [SerializeField] 
    private AudioSource audioSource;

    [SerializeField]
    private float triggerAngleThreshold = 2f;

    [SerializeField] 
    private float velocityMultiplier = 10f;

    [SerializeField, Min(0f)] 
    private float decay = 2f;

    [SerializeField, Min(0f)] 
    private float holdDecay = 0.2f;

    private bool triggered = false;
    private int contactCounter = 0;
    private Coroutine fadeCoroutine;
    private ArticulationBody keyBody;

    private void Awake()
    {
        keyBody = GetComponent<ArticulationBody>();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnCollisionEnter()
    {
        contactCounter++;
    }

    private void OnCollisionStay()
    {
        if (keyBody.transform.eulerAngles.x > triggerAngleThreshold)
        {
            if (triggered) 
            { 
                return;
            } 

            triggered = true;
            PlayNote(Mathf.Abs(keyBody.velocity.y) * velocityMultiplier);
        }
        else
        {
            triggered = false;
        }
    }

    private void OnCollisionExit()
    {
        contactCounter--;

        if (contactCounter == 0)
        {
            triggered = false;
        }
    }

    private void PlayNote(float volume)
    {
        audioSource.volume = volume;
        audioSource.Play();

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeNote());
    }

    private IEnumerator FadeNote()
    {
        while (audioSource.volume > 0)
        {
            yield return null;
            audioSource.volume -= Time.deltaTime * (triggered ? holdDecay : decay);
        }

        audioSource.Stop();
        fadeCoroutine = null;
    }
}
