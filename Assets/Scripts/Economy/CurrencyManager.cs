
public class CurrencyManager
{
    private int _pezates;

    public CurrencyManager (int initializePezates)
    {
        _pezates = initializePezates;
    }

    public void AddPezates(int amount)
    {
        _pezates += amount;
    }

    public bool SpendPezates(int amount)
    {
        if(CanAfford(amount))
        {
            _pezates -= amount;
            return true;
        }
        return false;
    }

    public bool CanAfford(int amount)
    {
        return _pezates>= amount;
    }

    public int GetBalance()
    {
        return _pezates;
    }

}
