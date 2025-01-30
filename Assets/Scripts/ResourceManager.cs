using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public string resourceFolder = ""; 
    public List<string> resourceNames;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadResourceNames();
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void LoadResourceNames()
    {
        resourceNames = new List<string>();

        var resources = Resources.LoadAll<GameObject>(resourceFolder);
        foreach (var resource in resources)
        {

            resourceNames.Add(resource.name.ToLower()); 
        }

        Debug.Log($"Loaded {resourceNames.Count} resources from folder {resourceFolder}");
    }

    public List<string> GetSuggestions(string input)
    {
        input = input.ToLower();
        return resourceNames.FindAll(name => name.StartsWith(input));
    }
}