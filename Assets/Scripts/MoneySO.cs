using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Money Type", menuName = "MoneySO")]
public class MoneySO : ScriptableObject
{    
    [SerializeField] private int _totalMoney;
    [SerializeField] private int _minMoney;
        
    public int totalMoney
    {
        get { return _totalMoney; }
        set { _totalMoney = value; }
    }
    public int minMoney
    {
        get { return _minMoney; }
        set { _minMoney = value; }
    }
}
