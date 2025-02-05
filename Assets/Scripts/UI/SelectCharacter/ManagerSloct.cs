using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerSloct : MonoBehaviour
{
    private Sloct _currentSloct;
    [SerializeField] CharacterList _listCharacterSO;
    [SerializeField] CharacterInfoDisplay _characterInfoDisplay;
    [SerializeField] List<Character> _listCharacter = new List<Character>();
    [SerializeField] List<Character> _listCharacterBuy = new List<Character>();
    [SerializeField] int _currentPlayer = 0;
    [SerializeField] List<Sloct> _buyList = new List<Sloct>();
    [SerializeField] List<CharacterSpawn> _totalBuyList = new List<CharacterSpawn>();
    [SerializeField] List<Sloct> _SloctCharacterList = new List<Sloct>();

    [SerializeField] GameObject canvasStore;

    [SerializeField] Transform[] _pointPlayers;

    [SerializeField] InstanteateCharacter _instanteateCharacter;

    [SerializeField] Button playBtn;

    public CharacterInfoDisplay CharacterInfoDisplay => _characterInfoDisplay;

    private void Awake() {
        _currentPlayer = 0;
        playBtn.onClick.AddListener(Buy);
    }

    public void Setup()
    {
        SceneSetup();

        canvasStore.SetActive(true);
        GameTurnManager.Instance.UpdatePlayerInturnUI(_currentPlayer + 1);
        
    }

    public void UpdateCurrentSloct(Sloct sloct)
    {
        if (sloct.Character == null)
        {
            return;
        }
        Debug.Log(_currentSloct + " " + sloct);
        if (_currentSloct == sloct)
        {
            if (_currentSloct.InBuyCar)
            {
                Character character = sloct.Character;
                _listCharacterBuy.Remove(character);
                _listCharacter.Add(character);

                //_currentSloct.ResetSloct();
                UpdateSloctUI();
                _currentSloct = null;

                Debug.Log("Entra inbuy");
                return;
            }

            AddCharacter(sloct);
            UpdateSloctUI();
            //_currentSloct.ResetSloct();


            _currentSloct = null;
            Debug.Log("No Entra inbuy");
            return;
        }
        _currentSloct = sloct;
        _characterInfoDisplay.UpdateInfoCharacterDisplay(_currentSloct.Character);
    }

    public void AddCharacter(Sloct sloct)
    {
        Character character = sloct.Character;
        _listCharacterBuy.Add(character);
        _listCharacter.Remove(character);
    }

    public void SceneSetup()
    {
        //UpdateSloctUI();

        foreach (Sloct sloct in _SloctCharacterList)
        {
            int ramdonIndex = Random.Range(0, _listCharacterSO.character.Count);
            _listCharacter.Add(_listCharacterSO.character[ramdonIndex]);
            

        }

        for(int i = 0; i > _SloctCharacterList.Count; i++)
        {
            _SloctCharacterList[i].UpdateSloct(_listCharacter[i], this);
        }
        UpdateSloctUI();
    }

    public void ChangePlayer()
    {
        _currentPlayer++;
    }

    public void UpdateSloctUI()
    {
        foreach(Sloct sloct in _SloctCharacterList)
        {

            sloct.ResetSloct();

        }

        int indexA = 0;
        Debug.Log($"list character {_listCharacter.Count} | list character buy {_listCharacterBuy}");
        foreach(Character character in _listCharacter)
        {

            _SloctCharacterList[indexA].UpdateSloct(character,this);

            indexA++;
        }
        Debug.Log(indexA);

        foreach(Sloct sloct in _buyList)
        {
            sloct.ResetSloct();

        }

        int indexB = 0;
        foreach(Character character in _listCharacterBuy)
        {

            _buyList[indexB].UpdateSloct(character,this);

            indexB++;
        }
    }

    public void StarGame()
    {

        _instanteateCharacter.setUpdateList(_totalBuyList);
        _instanteateCharacter.Inicializate();

        CanvasOff();
        
        GameTurnManager.Instance.Initialize();
    }

    public void Buy()
    {
        int index = 0;
        foreach(Character character in _listCharacterBuy)
        {
            CharacterSpawn newCharacter = new CharacterSpawn();
            newCharacter.IdCharacter = character.Id;
            newCharacter.PlayerId = _currentPlayer+1;
            newCharacter.PositionInitial = _pointPlayers[_currentPlayer].position + Vector3Int.down * index;

            _totalBuyList.Add(newCharacter);

            index++;
        }

        _listCharacter.Clear();
        _listCharacterBuy.Clear();


        if(_currentPlayer > 0)
        {
            Debug.Log("oppay");
            StarGame();
        }else
        {
            nextPlayer();
        }
        
    }

    public void nextPlayer()
    {
        _currentPlayer = 1;
        GameTurnManager.Instance.UpdatePlayerInturnUI(_currentPlayer + 1);

        Setup();
    }

    public void CanvasOff()
    {
        canvasStore.SetActive(false);
    }

}


