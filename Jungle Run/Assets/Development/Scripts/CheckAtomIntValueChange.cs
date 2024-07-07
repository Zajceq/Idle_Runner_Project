using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.Events;

public class CheckAtomIntValueChange : MonoBehaviour
{
    [SerializeField] private IntVariable variable;

    [SerializeField] private UnityEvent onValueIncrease;
    [SerializeField] private UnityEvent onValueDecrease;

    public void CheckValueChange()
    {
        if (variable.Value > variable.OldValue)
        {
            onValueIncrease?.Invoke();
        }
        else if (variable.Value < variable.OldValue)
        {
            onValueDecrease?.Invoke();
        }
    }
}
