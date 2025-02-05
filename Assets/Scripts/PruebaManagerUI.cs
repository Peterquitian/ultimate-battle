using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PruebaManagerUI : MonoBehaviour
{
    public TMP_Text _UIPezates;
    public TMP_Text _UICostPezates;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HidenCostPezates()
    {
        _UICostPezates.gameObject.SetActive(false);
    }

    public void ShowCostPezates()
    {
        _UICostPezates.gameObject.SetActive(true);
    }

    public void UpdateCostPezates(int amount)
    {
        _UICostPezates.text = "- "+ amount.ToString();
    }

    public void UpdatePezates(int amount)
    {
        _UIPezates.text = amount.ToString();
    }

    public void chanceColorRed()
    {
        _UICostPezates.color = Color.red;
    }

     public void chanceColorWhite()
    {
        _UICostPezates.color = Color.white;
    }
}
