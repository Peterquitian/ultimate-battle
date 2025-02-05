using UnityEngine;

public class SideToSideMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // Velocidad del movimiento
    [SerializeField] private float range = 5f; // Distancia máxima hacia los lados

    [SerializeField] private Vector3 startPosition;

    void Start()
    {
        // Guardar la posición inicial del objeto
        //startPosition = transform.position;
        transform.position = startPosition; 
    }

    void Update()
    {
        // Calcular la nueva posición usando Mathf.PingPong
        float offset = Mathf.PingPong(Time.time * speed, range) - range / 2f;
        transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
    }
}
