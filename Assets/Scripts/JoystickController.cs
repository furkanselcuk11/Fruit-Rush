using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private BasketSO basketType = null;    // Scriptable Objects erişir 
    [SerializeField] private MoneySO moneyType = null;    // Scriptable Objects erişir 

    [Space]
    [Header("Player Controller")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed = 500;

    Rigidbody rb;
    private Animator anim;

    [Space]
    [Header("Joystick Controller")]
    [SerializeField] Joystick joystick;   // Joystick scripti
    public float vertical, horizontal; // Player y�n�  
    [Space]
    [Header("Basket Controller")]
    [SerializeField] private GameObject collectedBoxFruits;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        // Sepetteki meyvelerin aktiflik kontrolü
        if (basketType.totalFruit == basketType.minFruit)
        {
            collectedBoxFruits.SetActive(false);
        }
        else
        {
            collectedBoxFruits.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        JoystickMove();   // Joystick kontrol� �al���r 
    }
    public void JoystickMove()
    {
        // Joystick kontrol�
        if (GameManager.gamemanagerInstance.isFinish)
        {
            vertical = joystick.Vertical * _movementSpeed * Time.fixedDeltaTime;
            horizontal = joystick.Horizontal * _movementSpeed * Time.fixedDeltaTime;

            rb.velocity = new Vector3(horizontal, 0, vertical);
            if (rb.velocity != Vector3.zero)
            {
                anim.SetBool("isRunning", true);
                Quaternion temp = Quaternion.LookRotation(rb.velocity, Vector3.up);
                rb.rotation = Quaternion.RotateTowards(transform.rotation, temp, _rotationSpeed * Time.fixedDeltaTime);
                GameManager.gamemanagerInstance.CollectionBox.SetActive(true);
            }
            else
            {                
                anim.SetBool("isRunning", false);
            }
        }
        else
        {
            GameManager.gamemanagerInstance.CollectionBox.SetActive(false);
        }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            AudioController.audioControllerInstance.Play("MoneySound");
            moneyType.totalMoney+=other.gameObject.GetComponent<Money>().value;
            UIController.uicontrollerInstance.totalMoneyTxt.text = moneyType.totalMoney.ToString();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Market"))
        {
            // Eğer temas edilen obje market ise elindeki tüm meyveleri sat
            Debug.Log("Market");
            if (basketType.totalFruit > 0)
            {
                // Eğer sepetinde 1 ve 1 den fazla meyve var ise
                moneyType.totalMoney += basketType.totalFruit;    // Sepetteki meyve kadar para ekler
                basketType.totalFruit = 0;
                UIController.uicontrollerInstance.totalMoneyTxt.text = moneyType.totalMoney.ToString();
            }
        }
    }
}