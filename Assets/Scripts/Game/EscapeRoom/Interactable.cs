using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [field: SerializeField] public bool IsInteractable { get; protected set; } = true;

    public abstract void Interact(Vector3 position);
}
