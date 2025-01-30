using UnityEngine;

public class CastingBehaviour : MonoBehaviour
{
    private bool isTyping = false;
    private string userInput = "";

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                isTyping = true;
                userInput = "";
                Debug.Log("Inizia a scrivere...");
            }
            else
            {
                isTyping = false;
                CreateGameObjectWithPrefab(userInput);
                //CreateGameObjectWithScript(userInput);
            }
        }

        if (isTyping)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b')
                {
                    if (userInput.Length > 0)
                    {
                        userInput = userInput.Substring(0, userInput.Length - 1);
                    }
                }
                else if (c == '\n' || c == '\r')
                {
                    continue;
                }
                else
                {
                    userInput += c;
                }
            }
        }
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
    private void CreateGameObjectWithPrefab(string name)
    {
        GameObject prefab = Resources.Load<GameObject>(name);

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
