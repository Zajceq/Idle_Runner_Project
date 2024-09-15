using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.Events;

public class CheckAtomIntValueChange : MonoBehaviour
{
    [SerializeField] private IntVariable variable; // Reference to an IntVariable from UnityAtoms

    [SerializeField] private UnityEvent onValueIncrease; // Event triggered when the value increases
    [SerializeField] private UnityEvent onValueDecrease; // Event triggered when the value decreases

    public void CheckValueChange()
    {
        // Check if the current value is greater than the old value
        if (variable.Value > variable.OldValue)
        {
            onValueIncrease?.Invoke(); // Invoke the event if the value increased
        }
        // Check if the current value is less than the old value
        else if (variable.Value < variable.OldValue)
        {
            onValueDecrease?.Invoke(); // Invoke the event if the value decreased
        }
    }
}