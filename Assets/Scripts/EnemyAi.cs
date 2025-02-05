using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    public Tilemap tilemap; // Referencia al Tilemap
    public Transform player; // Referencia al jugador
    public int maxMovement = 3; // Rango máximo de movimiento
    public int attackRange = 1; // Rango de ataque
    public int attackDamage = 10; // Daño del ataque

    private Vector3Int enemyCell; // Posición actual del enemigo en la grilla
    private Vector3Int playerCell; // Posición del jugador en la grilla

    public void Initialize(Tilemap map, Transform playerTransform)
    {
        tilemap = map;
        player = playerTransform;
        enemyCell = tilemap.WorldToCell(transform.position);
    }

    public void TakeTurn()
    {
        Debug.Log("IA tomando turno.");
        if (player == null || tilemap == null) return;

        // Obtener la celda del jugador
        playerCell = tilemap.WorldToCell(player.position);

        // Calcular dirección hacia el jugador
        Vector3Int direction = playerCell - enemyCell;
        Vector3Int targetCell = enemyCell;

        // Determinar próximo movimiento
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            targetCell.x += direction.x > 0 ? 1 : -1;
        }
        else
        {
            targetCell.y += direction.y > 0 ? 1 : -1;
        }

        // Verificar si el movimiento está en rango
        int distance = Mathf.Abs(targetCell.x - enemyCell.x) + Mathf.Abs(targetCell.y - enemyCell.y);
        if (distance <= maxMovement)
        {
            MoveToCell(targetCell);

            // Verificar si está en rango de ataque
            if (distance <= attackRange)
            {
                AttackPlayer(); // Atacar al jugador
            }
        }
        else
        {
            Debug.Log("Movimiento fuera de rango.");
        }
    }

    private void MoveToCell(Vector3Int targetCell)
    {
        Vector3 cellCenter = tilemap.GetCellCenterWorld(targetCell);
        transform.position = cellCenter;
        enemyCell = targetCell; // Actualizar posición del enemigo
        Debug.Log($"Enemigo movido a la celda {targetCell}");
    }

    private void AttackPlayer()
    {
        // Aquí se asume que el jugador tiene el script Character adjunto
        Character playerCharacter = player.GetComponent<Character>();
        if (playerCharacter != null)
        {
            playerCharacter.TakeDamage(attackDamage);
            Debug.Log($"Enemigo atacó al jugador y le quitó {attackDamage} de vida.");
        }
    }
}