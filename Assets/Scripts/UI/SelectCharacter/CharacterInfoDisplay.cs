using UnityEngine; 
using UnityEngine.UI;
using TMPro;

public class CharacterInfoDisplay : MonoBehaviour
{
    [Header("Componentes")]

    [SerializeField] Image _imgCharacter;
    [SerializeField] TMP_Text _name;
    //[SerializeField] TMP_Text _history;
    [SerializeField] TMP_Text _attack;
    [SerializeField] TMP_Text _health;
    [SerializeField] TMP_Text _movement;


    public void UpdateInfoCharacterDisplay(Character character)
    {
        _imgCharacter.sprite = character.ImgCharacter;
        _name.text = character.Nombre;
        //_history.text = character.History;
        _health.text = "Health: " + character.DefaultHealth;
        _attack.text = "Attack: " + character.DefaulAttack;
        _movement.text = "Movements: " + character.RemainingMovement;
    }
    
}
