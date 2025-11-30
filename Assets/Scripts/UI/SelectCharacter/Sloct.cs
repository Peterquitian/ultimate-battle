using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sloct : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

    void FixedUpdate()
    {
        if(_isHolding && _character != null)
        {
            Debug.Log(_character);
            _imgCharacter.transform.position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y);
        }
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
        Debug.Log("CLICK ABAJO");
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _isHolding = true;
            _imgCharacter.raycastTarget = false;
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(eventData.button);

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _isHolding = false;
            _imgCharacter.transform.position = _DefaultPosition;
            _imgCharacter.raycastTarget = true;

            if (eventData.pointerClick && _character != null)
            {
                _managerSloct.UpdateCurrentSloct(this);
                return;
            }

            if(eventData.pointerEnter?.GetComponent<Sloct>() is Sloct newSloct)
            {
                
                if(newSloct.InBuyCar != InBuyCar)
                {
                    _managerSloct.BuyCharacter(this);
                    return;
                }

                Debug.Log($"Entra al point enter con el objeto {newSloct.name}\nEstado del cart {newSloct._character.name} y el actual {_character.name}");
                
            }

        }



    }

}
