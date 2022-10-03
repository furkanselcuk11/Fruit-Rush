using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Money Type", menuName = "MoneySO")]
public class MoneySO : ScriptableObject
{    
    [SerializeField] private int _totalMoney;
    [SerializeField] private int _currentMoney;
    [SerializeField] private int _minMoney;
    [SerializeField] private int _currentLevel=1;
        
    public int totalMoney
    {
        get { return _totalMoney; }
        set { _totalMoney = value; }
    }
    public int currentMoney
    {
        get { return _currentMoney; }
        set { _currentMoney = value; }
    }
    public int minMoney
    {
        get { return _minMoney; }
        set { _minMoney = value; }
    }
    public int currentLevel
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }
}
