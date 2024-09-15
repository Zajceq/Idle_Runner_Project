using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventInvokerCoroutine : MonoBehaviour
{
    [SerializeField] private float coroutineTimer = 0f; // Time to wait before invoking the event
    [SerializeField] private UnityEvent coroutineEvent; // Event to be invoked after the timer

    private WaitForSeconds _timer; // Cached WaitForSeconds object for optimization

    private void Start()
    {
        // Initialize the timer with the specified wait time
        _timer = new WaitForSeconds(coroutineTimer);
    }

    public void StartCouroutine()
    {
        // Start the coroutine when called
        StartCoroutine(EventCoroutine());
    }

    private IEnumerator EventCoroutine()
    {
        // Wait for the specified time, then invoke the event
        yield return _timer;
        coroutineEvent?.Invoke();
    }

    private void OnDestroy()
    {
        // Stop all coroutines if the object is destroyed to prevent potential issues
        StopAllCoroutines();
    }
}