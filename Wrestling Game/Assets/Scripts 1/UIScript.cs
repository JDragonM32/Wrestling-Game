using UnityEngine;
using UnityEngine.UIElements;

public class UIScript : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    UIDocument uiDocument;

    ProgressBar playerHealth;

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        playerHealth = root.Q<ProgressBar>("PlayerHealth");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
