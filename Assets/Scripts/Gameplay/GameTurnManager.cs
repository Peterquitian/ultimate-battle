using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
public enum StateGame
{
    InStore,
    InGame,
    GameOver
}
public class GameTurnManager : MonoBehaviour
{
    public static GameTurnManager Instance; // Singleton para acceso global

    [SerializeField] private Tilemap _tilemap; // Referencia al Tilemap
    [SerializeField] private List<List<Character>> _players; // Lista de listas de personajes
    private LayerMask _layerMaskCharacter;
    private LayerMask _layerMaskNode;
    private int _currentPlayerIndex = 0; // Índice del jugador actual
    private Character _selectedCharacter;
    public Tilemap Tilemap => _tilemap;

    [SerializeField] StateGame _currentStateGame;

    [Header("UI")]
    [SerializeField] private GameUIManager _uiManager;

    public StateGame CurrentStateGame => _currentStateGame;

    [SerializeField] private Animator _animator;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

        _currentStateGame = StateGame.InStore;
        _uiManager = FindObjectOfType<GameUIManager>();

        _layerMaskNode = LayerMask.GetMask("LayerNodo");
        _layerMaskCharacter = LayerMask.GetMask("InteractableLayer");

    }

    public void Initialize()
    {

        if (_tilemap == null)
        {
            Debug.LogError("Tilemap no asignado en GameTurnManager.");
            return;
        }

        _currentStateGame = StateGame.InGame;
        _uiManager.ShowUIInGame();
        StartPlayerTurn();
        _selectedCharacter = null;
    }

    private void Update()
    {
        if (CurrentStateGame == StateGame.InGame)
        {
            HandlePlayerInput();
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log($"Player 1{_players[0]} - {_players[0].Count}");
            Debug.Log($"Player 2{_players[1]} - {_players[1].Count}");
        }

    }

    public Tilemap GetTilemap()
    {
        return _tilemap;
    }

    public void SetupPlayers(List<List<Character>> playerLists)
    {
        if (playerLists == null || playerLists.Count == 0)
        {
            Debug.LogError("La lista de jugadores está vacía o no es válida.");
            return;
        }

        _players = playerLists;
        Debug.Log("Lista de Jugadores cargada correctamente.");


    }

    // Inicia el turno del jugador actual
    public void StartPlayerTurn()
    {
        List<Character> currentPlayerCharacters = _players[_currentPlayerIndex];
        UpdatePlayerInturnUI(_currentPlayerIndex + 1);

        foreach (Character character in currentPlayerCharacters)
        {
            character.Movement.ResetMovement();
            character.UpdateMovementUI();
        }

        Debug.Log($"Es el turno del Jugador {_currentPlayerIndex + 1}. ¡Mueve tus personajes!");
    }

    public void CheckTurnEnd()
    {

        List<Character> currentPlayerCharacters = _players[_currentPlayerIndex];


        foreach (Character character in currentPlayerCharacters)
        {
            if (!CharacterHasMoved(character))
                return; // Si hay al menos un personaje que no ha terminado, el turno sigue
        }

        Debug.Log($"Todos los personajes del Jugador {_currentPlayerIndex + 1} han terminado su turno.");

        EndPlayerTurn();

    }

    public void EndPlayerTurn()
    {
        Debug.Log($"el Jugador: {_currentPlayerIndex + 1} ha finalizado su turno");
        ChangePlayer();
    }

    private void ChangePlayer()
    {
        Debug.Log("Cambiando de Jugador");
        if (_currentPlayerIndex >= _players.Count - 1)
        {
            _currentPlayerIndex = 0;

        }
        else
        {
            _currentPlayerIndex++;
        }
        Debug.Log($"Jugador Seleccionado: {_currentPlayerIndex}");

        StartPlayerTurn();

    }

    private void HandlePlayerInput()
    {

        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit;

            if (_selectedCharacter == null)
            {
                hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 1f, _layerMaskCharacter);
            }
            else
            {
                hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 1f, _layerMaskNode);
            }

            Debug.Log(hit.collider);
            Debug.DrawRay(hit.centroid, Vector2.zero * 1f, Color.red, 2f);

            if (hit.collider != null)
            {
                if (_selectedCharacter == null && IsCharacterInCurrentPlayer(hit.collider.GetComponent<Character>()))
                {
                    _selectedCharacter = hit.collider.GetComponent<Character>();


                    Debug.Log($"Character selected {_selectedCharacter}");

                    if (CharacterHasMoved(_selectedCharacter))
                    {

                        Debug.Log($"Character {_selectedCharacter.name} no tiene movimientos disponibles");

                        DeselectCharacter();
                        return;
                    }

                    _selectedCharacter.InTurn();

                    return;
                }
                else if (_selectedCharacter != null && IsCharacterInCurrentPlayer(hit.collider.GetComponent<Character>()))
                {
                    _selectedCharacter.OutOfTurn();

                    _selectedCharacter = hit.collider.GetComponent<Character>();
                    _selectedCharacter.InTurn();
                    Debug.Log($"The Character has bin changed selected {_selectedCharacter}");

                    if (CharacterHasMoved(_selectedCharacter))
                    {
                        Debug.Log($"Character {_selectedCharacter.name} no tiene movimientos disponibles");
                        DeselectCharacter();
                        return;
                    }
                }

            }

            if (_selectedCharacter != null && !_selectedCharacter.Movement.HasMoved)
            {
                if (hit.collider == null)
                {
                    Debug.Log("1");

                    CheckOutOfTurnCharacter();
                    DeselectCharacter();
                    return;
                }

                Debug.Log("Entra al que envia la acción de movimiento");
                if (hit.collider.TryGetComponent<Node>(out Node node))
                {
                    Debug.Log("2");
                    node.Action();
                }

                //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //_selectedCharacter.Movement.MoveToCell(cellPos, _tilemap);
                //Debug.Log($"Character {_selectedCharacter.name} Se movio a la casilla {cellPos}");
                DeselectCharacter();

            }


            CheckTurnEnd(); // Revisa si el turno debe finalizar
        }
    }

    public void DeselectCharacter()
    {
        if (_selectedCharacter != null)
        {
            Debug.Log($"Character: {_selectedCharacter.name}  deselected");
            _selectedCharacter.OutOfTurn();
            _selectedCharacter = null;
        }
    }

    public void CheckOutOfTurnCharacter()
    {
        foreach (Character character in _players[_currentPlayerIndex])
        {
            character.OutOfTurn();
        }
    }
    public bool IsCharacterInCurrentPlayer(Character character)
    {
        return _players[_currentPlayerIndex].Contains(character);
    }

    private bool CharacterHasMoved(Character character)
    {
        return character.Movement.HasMoved;
    }

    public void UpdatePlayerInturnUI(int index)
    {
        _uiManager.UpdateCurrentPlayer(index);
    }

    public void CheckGame()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            if (_players[i].Count < 1)
            {

                _uiManager.ShowWinner(i == 0 ? 2 : 1);

                _currentStateGame = StateGame.GameOver;
                Debug.Log("GANAMOS?");
                return;
            }
        }
    }

    public void RemoveCharacter(Character character)
    {
        foreach (var player in _players)
        {
            if (player.Contains(character))
            {
                player.Remove(character);
                Debug.Log($"Personaje {character.name} eliminado.");

                CheckGame();
                return;
            }
        }
    }
}
