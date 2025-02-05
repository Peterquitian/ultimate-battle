using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public Tilemap tilemap; // Arrastra aquí tu Tilemap desde el editor.

    void Start()
    {
        // Convertir una posición del mundo a coordenadas de celda del Tilemap.
        Vector3 worldPosition = new Vector3(1.5f, 1.5f, 0f); // Ejemplo de posición en el mundo.
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        Debug.Log("La celda correspondiente es: " + cellPosition);

        // Convertir de celda a posición en el mundo (para mover un objeto, por ejemplo).
        Vector3 newWorldPosition = tilemap.GetCellCenterWorld(cellPosition);
        Debug.Log("El centro de la celda en el mundo es: " + newWorldPosition);
    }
}