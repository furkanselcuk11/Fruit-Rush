using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Doors Type", menuName = "DoorsSO")]
public class DoorsSO : ScriptableObject
{
    [SerializeField] private float _totalMoney;
    [SerializeField] private float _currentMoney;
    [SerializeField] private bool _locked = true;
    [SerializeField] private Color _lockedColor = Color.red;
    [SerializeField] private Color _unlockedColor = Color.green;

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
