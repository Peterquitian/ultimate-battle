using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Vector3 _defaultPosition;
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        if (_canvasGroup == null)
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        _defaultPosition = _rectTransform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(">>> Inicia el arrastre");

        _canvasGroup.blocksRaycasts = false;

        _canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = eventData.delta / _rectTransform.localScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("<<< Fin del arrastre");

        _canvasGroup.blocksRaycasts = true;

        _canvasGroup.alpha = 1f;
    }
}
