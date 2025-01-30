using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    public void ActivateInputField(TMP_InputField inputField)
    {
        inputField.ActivateInputField(); 
        EventSystem.current.SetSelectedGameObject(inputField.gameObject, null); 
    }

    public void DeactivateInputField(TMP_InputField inputField)
    {
        inputField.DeactivateInputField();
        inputField.SetTextWithoutNotify("");
    }
}