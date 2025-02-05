using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapMovement : MonoBehaviour
{
    public Tilemap tilemap; // Referencia al Tilemap
    public Transform player; // Referencia al objeto del jugador (tu prefab de Character 1)
    public int maxMovement = 3; // Máximo de casillas que puede moverse por turno
    public int attackRange = 2; // Rango de ataque
    public int attackDamage = 10; // Daño del ataque

    private Vector3Int startCell; // Celda inicial del jugador
    private Character currentEnemy; // Referencia al enemigo al que el jugador puede atacar
    public bool isPlayerTurn = true; // Indica si es el turno del jugador

    void Start()
    {
        // Calcula la celda inicial del jugador según su posición
        startCell = tilemap.WorldToCell(player.position);
        player.position = tilemap.GetCellCenterWorld(startCell); // Centra al jugador
    }

    void Update()
    {
        if (isPlayerTurn)
        {
            if (Input.GetMouseButtonDown(0)) // Clic izquierdo para mover
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int targetCell = tilemap.WorldToCell(mouseWorldPos);

                // Calcula la distancia en casillas
                int distance = Mathf.Abs(targetCell.x - startCell.x) + Mathf.Abs(targetCell.y - startCell.y);

                if (distance <= maxMovement) // Si está dentro del rango permitido
                {
                    MovePlayerToCell(targetCell);

                    // Comprobar si el jugador está cerca de un enemigo
                    if (distance <= attackRange)
                    {
                        currentEnemy = FindEnemy(); // Asigna al enemigo cercano
                    }
                    else
                    {
                        currentEnemy = null; // Si no está cerca de ningún enemigo, asigna null
                    }

                    isPlayerTurn = false; // Termina el turno del jugador
                }
                else
                {
                    Debug.Log("El objetivo está fuera de rango.");
                }
            }

            // Si el jugador tiene un enemigo cercano, presionar la tecla "L" para atacar
            if (currentEnemy != null && Input.GetKeyDown(KeyCode.L))
            {
                AttackEnemy();
            }
        }
    }

    void MovePlayerToCell(Vector3Int targetCell)
    {
        // Mueve al jugador al centro de la celda seleccionada
        Vector3 cellCenter = tilemap.GetCellCenterWorld(targetCell);
        player.position = cellCenter;
        startCell = targetCell; // Actualiza la celda actual del jugador
        Debug.Log($"Jugador movido a la celda {targetCell}");
        TurnComplete(); // Notifica al TurnManager que el turno ha terminado
    }

    private void AttackEnemy()
    {
        if (currentEnemy != null)
        {
            currentEnemy.TakeDamage(attackDamage);
            Debug.Log($"Jugador atacó al enemigo y le quitó {attackDamage} de vida.");
            currentEnemy = null; // Después de atacar, reseteamos la referencia al enemigo
        }
    }

    private Character FindEnemy()
    {
        // Lógica para encontrar el enemigo más cercano
        // Puedes mejorar esto si hay más enemigos
        return GameObject.FindObjectOfType<Character>(); // Esto solo es un ejemplo simple
    }

    public void TurnComplete()
    {
        TurnManager.Instance.NextTurn(); // Llama al siguiente turno en el TurnManager
    }
}
