using UnityEngine;
using UnityAtoms.BaseAtoms;
using TMPro;

public class UpdateUIFromAtomVariable : MonoBehaviour
{
    [SerializeField] private IntVariable variable;
    [SerializeField] private TextMeshProUGUI Text;

    private void Start()
    {
        Text.text = variable.Value.ToString();
    }

    private void OnEnable()
    {
        variable.Changed.Register(UpdateUIText);
    }

    private void OnDisable()
    {
        variable.Changed.Unregister(UpdateUIText);
    }

    private void UpdateUIText()
    {
        Text.text = variable.Value.ToString();
    }
}
