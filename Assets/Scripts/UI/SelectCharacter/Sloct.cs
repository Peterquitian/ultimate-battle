using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sloct : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    [SerializeField] Image _imgCharacter;

    [SerializeField] Character _character;

    [SerializeField] ManagerSloct _managerSloct;
    public bool InBuyCar;

    public Character Character => _character;

    private void Awake() {
        _imgCharacter = GetComponent<Image>();
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
    }

    public void OnPointerDown(PointerEventData a)
    {
        _managerSloct.UpdateCurrentSloct(this);
    }

    public void OnPointerUp(PointerEventData a)
    {

    }

}
