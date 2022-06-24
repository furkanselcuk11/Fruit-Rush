using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Money Type", menuName = "MoneySO")]
public class MoneySO : ScriptableObject
{    
    [SerializeField] private float _totalMoney;
    [SerializeField] private float _currentMoney;
    [SerializeField] private float _minMoney;
        
    public float totalMoney
    {
        get { return _totalMoney; }
        set { _totalMoney = value; }
    }
    public float currentMoney
    {
        get { return _currentMoney; }
        set { _currentMoney = value; }
    }
    public float minMoney
    {
        get { return _minMoney; }
        set { _minMoney = value; }
    }
}
