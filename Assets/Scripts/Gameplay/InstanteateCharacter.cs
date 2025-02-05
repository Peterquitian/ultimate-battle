using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InstanteateCharacter : MonoBehaviour
{
    [SerializeField] private List<CharacterSpawn> listCharacterSpawn; // Configuración de personajes a instanciar
    [SerializeField] private CharacterList characterList; // Lista de prefabs de personajes
    [SerializeField] private Tilemap tilemap; // Tilemap para calcular posiciones en el mundo

    [Header("Player Colors")]
    [SerializeField] private Color _colorPlayerOne;
    [SerializeField] private Color _colorPlayerTwo;
    private List<Character> player1 = new List<Character>();
    private List<Character> player2 = new List<Character>();

    public void setUpdateList(List<CharacterSpawn> listCharacter)
    {
        listCharacterSpawn = listCharacter;
        Debug.Log(listCharacterSpawn.Count);
    }

    public void Inicializate()
    {
        characterList = Instantiate(characterList);
        InitializePlayers();

        // Pasar las listas de jugadores al GameTurnManager
        List<List<Character>> players = new List<List<Character>> { player1, player2 };
        GameTurnManager.Instance.SetupPlayers(players);
    }

    public void InitializePlayers()
    {
        foreach (var item in listCharacterSpawn)
        {
            // Encuentra el prefab en la lista
            Character characterPrefab = characterList.FindCharacterForId(item.IdCharacter);

            if (characterPrefab == null)
            {
                Debug.LogError($"No se encontró un prefab para el ID {item.IdCharacter}");
                continue;
            }

            // Calcula la posición en el mundo
            Vector3 cellCenter = tilemap.GetCellCenterWorld(tilemap.WorldToCell(item.PositionInitial));

            // Instancia el personaje en la posición inicial
            Character character = Instantiate(characterPrefab, cellCenter, Quaternion.identity);
            character.Setup();

            // Asigna el personaje al jugador correspondiente
            if (item.PlayerId == 1)
            {
                //character.GetComponent<SpriteRenderer>().color= new Color(0.5f, 0.8f, 1f, 1f);
                character.GetComponent<SpriteRenderer>().material.SetColor("_PlayerColor", _colorPlayerOne);
                character.GetComponent<SpriteRenderer>().flipX = false;
                player1.Add(character);
            }
            else if (item.PlayerId == 2)
            {
                //character.GetComponent<SpriteRenderer>().color= new Color(1f, 0.4f, 0.4f, 1f);
                character.GetComponent<SpriteRenderer>().material.SetColor("_PlayerColor", _colorPlayerTwo);
                character.GetComponent<SpriteRenderer>().flipX = true;
                player2.Add(character);
            }
            else
            {
                Debug.LogWarning($"PlayerId {item.PlayerId} no reconocido. Personaje no asignado.");
            }
        }
    }
}

[System.Serializable]
public class CharacterSpawn
{
    public int PlayerId; // ID del jugador al que pertenece este personaje
    public int IdCharacter; // ID del personaje en CharacterList
    public Vector3 PositionInitial; // Posición inicial en el mundo
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanteateCharacter : MonoBehaviour
{
    [SerializeField] private List<CharacterSpawn> listCharacterSpawn;

    [SerializeField] private CharacterList characterList;

    [SerializeField] private List<Character> player1;
    [SerializeField] private List<Character> player2;

    // Start is called before the first frame update
    void Start()
    {
        characterList = Instantiate(characterList);
        InitializarPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializarPlayers()
    {
        foreach (var item in listCharacterSpawn)
        {
            Character character = Instantiate(characterList.FindCharacterForId(item.IdCharacter));
            if(item.PlayerId == 1)
            {
                player1.Add(character);
            }else
            {
                player2.Add(character);
            }
        }
    }
}

[System.Serializable]
public class CharacterSpawn
{
    public int PlayerId;
    public int IdCharacter;
    public Vector3 PositionInitial;

}
*/