using System.Linq;
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
            inputField.onEndEdit.AddListener(HandleSpell);
        }
    }
    void Update()
    {
        
       
    }
    /// <summary>
    /// Questo metodo non istanzia un prefab, ma partendo da uno script istanzia un cubo con solo questo componente. Si potrebbe istanziare un manager...! eh sì
    /// </summary>
    /// <param name="scriptName"></param>
    private void CreateGameObjectWithScript(string scriptName)
    {
        GameObject newObject = new GameObject(scriptName);

        System.Type scriptType = System.Type.GetType(scriptName);

        if (scriptType != null)
        {
            newObject.AddComponent(scriptType);
            Debug.Log($"Script {scriptName} aggiunto al GameObject {scriptName}");
        }
        else
        {
            Debug.LogError($"Nessuno script trovato con il nome: {scriptName}. Assicurati che esista e sia nello stesso namespace del progetto.");
        }
    }
    private void HandleSpell(string input)
    {Debug.Log(input);
        CreateGameObjectWithPrefab(input);
    }
    private void CreateGameObjectWithPrefab(string name)
    {
        GameObject prefab = ResourceManager.Instance.totalResources.FirstOrDefault(gameObject => gameObject.name.StartsWith(name));

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
