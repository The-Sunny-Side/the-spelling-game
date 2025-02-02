using UnityEngine;

public class SingleSuggestionBehaviour : MonoBehaviour
{
    public bool IsSelected;
    // Update is called once per frame
    void Update()
    {
        var background = transform.Find("Background");

        if (IsSelected)
        {
            background.gameObject.SetActive(true);
        }
        else
        {
            background.gameObject.SetActive(false);
        }
    }
}
