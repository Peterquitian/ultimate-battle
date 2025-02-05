using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CajaManager : MonoBehaviour
{
    [Header("Referencias a Cajas")]
    public GameObject[] cajasArriba;
    public GameObject[] cajasAbajo;

    [Header("Contadores")]
    public static int objetosArribaActivos = 1;
    public static int objetosAbajoActivos = 3;

    [Header("Generador de Personajes")]
    public GenerarPersonajes generarPersonajesScript;

    [Header("UI Botones")]
    [SerializeField] private Button botonIncrementar;
    [SerializeField] private Button botonDesactivar;

    [Header("UI Elementos")]
    [SerializeField] private Button botonNext;
    [SerializeField] private Button botonPrevious;
    [SerializeField] private TMP_Text textForma;
    [SerializeField] private TMP_Text textColor;
    [SerializeField] private Transform characterPosition;
    [SerializeField] private TMP_Text mensajeUI;

    [Header("Menu de Selecci�n de Personaje")]
    public CharacterSelectionMenu characterSelectionMenu;

    public GameObject spawnPoint;
    public float offsetX = 2f;

    private List<GameObject> clonesGenerados = new List<GameObject>();

    private IEnumerator EsconderMensajeTrasTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        if (mensajeUI != null)
        {
            mensajeUI.gameObject.SetActive(false); // Ocultar el mensaje despu�s del tiempo especificado
        }
    }

    private void Start()
    {
        if (generarPersonajesScript == null)
        {
            Debug.LogError("Generador de personajes no asignado.");
            return;
        }

        if (botonIncrementar != null)
        {
            botonIncrementar.onClick.RemoveAllListeners();
            botonIncrementar.onClick.AddListener(IncrementarContadores);
        }

        if (botonDesactivar != null)
        {
            botonDesactivar.onClick.RemoveAllListeners();
            botonDesactivar.onClick.AddListener(DesactivarCajasAbajoYGenerarSpawn);
        }
        ActivarCajasSegunContador();
    }

    public void ActivarCajasSegunContador()
    {
        Debug.Log("Activando cajas seg�n contadores...");

        EliminarClonesGenerados();

        ActivarCajas(cajasArriba, objetosArribaActivos);

        ActivarCajas(cajasAbajo, objetosAbajoActivos);

        generarPersonajesScript.GenerarPersonajesEnCajas();

        ActivarElementosUI();

        if (characterSelectionMenu.CharacterPrefabInstance != null)
        {
            characterSelectionMenu.CharacterPrefabInstance.SetActive(true);
        }
    }

    public void IncrementarContadores()
    {
        if (objetosArribaActivos < cajasArriba.Length)
            objetosArribaActivos++;

        if (objetosAbajoActivos < cajasAbajo.Length)
            objetosAbajoActivos++;

        Debug.Log($"Contadores incrementados: Arriba {objetosArribaActivos}, Abajo {objetosAbajoActivos}");
        ActivarCajasSegunContador();
    }

    public void DesactivarCajasAbajo()
    {
        // Verificar si todas las cajas activas de arriba tienen un prefab.
        bool todasLasCajasArribaActivasTienenPrefabs = true;

        foreach (GameObject caja in cajasArriba)
        {
            if (caja != null && caja.activeSelf && caja.transform.childCount == 0) // Solo revisar las cajas activas
            {
                todasLasCajasArribaActivasTienenPrefabs = false;
                break;
            }
        }

        // Si no todas las cajas activas de arriba est�n llenas, no desactivar las cajas de abajo.
        if (!todasLasCajasArribaActivasTienenPrefabs)
        {
            Debug.Log("No todas las cajas de arriba est�n llenas, no desactivando las cajas de abajo.");
            return; // Salir del m�todo sin hacer nada si no est�n todas llenas.
        }

        // Si todas las cajas activas de arriba est�n llenas, proceder con la desactivaci�n de las cajas de abajo.
        foreach (GameObject caja in cajasAbajo)
        {
            if (caja != null)
            {
                caja.SetActive(false);

                // Eliminar cualquier contenido de la caja de abajo.
                while (caja.transform.childCount > 0)
                {
                    DestroyImmediate(caja.transform.GetChild(0).gameObject);
                }

                Debug.Log($"Caja {caja.name} desactivada y contenido eliminado.");
            }
        }
    }


    public void DesactivarCajasAbajoYGenerarSpawn()
    {
        // Verificar si todas las cajas activas de arriba tienen un prefab.
        bool todasLasCajasArribaActivasTienenPrefabs = true;

        foreach (GameObject caja in cajasArriba)
        {
            if (caja != null && caja.activeSelf && caja.transform.childCount == 0) // Solo revisar las cajas activas
            {
                todasLasCajasArribaActivasTienenPrefabs = false;
                break;
            }
        }

        // Si no todas las cajas activas de arriba est�n llenas, no hacer nada.
        if (!todasLasCajasArribaActivasTienenPrefabs)
        {
            Debug.Log("Debes seleccionar m�s jugadores.");
            MostrarMensajeUI("Debes seleccionar m�s jugadores."); // Mostrar mensaje en la UI.
            return; // Salir del m�todo si no hay suficientes prefabs.
        }

        // Proceder a generar clones solo si todas las cajas activas de arriba tienen prefabs.
        Vector3 spawnPosition = spawnPoint.transform.position;

        // Eliminar clones generados previamente.
        EliminarClonesGenerados();

        foreach (GameObject caja in cajasArriba)
        {
            if (caja != null && caja.activeSelf) // Solo procesar las cajas activas
            {
                if (caja.transform.childCount > 0)
                {
                    GameObject prefabOriginal = caja.transform.GetChild(0).gameObject;

                    GameObject clon = Instantiate(prefabOriginal, spawnPosition, Quaternion.identity);

                    clonesGenerados.Add(clon);

                    spawnPosition.x += offsetX; // Asegurar que los clones se posicionen en X de forma progresiva.

                    caja.SetActive(false); // Desactivar la caja arriba.
                }
            }
        }

        // Desactivar las cajas de abajo solo si todas las cajas activas de arriba tienen prefabs.
        DesactivarCajasAbajo();

        // Desactivar los elementos de la UI.
        DesactivarElementosUI();

        // Desactivar el prefab de selecci�n de personaje.
        if (characterSelectionMenu.CharacterPrefabInstance != null)
        {
            characterSelectionMenu.CharacterPrefabInstance.SetActive(false);
        }
    }


    private void EliminarClonesGenerados()
    {
        foreach (GameObject clon in clonesGenerados)
        {
            Destroy(clon);
        }

        clonesGenerados.Clear();
    }

    public void ActivarElementosUI()
    {
        if (botonNext != null) botonNext.gameObject.SetActive(true);
        if (botonPrevious != null) botonPrevious.gameObject.SetActive(true);
        if (textForma != null) textForma.gameObject.SetActive(true);
        if (textColor != null) textColor.gameObject.SetActive(true);
    }

    public void DesactivarElementosUI()
    {
        if (botonNext != null) botonNext.gameObject.SetActive(false);
        if (botonPrevious != null) botonPrevious.gameObject.SetActive(false);
        if (textForma != null) textForma.gameObject.SetActive(false);
        if (textColor != null) textColor.gameObject.SetActive(false);
    }

    private void ActivarCajas(GameObject[] cajas, int cantidad)
    {
        for (int i = 0; i < cajas.Length; i++)
        {
            if (cajas[i] != null)
            {
                cajas[i].SetActive(i < cantidad);
                Debug.Log($"Caja {cajas[i].name} {(i < cantidad ? "activada" : "desactivada")}.");
            }
        }
    }

    private void MostrarMensajeUI(string mensaje)
    {
        if (mensajeUI != null)
        {
            mensajeUI.text = mensaje;
            mensajeUI.gameObject.SetActive(true);

            // Opcional: Ocultar el mensaje despu�s de unos segundos.
            StartCoroutine(EsconderMensajeTrasTiempo(3f));
        }
    }
}
