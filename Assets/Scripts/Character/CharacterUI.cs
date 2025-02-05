using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CharacterUI : MonoBehaviour
{
    //Vida UI
    [SerializeField] Image _health;
    [SerializeField] TMP_Text _Movement;

    public void TakeDamage(float damage)
    {
        _health.fillAmount = damage;
    }

    public void UpdateMovement(int value)
    {
        _Movement.text = "" + value;
    }
}