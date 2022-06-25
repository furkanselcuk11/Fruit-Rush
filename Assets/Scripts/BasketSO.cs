using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basket Type", menuName = "BasketSO")]
public class BasketSO : ScriptableObject
{
    [SerializeField] private int _totalFruit;
    [SerializeField] private int _currentFruit;
    [SerializeField] private int _minFruit;
    [SerializeField] private float _fruitPrice=0.1f;
    [SerializeField] private int _priceLevel=1;

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
    public float fruitPrice
    {
        get { return _fruitPrice; }
        set { _fruitPrice = value; }
    }
}
