using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterList", menuName = "ScriptableObjects/Characterlist", order = 1)]
public class CharacterList : ScriptableObject
{
    public List<Character> character;
    public Dictionary<int, Character> IdToCharacter;

    private void Awake() {
        IdToCharacter = new Dictionary<int, Character>();

        foreach(var item in character)
        {
            IdToCharacter.Add(item.Id, item);
        }
    }

    public Character FindCharacterForId(int id)
    {
        return IdToCharacter[id];
    }

}
