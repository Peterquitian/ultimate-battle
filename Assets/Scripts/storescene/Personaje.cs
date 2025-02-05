using UnityEngine;

public class Personaje : MonoBehaviour
{
    // Forma del personaje, inicialmente vacío
    [SerializeField]
    private string forma = "";

    // Color como palabra, inicializado vacío
    [SerializeField]
    private string color = "";

    // Propiedades para acceder a los valores
    public string Forma => forma;
    public string Color => color;

    // Método para mostrar en consola
    void Start()
    {
        // Mostrar en consola lo que el jugador ingresó en el Inspector
        Debug.Log($"Forma: {forma}, Color: {color}");
    }
}
