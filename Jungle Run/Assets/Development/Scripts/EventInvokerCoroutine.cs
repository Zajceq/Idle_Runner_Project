using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventInvokerCoroutine : MonoBehaviour
{
    [SerializeField] private float coroutineTimer = 0f;
    [SerializeField] private UnityEvent coroutineEvent;

    private WaitForSeconds _timer;

    private void Start()
    {
        _timer = new WaitForSeconds(coroutineTimer);
    }

    public void StartCouroutine()
    {
        StartCoroutine(EventCoroutine());
    }

    private IEnumerator EventCoroutine()
    {
        yield return _timer;
        coroutineEvent?.Invoke();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
