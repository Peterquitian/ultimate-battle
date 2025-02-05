using UnityEngine;
public class ControlEscenas : MonoBehaviour
{
    public CajaManager cajaManager;
        private void Start()
    {
        if (cajaManager == null)
        {
            Debug.LogError("CajaManager no asignado.");
        }
    }
        public void IncrementarCajas()
    {
        if (cajaManager != null)
        {
            cajaManager.IncrementarContadores();
        }
    }
        public void DesactivarCajasDeAbajo()
    {
        if (cajaManager != null)
        {
            cajaManager.DesactivarCajasAbajo();
        }
    }
}
