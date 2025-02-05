using UnityEngine;

public class DragObject2D : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 originalPosition;
    private Camera mainCamera;
    private BoxZone2D currentBox;
    private bool isDragged = false;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;

        if (mainCamera == null)
        {
            Debug.LogWarning("La cámara principal no está asignada. Asegúrate de que hay una cámara principal en la escena.");
        }
         Application.logMessageReceived += HandleLog;
    }
        void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
        void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
        void OnMouseDown()
    {
        if (mainCamera == null) return; 
        offset = transform.position - GetMouseWorldPosition();
        isDragged = true;
    }

    void OnMouseDrag()
    {
        if (mainCamera == null) return;

        if (isDragged)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }
        void OnMouseUp()
    {
        if (mainCamera == null) return; 
        isDragged = false;

        BoxZone2D[] allBoxesUp = FindObjectsOfType<BoxZone2D>(); 
        foreach (BoxZone2D boxUp in allBoxesUp)
        {
            if (boxUp.CompareTag("boxUp") && boxUp.currentObject == null) 
            {
                MoverABoxUp(boxUp);
                return;
            }
        }
        RegresarABoxDown();
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) return Vector3.zero;

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
        public void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
        Debug.Log($"{gameObject.name} regresó a la posición original.");
    }
    private void MoverABoxUp(BoxZone2D boxUp)
    {
        boxUp.currentObject = this.gameObject;
        transform.SetParent(boxUp.transform);
        transform.localPosition = Vector3.zero;
        Debug.Log($"El objeto {gameObject.name} se movió a la caja de arriba {boxUp.name}.");
    }
        private void RegresarABoxDown()
    {
        BoxZone2D[] allBoxesDown = FindObjectsOfType<BoxZone2D>(); 
        foreach (BoxZone2D boxDown in allBoxesDown)
        {
            if (boxDown.CompareTag("boxDown") && boxDown.currentObject == null) 
            {
                boxDown.currentObject = this.gameObject;
                transform.SetParent(boxDown.transform);
                transform.localPosition = Vector3.zero;
                Debug.Log($"El objeto {gameObject.name} regresó a la caja de abajo {boxDown.name}.");
                return;
            }
        }
                       ReturnToOriginalPosition();
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (logString.Contains("NullReferenceException"))
        {
                       return;
        }
        Debug.Log(logString);
    }
}
