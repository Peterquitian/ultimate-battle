using UnityEngine;
using UnityEngine.Tilemaps;

public class ManualEnemyMovement : MonoBehaviour
{
    public Tilemap tilemap; // Referencia al Tilemap
    public Transform enemy; // El enemigo manual que se moverá
    public int maxMovement = 3; // Máximo número de celdas que puede moverse
    public bool isEnemyTurn = false; // Determina si es el turno del enemigo

    private Vector3Int currentCell; // Celda actual del enemigo
    private int remainingMovement; // Movimientos restantes durante el turno

    void Start()
    {
        // Inicializar la posición del enemigo en la celda más cercana
        currentCell = tilemap.WorldToCell(enemy.position);
        remainingMovement = maxMovement;
    }

    void Update()
    {
        if (!isEnemyTurn) return;

        if (remainingMovement > 0)
        {
            HandleInput();
        }
        else
        {
            EndTurn();
        }
    }

    private void HandleInput()
    {
        Vector3Int direction = Vector3Int.zero;

        // Controles básicos para mover al enemigo (puedes personalizarlos)
        if (Input.GetKeyDown(KeyCode.W)) direction = Vector3Int.up; // Arriba
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector3Int.down; // Abajo
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector3Int.left; // Izquierda
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector3Int.right; // Derecha

        if (direction != Vector3Int.zero)
        {
            MoveToCell(currentCell + direction);
        }
    }

    private void MoveToCell(Vector3Int newCell)
    {
        if (!tilemap.HasTile(newCell)) return; // Asegurarse de que la celda sea válida

        currentCell = newCell;
        enemy.position = tilemap.GetCellCenterWorld(newCell);
        remainingMovement--;
    }

    private void EndTurn()
    {
        Debug.Log("Turno del enemigo manual terminado.");
        remainingMovement = maxMovement;
        isEnemyTurn = false;

        // Notificar al TurnManager que terminó el turno
        TurnManager.Instance.NextTurn();
    }
}
