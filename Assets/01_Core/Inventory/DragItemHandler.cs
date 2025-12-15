using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SlotView))]
public class DragItemHandler : MonoBehaviour, IBeginDragHandler, IDragHandler,  IEndDragHandler, IDropHandler
{
    private SlotView _slotView;
    private GameObject _dragginhIcon;
    private RectTransform _dragCanvasRect;

    private void Awake()
    {
        _slotView = GetComponent<SlotView>();
        Canvas canvas = GetComponentInParent<Canvas>();

        if(canvas != null)
        {
            _dragCanvasRect = canvas.GetComponent<RectTransform>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_slotView.IsEmpty)
        {
            return;
        }

        _dragginhIcon = new GameObject("DraggingIcon");
        _dragginhIcon.AddComponent<CanvasGroup>().blocksRaycasts = false;
        _dragginhIcon.transform.SetParent(_dragCanvasRect, false);
        _dragginhIcon.transform.SetAsLastSibling();

        Image iconImage = _dragginhIcon.AddComponent<Image>();
        iconImage.sprite = _slotView.CurrentItem.ItemIcon;
        iconImage.SetNativeSize();

        _slotView.SetTransparency(0.5f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_dragginhIcon == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_dragCanvasRect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
        _dragginhIcon.GetComponent<RectTransform>().localPosition = localPoint;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(_dragginhIcon == null) return;
        Destroy(_dragginhIcon);

        _dragginhIcon = null;

        _slotView.SetTransparency(1.0f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if(draggedObject == null)
        {
            Debug.LogWarning("OnDrop: No dragged object found");
            return;
        } 

        DragItemHandler sourceDragHandle = draggedObject.GetComponent<DragItemHandler>();
        if(sourceDragHandle == null) return;

        SlotView sourceSlot = sourceDragHandle._slotView;
        SlotView destinationSlot = _slotView;

        if(sourceSlot == destinationSlot) return;

        DropEvents.NotifyInteraction(sourceSlot, destinationSlot);
    }
}
