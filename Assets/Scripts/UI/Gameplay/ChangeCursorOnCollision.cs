using UnityEngine;

public class ChangeCursorOnCollision : MonoBehaviour
{
    // Cursor personalizado
    public Texture2D customCursor;

    // Punto de anclaje del cursor
    public Vector2 hotSpot = Vector2.zero;

    private Texture2D defaultCursor;

    void Start()
    {
        defaultCursor = null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameTurnManager.Instance.CurrentStateGame == StateGame.InGame)
        {
            Debug.Log("Colisiono");
            if (customCursor == null)
            {
                return;
            }

            Debug.Log("No es nulo");

            if (collision.gameObject.TryGetComponent<Node>(out Node node))
            {
                Debug.Log("es un nodo");
                if (node.IsEnemyPresent())
                {
                    Debug.Log("Hay un enemigo");
                    Cursor.SetCursor(customCursor, hotSpot, CursorMode.Auto);
                }

            }
        }


    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Restaura el cursor predeterminado al salir de la colisión
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

}