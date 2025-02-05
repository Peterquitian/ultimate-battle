using TMPro;
using UnityEngine;


public class PlayerInTurn : MonoBehaviour
{
    [SerializeField] TMP_Text _playerInTurn;
    public void UpdatePlayerInturn(int playerIndex) {
        _playerInTurn.text = $"Jugador: {playerIndex}";
    }

}
