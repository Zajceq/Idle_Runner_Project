using UnityEngine;
using UnityAtoms.BaseAtoms;
using TMPro;

public class UpdateUIFromAtomVariable : MonoBehaviour
{
    [SerializeField] private IntVariable variable; // Reference to the IntVariable from UnityAtoms
    [SerializeField] private TextMeshProUGUI Text; // UI Text component (TextMeshPro) to display the variable value

    private void Start()
    {
        // Set the initial text to the current value of the variable
        Text.text = variable.Value.ToString();
    }

    private void OnEnable()
    {
        // Register a listener that updates the UI when the variable's value changes
        variable.Changed.Register(UpdateUIText);
    }

    private void OnDisable()
    {
        // Unregister the listener when the object is disabled to prevent memory leaks
        variable.Changed.Unregister(UpdateUIText);
    }

    private void UpdateUIText()
    {
        // Update the UI text with the new value of the variable
        Text.text = variable.Value.ToString();
    }
}