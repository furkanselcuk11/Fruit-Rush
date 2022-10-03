using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basket Type", menuName = "BasketSO")]
public class BasketSO : ScriptableObject
{
    [SerializeField] private int _totalFruit;
    [SerializeField] private int _currentFruit;
    [SerializeField] private int _minFruit;
    [SerializeField] private int _fruitPrice=1;
    [SerializeField] private int _priceLevel=1;
    [SerializeField] private int _pricelevelUP=100;

    public int totalFruit
    {
        get { return _totalFruit; }
        set { _totalFruit = value; }
    }
    public int currentFruit
    {
        get { return _currentFruit; }
        set { _currentFruit = value; }
    }
    public int minFruit
    {
        get { return _minFruit; }
        set { _minFruit = value; }
    }
    public int fruitPrice
    {
        get { return _fruitPrice; }
        set { _fruitPrice = value; }
    }
    public int priceLevel
    {
        get { return _priceLevel; }
        set { _priceLevel = value; }
    }
    public int pricelevelUP
    {
        get { return _pricelevelUP; }
        set { _pricelevelUP = value; }
    }
}
