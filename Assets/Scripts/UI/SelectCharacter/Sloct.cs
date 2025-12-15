using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sloct : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDropHandler
{
    // Start is called before the first frame update
    [SerializeField] Image _imgCharacter;

    [SerializeField] Character _character;

    [SerializeField] ManagerSloct _managerSloct;

    [SerializeField] Vector3 _DefaultPosition;

    private bool _isHolding;
    public bool InBuyCar;

    public Character Character => _character;

    void Awake()
    {
        _imgCharacter = GetComponent<Image>();
        _DefaultPosition = _imgCharacter.GetComponent<RectTransform>().position;
    }
    
    public void UpdateSloct(Character character, ManagerSloct managerSloct)
    {
        _character = character;
        _imgCharacter.sprite = character.ImgCharacter;

        _managerSloct = managerSloct;

    }

    public void ResetSloct()
    {
        _character = null;
        _imgCharacter.sprite = null;
        _isHolding = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(">>> El objeto se solto aquí en mi");

        if(eventData.pointerDrag != null)
        {
            DragItem dragItem = eventData.pointerDrag.GetComponent<DragItem>();

            if(dragItem != null)
            {
                if(dragItem?.GetComponent<Sloct>() is Sloct currentSloct)
                {
                    if(currentSloct.InBuyCar != InBuyCar)
                    {
                         _managerSloct.BuyCharacter(this);
                    }
                }
            }
        }
    }

}
