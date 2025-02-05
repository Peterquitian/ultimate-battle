using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaGameManager : MonoBehaviour
{
    public int cost = 0;
    public CurrencyManager currencyManager;
    public PruebaManagerUI pruebaManagerUI;
    // Start is called before the first frame update
    void Start()
    {
        currencyManager = new CurrencyManager(100);

        pruebaManagerUI.UpdatePezates(currencyManager.GetBalance());
        pruebaManagerUI.HidenCostPezates();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy(int amount)
    {
        currencyManager.SpendPezates(amount);
    }

    public int GetPezates()
    {
        return currencyManager.GetBalance();
    }

    public void add()
    {
        cost+= 10;
        pruebaManagerUI.UpdateCostPezates(cost);
        checkAmount();

    }

    public void delete()
    {
        if(cost >= 10)
        {
            cost-= 10;
        }

        pruebaManagerUI.UpdateCostPezates(cost);
        
        checkAmount();
        
    }

    public void buy()
    {
        currencyManager.SpendPezates(cost);
        pruebaManagerUI.UpdatePezates(currencyManager.GetBalance());

        cost = 0;
        pruebaManagerUI.UpdateCostPezates(cost);
        
        checkAmount();
    }

    public void checkAmount()
    {
        if(cost>0)
        {
            pruebaManagerUI.ShowCostPezates();
        }else
        {
            pruebaManagerUI.HidenCostPezates();
        }

        if(cost> currencyManager.GetBalance())
        {
            pruebaManagerUI.chanceColorRed();
        }else
        {
            pruebaManagerUI.chanceColorWhite();
        }
    }

}
