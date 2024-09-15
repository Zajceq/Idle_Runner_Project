using UnityEngine;
using UnityEngine.Events;

public class OnEnableBehaviour : MonoBehaviour
{
    public UnityEvent onEnableEvent; // Event triggered when the object is enabled

    private void OnEnable()
    {
        // Invoke the event when the object is enabled
        onEnableEvent?.Invoke();
    }
}