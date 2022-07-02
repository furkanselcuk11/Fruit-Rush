using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Type", menuName = "CarSO")]
public class CarSO : ScriptableObject
{
    [SerializeField] private int _selectedCar = 0;

    [SerializeField] private CarPrint[] _cars;
    public int selectedCar
    {
        get { return _selectedCar; }
        set { _selectedCar = value; }
    }
    public CarPrint[] cars
    {
        get { return _cars; }
        set { _cars = value; }
    }
}

[System.Serializable]
public class CarPrint
{
    public int index;
    public int price;

    public bool isUnlocked;
}