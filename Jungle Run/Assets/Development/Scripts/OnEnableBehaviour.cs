using UnityEngine;
using UnityEngine.Events;

public class OnEnableBehaviour : MonoBehaviour
{
    public UnityEvent onEnableEvent;

    private void OnEnable()
    {
        onEnableEvent?.Invoke();
    }
}
