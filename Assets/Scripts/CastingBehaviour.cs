using TMPro;
using UnityEngine;

public class CastingBehaviour : MonoBehaviour
{
    public PlayerManager PlayerManager;
    void Start()
    {
        PlayerManager = GetComponent<PlayerManager>();
        if (PlayerManager.UIInputWindow != null)
        {
            var inputField = PlayerManager.UIInputWindow.GetComponentInChildren<TMP_InputField>();
            inputField.onSubmit.AddListener(HandleSpell);
        }
    }
    void Update()
    {
        
       
    }

    //private void CreateGameObjectWithScript(string name)
    //{
    //    GameObject newObject = new GameObject(name);

    //    System.Type scriptType = System.Type.GetType(name);

    //    if (scriptType != null)
    //    {
    //        newObject.AddComponent(scriptType);
    //        Debug.Log($"Script {name} aggiunto al GameObject {name}");
    //    }
    //    else
    //    {
    //        Debug.LogError($"Nessuno script trovato con il nome: {name}. Assicurati che esista e sia nello stesso namespace del progetto.");
    //    }
    //}
    private void HandleSpell(string input)
    {Debug.Log(input);
        CreateGameObjectWithPrefab(input);
    }
    private void CreateGameObjectWithPrefab(string name)
    {
        GameObject prefab = Resources.Load<GameObject>($"{ResourceManager.Instance.resourceFolder}/{name}");

        if (prefab != null)
        {
            GameObject newObject = Instantiate(prefab);
            Debug.Log($"Prefab {name} instanziato correttamente.");
        }
        else
        {
            Debug.LogError($"Nessun prefab trovato con il nome: {name}. Assicurati che sia nella cartella 'Resources'.");
        }
    }
}
