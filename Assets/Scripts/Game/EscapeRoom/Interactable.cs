using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [field: SerializeField] public bool CanInteract { get; protected set; } = true;

    public abstract void Interact(Vector3 position);
}
