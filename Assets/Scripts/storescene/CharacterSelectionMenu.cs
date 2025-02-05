using UnityEngine;
using TMPro;

public class CharacterSelectionMenu : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform CharacterPosition;
    public int selectedCharacter = 0;

    public TMP_Text textForma;
    public TMP_Text textColor;

    private GameObject currentCharacterInstance;
    private string selectedCharacterDataName = "SelectedCharacter";

    private bool isInMenu = true;

    public GameObject CharacterPrefabInstance => currentCharacterInstance; // Propiedad para acceder al prefab generado

    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);

        SpawnCharacter(selectedCharacter);

        UpdateCharacterInfo(selectedCharacter);
    }

    private void SpawnCharacter(int index)
    {
        // Eliminar la instancia actual si existe
        if (currentCharacterInstance != null)
        {
            Destroy(currentCharacterInstance);
        }
        currentCharacterInstance = Instantiate(characterPrefabs[index], CharacterPosition.position, Quaternion.identity);

        // Configurar el personaje si tiene un componente Character
        Character characterComponent = currentCharacterInstance.GetComponent<Character>();
        if (characterComponent != null)
        {
            characterComponent.Setup(); // Inicializa el personaje usando su método Setup
        }

        if (isInMenu) // Solo desactivamos los componentes si es un personaje del menú
        {
            Rigidbody rb = currentCharacterInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            BoxCollider boxCollider = currentCharacterInstance.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                boxCollider.enabled = false; // Desactiva el BoxCollider
            }

            Component[] components = currentCharacterInstance.GetComponents<Component>();
            foreach (var component in components)
            {
                if (component is Rigidbody || component is BoxCollider)
                    continue; // No desactivar Rigidbody ni BoxCollider

                if (component is MonoBehaviour)
                {
                    ((MonoBehaviour)component).enabled = false;
                }
            }
        }
    }


    public void NextCharacter()
    {
        selectedCharacter++;
        if (selectedCharacter >= characterPrefabs.Length)
        {
            selectedCharacter = 0;
        }
        SpawnCharacter(selectedCharacter);
        UpdateCharacterInfo(selectedCharacter);
    }

    public void PreviousCharacter()
    {
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter = characterPrefabs.Length - 1;
        }

        SpawnCharacter(selectedCharacter);

        UpdateCharacterInfo(selectedCharacter);
    }

    private void UpdateCharacterInfo(int characterIndex)
    {
        Personaje personaje = currentCharacterInstance.GetComponent<Personaje>();
        textForma.text = "Forma: " + personaje.Forma;
        textColor.text = "Color: " + personaje.Color;
    }

    public void IncrementarNumeroDeCajas()
    {
        isInMenu = false;
        isInMenu = true;
    }
}
