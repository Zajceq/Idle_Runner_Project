using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorParameterSetter : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetBoolTrue(string name)
    {
        _animator.SetBool(name, true);
    }

    public void SetBoolFalse(string name)
    {
        _animator.SetBool(name, false);
    }

    public void SetTriggerParameter(string name)
    {
        _animator.SetTrigger(name);
    }
}
