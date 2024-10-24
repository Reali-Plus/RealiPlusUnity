using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [field: SerializeField] public bool CanInteractable { get; protected set; } = true;

    public abstract void Interact(Vector3 position);
}
