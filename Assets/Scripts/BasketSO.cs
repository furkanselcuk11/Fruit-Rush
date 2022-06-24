using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basket Type", menuName = "BasketSO")]
public class BasketSO : ScriptableObject
{
    [SerializeField] private int _totalFruit;
    [SerializeField] private int _minFruit;

    public int totalFruit
    {
        get { return _totalFruit; }
        set { _totalFruit = value; }
    }
    public int minFruit
    {
        get { return _minFruit; }
        set { _minFruit = value; }
    }
}
