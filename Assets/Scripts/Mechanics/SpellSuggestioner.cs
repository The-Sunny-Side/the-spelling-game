using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SpellSuggestioner : MonoBehaviour
{
    public GameObject SingleSuggestionPrefab;
    public TMP_InputField inputField;
    [SerializeField] private int _maxSuggestion = 5;

    private int _selectedIndex = -1;
    private List<GameObject> _suggestionPool = new();
    private bool _isNavigating = false;

    private void Start()
    {
        inputField = transform.parent.GetComponentInChildren<TMP_InputField>();
        inputField.onValueChanged.AddListener(UpdateDropdownSuggestions);
        inputField.onEndEdit.AddListener(ClearInput);

        InitializePool(); 
    }

    private void Update()
    {
        HandleKeyboardNavigation();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _maxSuggestion; i++)
        {
            var suggestionObject = Instantiate(SingleSuggestionPrefab, transform);
            suggestionObject.SetActive(false);
            _suggestionPool.Add(suggestionObject);
        }
    }

    private void HandleKeyboardNavigation()
    {
        if (_suggestionPool.Count(s => s.activeSelf) == 0) return; 

        if (Input.GetKeyDown(KeyCode.DownArrow) && _selectedIndex < _suggestionPool.Count(s => s.activeSelf) - 1 && !_isNavigating)
        {
            ChangeSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && _selectedIndex > 0 && !_isNavigating)
        {
            ChangeSelection(-1);
        }
    }

    private void ChangeSelection(int direction)
    {
        _isNavigating = true;

        if (_selectedIndex >= 0 && _selectedIndex < _suggestionPool.Count)
        {
            _suggestionPool[_selectedIndex].GetComponent<SingleSuggestionBehaviour>().IsSelected = false;
        }

        _selectedIndex = Mathf.Clamp(_selectedIndex + direction, 0, _suggestionPool.Count(s => s.activeSelf) - 1);

        if (_selectedIndex >= 0 && _selectedIndex < _suggestionPool.Count)
        {
            _suggestionPool[_selectedIndex].GetComponent<SingleSuggestionBehaviour>().IsSelected = true;
            UpdateInputFieldWithSelection();
        }

        _isNavigating = false;
    }

    private void UpdateInputFieldWithSelection()
    {
        if (_selectedIndex >= 0 && _selectedIndex < _suggestionPool.Count)
        {
            string selectedText = _suggestionPool[_selectedIndex].transform.Find("Text").GetComponent<TMP_Text>().text;
            inputField.SetTextWithoutNotify(selectedText);
        }
    }

    private void ClearInput(string inputText)
    {
        if (_selectedIndex >= 0 && _selectedIndex < _suggestionPool.Count)
        {
            UpdateInputFieldWithSelection();
        }
        ClearPreviousSuggestions();
    }

    private void UpdateDropdownSuggestions(string inputText)
    {
        ClearPreviousSuggestions();

        List<string> suggestions = ResourceManager.Instance.GetSuggestions(inputText);
        int maxToShow = Mathf.Min(_maxSuggestion, suggestions.Count);

        for (int i = 0; i < maxToShow; i++)
        {
            ActivateSuggestionItem(i, suggestions[i]);
        }

        if (maxToShow > 0)
        {
            _selectedIndex = 0;
            _suggestionPool[_selectedIndex].GetComponent<SingleSuggestionBehaviour>().IsSelected = true;
        }
    }

    private void ActivateSuggestionItem(int index, string suggestionText)
    {
        var suggestionObject = _suggestionPool[index];
        suggestionObject.SetActive(true);
        suggestionObject.GetComponentInChildren<TMP_Text>().text = suggestionText;
    }

    private void ClearPreviousSuggestions()
    {
        foreach (var suggestion in _suggestionPool)
        {
            suggestion.SetActive(false);
        }
        _selectedIndex = -1;
    }
    private void OnDestroy()
    {
        foreach (var suggestion in _suggestionPool)
        {
            if (suggestion != null)
            {
                Destroy(suggestion);
            }
        }
    }
}
