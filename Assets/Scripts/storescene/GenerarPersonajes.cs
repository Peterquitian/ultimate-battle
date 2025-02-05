using UnityEngine;
using System.Linq; 

public class GenerarPersonajes : MonoBehaviour
{
    public GameObject[] prefabsPersonajes; 
    private Transform[] cajasAbajo; 

    public void GenerarPersonajesEnCajas()
    {
        // Encontrar todas las cajas activas de abajo
        cajasAbajo = GameObject.FindGameObjectsWithTag("boxDown")
                                .Where(go => go.activeSelf) // Solo obtener las cajas activas
                                .Select(go => go.transform) // Convertir GameObjects a Transform
                                .ToArray();

        // Eliminar personajes actuales en las cajas de abajo
        foreach (Transform caja in cajasAbajo)
        {
            // Verificar si la caja ya tiene un personaje
            if (caja.childCount == 0) // Solo generar un personaje si la caja está vacía
            {
                int indiceAleatorio = Random.Range(0, prefabsPersonajes.Length); // Seleccionar un prefab aleatorio
               
                GameObject personaje = Instantiate(prefabsPersonajes[indiceAleatorio], caja.position, Quaternion.identity);
                personaje.GetComponent<Character>().Setup();
                personaje.transform.SetParent(caja); // Asignar al transform de la caja
                Debug.Log($"Generado {personaje.name} en la caja {caja.name}");
            }
        }
    }
}
