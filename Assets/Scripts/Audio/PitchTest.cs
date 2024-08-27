using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PitchTest : MonoBehaviour
{
    [SerializeField] 
    private AudioSource[] notes;

    [SerializeField] 
    private InputAction playAction;

    [SerializeField] 
    private float delay = 0.5f;

    [SerializeField] 
    private float noteDuration = 1f;

    private void OnEnable()
    {
        playAction.performed += PlayActionCallback;
        playAction.Enable();
    }

    private void OnDisable()
    {
        playAction.Disable();
        playAction.performed -= PlayActionCallback;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();        
    }

    private void PlayActionCallback(InputAction.CallbackContext c)
    {
        StartCoroutine(PlayNotesCoroutine());
    }

    private IEnumerator PlayNotesCoroutine()
    {
        YieldInstruction wait = new WaitForSeconds(delay);
        foreach (var note in notes)
        {
            note.Play();
            StartCoroutine(FadeNote(note));
            yield return wait;
        }
    }

    private IEnumerator FadeNote(AudioSource source)
    {
        float defaultVolume = source.volume;

        while (source.volume > 0)
        {
            yield return null;
            source.volume -= Time.deltaTime / noteDuration;
        }

        source.Stop();
        source.volume = defaultVolume;
    }
}
