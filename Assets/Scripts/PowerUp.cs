using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string Name { get; private set; }
    public Rarity Rarity { get; private set; }
    public int Cost { get; private set; }

    public int Healt { get; private set;}
    public int Damage {get; private set;}
    

    public PowerUp(string name, Rarity rarity, int cost, int healt, int damage)
    {
        Name = name;
        Rarity = rarity;
        Cost = cost;
        Healt = healt;
        Damage = damage;
    }

}
