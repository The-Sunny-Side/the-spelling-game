using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public GameObject UIInputWindow;

    private bool _isSpelling=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReadyToSpell();
    }

    private void ReadyToSpell()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var inputField = UIInputWindow.GetComponentInChildren<TMP_InputField>();
            Debug.Log(inputField);

            if (inputField != null)
            {
                if (!_isSpelling)
                {

                    UIManager.Instance.ActivateInputField(inputField);
                    _isSpelling = true;

                }
                else
                {

                    UIManager.Instance.DeactivateInputField(inputField);
                    _isSpelling = false;
                }
            }
            else
            {
                Debug.LogError("InputField not found in UIInputWindow");
            }
        }
    }
}
