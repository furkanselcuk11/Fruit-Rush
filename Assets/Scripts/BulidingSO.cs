using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Building Type", menuName = "BulidingSO")]
public class BulidingSO : ScriptableObject
{
    [SerializeField] private int _currentValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private int _moneyMaking;
    [SerializeField] private Color _lockedColor = Color.red;

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
    public Color lockedColor
    {
        get { return _lockedColor; }
        set { _lockedColor = value; }
    }
}
