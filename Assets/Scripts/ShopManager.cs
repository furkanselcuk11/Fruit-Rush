using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private CarSO carType = null;    // Scriptable Objects eriþir 
    [SerializeField] private MoneySO moneyType = null;    // Scriptable Objects eriþir 

    [SerializeField] private int currentCarIndex;
    [SerializeField] private GameObject[] carModels;
    [SerializeField] private Button[] buyButtons;
    void Start()
    {
        CarUpdate();
    }
    void Update()
    {

    }
    public void ChangeCar(int newBall)
    {
        carModels[currentCarIndex].SetActive(false);
        carModels[newBall].SetActive(true);
        carType.selectedCar = newBall;
    }
    public void CarUpdate()
    {
        currentCarIndex = carType.selectedCar;
        foreach (GameObject ball in carModels)
        {
            ball.SetActive(false);
        }
        carModels[currentCarIndex].SetActive(true);
    }
    public void UpdateButtons()
    {
        for (int i = 0; i < carType.cars.Length; i++)
        {
            if (carType.cars[i].isUnlocked)
            {
                buyButtons[i].gameObject.SetActive(false);  // Eğer Ball alınmış (isUnlocked) ise satın alma tuşu pasif olacak
                buyButtons[i].transform.parent.GetComponent<Button>().interactable = true;  // Eğer Ball alınmış (isUnlocked) ise  ball seçilebilir olacak
            }
            else
            {
                buyButtons[i].gameObject.SetActive(true);   // Eğer Ball alınmamış (isUnlocked) ise satın alma tuşu aktif olacak
                buyButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "BUY " + carType.cars[i].price;    // Satın alınacak ball fiyatı
                buyButtons[i].transform.parent.GetComponent<Button>().interactable = false;       // Eğer Ball alınmamış (isUnlocked)ise  ball seçilemez olacak    

            }
        }
    }
    public void CarBuy(int newCar)
    {
        if (moneyType.totalMoney >= carType.cars[newCar].price)
        {
            carType.cars[newCar].isUnlocked = true;
            buyButtons[newCar].gameObject.SetActive(false);  // Eğer Ball alınmış (isUnlocked) ise satın alma tuşu pasif olacak
            buyButtons[newCar].transform.parent.GetComponent<Button>().interactable = true;  // Eğer Ball alınmış (isUnlocked) ise  ball seçilebilir olacak
            moneyType.totalMoney -= carType.cars[newCar].price;
            UIController.uicontrollerInstance.UpdateUI();
        }
        else
        {
            carType.cars[newCar].isUnlocked = false;
        }
    }
}