using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownSuggestionManager : MonoBehaviour
{
    public TMP_InputField inputField;       // Il campo di input da monitorare
    public TMP_Dropdown dropdown;          // Il menu a tendina

    private void Start()
    {
        inputField = transform.parent.GetComponentInChildren<TMP_InputField>();
        inputField.onValueChanged.AddListener(UpdateDropdownSuggestions);
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.gameObject.SetActive(false);
    }

    private void UpdateDropdownSuggestions(string inputText)
    {
        if (string.IsNullOrWhiteSpace(inputText))
        {
            dropdown.gameObject.SetActive(false);
            return;
        }

        List<string> suggestions = ResourceManager.Instance.GetSuggestions(inputText);

        if (suggestions.Count == 0)
        {
            dropdown.gameObject.SetActive(false);
            return;
        }

        dropdown.ClearOptions(); 
        dropdown.AddOptions(suggestions);

        dropdown.gameObject.SetActive(true);
    }
}