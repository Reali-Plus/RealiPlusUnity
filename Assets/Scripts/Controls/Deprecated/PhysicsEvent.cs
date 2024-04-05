using UnityEngine;

public abstract class PhysicsEvent<T> : ScriptableObject
{
    public T CurrentValue { get; protected set; }

    public virtual void SetValue(T newValue)
    {
        CurrentValue = newValue;
    }
}
