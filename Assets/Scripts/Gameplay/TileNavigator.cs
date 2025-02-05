using UnityEngine;
using UnityEngine.Tilemaps;

public class TileNavigator : MonoBehaviour
{
    /*
    public Tilemap tilemap;

    private Character selectedCharacter; // Personaje actualmente seleccionado

    void Update()
    {
        HandleCharacterSelection();
        HandleCharacterMovement();
    }

    private void HandleCharacterSelection()
    {
        if (Input.GetMouseButtonDown(1)) // Botón derecho para seleccionar un personaje
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
                Character character = hit.collider.GetComponent<Character>();
                if (character != null && !character.Movement.HasMoved)
                {
                    selectedCharacter = character;
                    Debug.Log($"Seleccionaste a {character.gameObject.name}");
                }
            }
        }
    }

    private void HandleCharacterMovement()
    {
        if (selectedCharacter == null || Input.GetMouseButtonDown(0) == false)
            return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int targetCell = tilemap.WorldToCell(mouseWorldPos);

        if (!tilemap.HasTile(targetCell))
        {
            Debug.Log("No puedes moverte a esta celda.");
            return;
        }

        // Mueve al personaje
        selectedCharacter.Movement.MoveToCell(targetCell, tilemap);

        // Verifica si el turno ha terminado
        GameTurnManager.Instance.CheckTurnEnd();
    }
    */
}
