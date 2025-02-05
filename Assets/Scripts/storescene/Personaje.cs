using UnityEngine;

public class Personaje : MonoBehaviour
{
    // Forma del personaje, inicialmente vac�o
    [SerializeField]
    private string forma = "";

    // Color como palabra, inicializado vac�o
    [SerializeField]
    private string color = "";

    // Propiedades para acceder a los valores
    public string Forma => forma;
    public string Color => color;

    // M�todo para mostrar en consola
    void Start()
    {
        // Mostrar en consola lo que el jugador ingres� en el Inspector
        Debug.Log($"Forma: {forma}, Color: {color}");
    }
}
