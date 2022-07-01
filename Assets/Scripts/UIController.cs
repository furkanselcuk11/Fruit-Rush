using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController uicontrollerInstance;

    [SerializeField] private BasketSO basketType = null;    // Scriptable Objects eriþir 
    [SerializeField] private MoneySO moneyType = null;    // Scriptable Objects eriþir 

    [Header("Game UI Controller")]
    [SerializeField] private GameObject GameStartPanel;
    [SerializeField] private GameObject GameRunTimePanel;
    [SerializeField] private GameObject GameFinishPanel;
    [Space]
    [Header("Score Controller")]
    public TextMeshProUGUI totalMoneyTxt;
    public TextMeshProUGUI currentMoneyTxt;
    public TextMeshProUGUI fruitLevelTxt;
    public TextMeshProUGUI fruitLevelUPTxt;

    private void Awake()
    {
        if (uicontrollerInstance == null)
        {
            uicontrollerInstance = this;
        }
    }
    void Start()
    {
        UpdateUI();
    }

    
    void Update()
    {
        
    }
    public void UpdateUI()
    {
        currentMoneyTxt.text = moneyType.currentMoney.ToString();
        totalMoneyTxt.text = moneyType.totalMoney.ToString();
        fruitLevelTxt.text = "Lvl " + basketType.priceLevel.ToString();
        fruitLevelUPTxt.text = "UP "+basketType.pricelevelUP.ToString();
    }
    public void UpdatePanel()
    {
        if (GameManager.gamemanagerInstance.startGame & !GameManager.gamemanagerInstance.isFinish)
        {
            GameStartPanel.SetActive(false);
            GameRunTimePanel.SetActive(true);
            GameFinishPanel.SetActive(false);
        }
        else
        {
            GameStartPanel.SetActive(false);
            GameRunTimePanel.SetActive(false);
            GameFinishPanel.SetActive(true);
        }
        if (!GameManager.gamemanagerInstance.startGame & !GameManager.gamemanagerInstance.isFinish)
        {
            GameStartPanel.SetActive(true);
            GameRunTimePanel.SetActive(false);
            GameFinishPanel.SetActive(true);
        }
    }
}
