using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Building Type", menuName = "BulidingSO")]
public class BuildingSO : ScriptableObject
{
    [SerializeField] private int _currentValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private int _moneyMaking;
    [SerializeField] private bool _locked=true;
    [SerializeField] private Color _lockedColor = Color.red;
    [SerializeField] private Color _unlockedColor = Color.green;

    public int currentValue
    {
        get { return _currentValue; }
        set { _currentValue = value; }
    }
    public int maxValue
    {
        get { return _maxValue; }
        set { _maxValue = value; }
    }
    public int monkeyMaking
    {
        get { return _moneyMaking; }
        set { _moneyMaking = value; }
    }
    public bool locked
    {
        get { return _locked; }
        set { _locked = value; }
    }
    public Color lockedColor
    {
        get { return _lockedColor; }
        set { _lockedColor = value; }
    }
    public Color unlockedColor
    {
        get { return _unlockedColor; }
        set { _unlockedColor = value; }
    }
}
