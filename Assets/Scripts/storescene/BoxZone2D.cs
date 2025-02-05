using UnityEngine;
public class BoxZone2D : MonoBehaviour
{
    public GameObject currentObject = null;
    private CajaManager cajaManager;

    private void Start()
    {
        cajaManager = FindObjectOfType<CajaManager>();
    }
        private void OnMouseDown() 
    {
        if (currentObject != null && cajaManager != null)
        {
            
            for (int i = 0; i < cajaManager.cajasArriba.Length; i++)
            {
                if (cajaManager.cajasArriba[i].activeSelf && cajaManager.cajasArriba[i].transform.childCount == 0)
                {
                    BoxZone2D cajaArriba = cajaManager.cajasArriba[i].GetComponent<BoxZone2D>();
                    cajaArriba.MoverObjetoACaja(currentObject, cajaArriba);
                    break;
                }
            }
        }
    }
        private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag("Personaje")) return; 
        if (currentObject != null) 
        {
            ReturnToOriginalPosition(other.gameObject);
            return;
        }

        currentObject = other.gameObject; 
        other.transform.SetParent(transform);
        other.transform.localPosition = Vector3.zero;
        Debug.Log($"Objeto {other.name} se colocó en la caja.");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Personaje") && currentObject == other.gameObject)
        {
            currentObject = null; 
        }
    }

    private void ReturnToOriginalPosition(GameObject obj) 
    {
    }
        public void MoverObjetoACaja(GameObject objeto, BoxZone2D nuevaCaja)
    {
        if (currentObject != null)
        {
            currentObject.transform.SetParent(null);
            currentObject = null;
        }   
        nuevaCaja.currentObject = objeto;
        objeto.transform.SetParent(nuevaCaja.transform);
        objeto.transform.localPosition = Vector3.zero;
    }
}
