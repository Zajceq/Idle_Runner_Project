using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorParameterSetter : MonoBehaviour
{
    private Animator _animator; // Reference to the Animator component

    private void Awake()
    {
        _animator = GetComponent<Animator>(); // Get Animator component on Awake
    }

    public void SetBoolTrue(string name)
    {
        _animator.SetBool(name, true); // Set Animator bool parameter to true
    }

    public void SetBoolFalse(string name)
    {
        _animator.SetBool(name, false); // Set Animator bool parameter to false
    }

    public void SetTriggerParameter(string name)
    {
        _animator.SetTrigger(name); // Activate Animator trigger parameter
    }
}