using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public enum Turn { Player, ManualEnemy, AIEnemy } // Ciclo de turnos
    public Turn currentTurn;

    public TileMapMovement playerController; // Referencia al jugador
    public ManualEnemyMovement manualEnemy; // Referencia al enemigo controlado manualmente
    public EnemyAI aiEnemy; // Referencia a la IA enemiga

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentTurn = Turn.Player; // Inicia con el jugador
        playerController.isPlayerTurn = true; // Turno del jugador
        manualEnemy.isEnemyTurn = false; // Enemigo manual fuera de turno
    }

    public void NextTurn()
    {
        if (currentTurn == Turn.Player)
        {
            Debug.Log("Turno del jugador completado. Turno del enemigo manual.");
            currentTurn = Turn.ManualEnemy;
            playerController.isPlayerTurn = false;
            manualEnemy.isEnemyTurn = true; // Ahora es el turno del enemigo manual
        }
        else if (currentTurn == Turn.ManualEnemy)
        {
            Debug.Log("Turno del enemigo manual completado. Turno de los enemigos AI.");
            
            manualEnemy.isEnemyTurn = false;
            currentTurn = Turn.Player;
            playerController.isPlayerTurn = true;

            //currentTurn = Turn.AIEnemy;
            //StartCoroutine(EnemyTurnCoroutine());
            
        }
        /*else if (currentTurn == Turn.AIEnemy)
        {
            Debug.Log("Turno de los enemigos AI completado. Turno del jugador.");
            currentTurn = Turn.Player;
            playerController.isPlayerTurn = true;
        }
        */
    }

    private IEnumerator EnemyTurnCoroutine()
    {
        Debug.Log("Turno de los enemigos AI...");
        yield return new WaitForSeconds(1f); // Espera 1 segundo para simular el tiempo de procesamiento
        aiEnemy.TakeTurn(); // Llama al turno de la IA
        EndEnemyTurn(); // Finaliza el turno de la IA
    }

    public void EndEnemyTurn()
    {
        Debug.Log("Turno de la IA completado.");
        NextTurn(); // Pasa al siguiente turno
    }
}
