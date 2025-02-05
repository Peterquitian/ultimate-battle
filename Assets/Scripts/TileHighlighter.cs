using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; // Tilemap a remarcar
    [SerializeField] private Color highlightColor = Color.yellow; // Color de remarcar
    [SerializeField] private Color defaultColor = Color.white; // Color por defecto
    private Vector3Int lastCellPosition; // Posición de la última celda remarcada
    private bool isHighlighted = false; // Bandera para saber si una casilla está resaltada

    private void Update()
    {
        // Obtén la posición del mouse en coordenadas del mundo
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Convierte esa posición a coordenadas de celda
        Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPosition);

        // Si la posición cambia, actualiza el resaltado
        if (cellPosition != lastCellPosition)
        {
            RemoveHighlight();
            HighlightCell(cellPosition);
            lastCellPosition = cellPosition;
        }
    }

    private void HighlightCell(Vector3Int cellPosition)
    {
        if (tilemap.HasTile(cellPosition))
        {
            TileBase tile = tilemap.GetTile(cellPosition);
            if (tile != null)
            {
                // Cambia el color de la casilla
                tilemap.SetTileFlags(cellPosition, TileFlags.None);
                tilemap.SetColor(cellPosition, highlightColor);
                isHighlighted = true;
            }
        }
    }

    private void RemoveHighlight()
    {
        if (isHighlighted && tilemap.HasTile(lastCellPosition))
        {
            // Restaura el color por defecto de la última casilla resaltada
            tilemap.SetTileFlags(lastCellPosition, TileFlags.None);
            tilemap.SetColor(lastCellPosition, defaultColor);
            isHighlighted = false;
        }
    }
}
