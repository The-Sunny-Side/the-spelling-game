using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public string resourceFolder = ""; 
    public List<string> resourceNames;
    public List<GameObject> totalResources;
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

        totalResources = Resources.LoadAll<GameObject>(resourceFolder).ToList();
        foreach (var resource in totalResources)
        {

            resourceNames.Add(resource.name.ToLower()); 
        }

        Debug.Log($"Loaded {resourceNames.Count} resources from folder {resourceFolder}");
    }

    public List<string> GetSuggestions(string input)
    {
        input = input.ToLower();
        if (!string.IsNullOrEmpty(input))
        {
            return resourceNames.FindAll(name => name.StartsWith(input));
        }
        else return new();
    }
}