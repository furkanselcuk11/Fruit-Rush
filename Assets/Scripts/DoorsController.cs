using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField] private DoorsSO doorsType = null;    // Scriptable Objects eri�ir 
    [SerializeField] private MoneySO moneyType = null;    // Scriptable Objects eri�ir 
    [SerializeField] private GameObject lockedPlane;
    [SerializeField] private TextMeshPro doorText;
    void Start()
    {
        if (doorsType.locked)
        {
            // Kap� kapal� ise
            lockedPlane.GetComponent<MeshRenderer>().material.color = doorsType.lockedColor;
        }
        else
        {
            // Kap� a��k ise
            lockedPlane.GetComponent<MeshRenderer>().material.color = doorsType.unlockedColor;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetComponent<BoxCollider>().isTrigger = true;
        }
        //lockedPlane.GetComponent<MeshRenderer>().material.color = doorsType.lockedColor;
        doorText.text = doorsType.currentMoney.ToString() + " / " + doorsType.totalMoney.ToString();
    }
    void Update()
    {
        //if (!doorsType.locked)
        //{
        //    lockedPlane.GetComponent<MeshRenderer>().material.color = doorsType.unlockedColor;
        //    // Kap� a��l�r
        //    transform.GetChild(0).gameObject.SetActive(false);
        //    transform.GetComponent<BoxCollider>().isTrigger = true;
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorsType.locked)
            {
                // Kap� kapal� ise
                if (moneyType.totalMoney > moneyType.minMoney)
                {
                    moneyType.totalMoney--;    // Toplanan para say�s�n� azalt
                    GameManager.gamemanagerInstance.totalMoneyTxt.text = moneyType.totalMoney.ToString();
                    doorsType.currentMoney++;    // Binan�n toplanan para say�s�n� artt�r
                    doorText.text = doorsType.currentMoney.ToString() + " / " + doorsType.totalMoney.ToString();
                    AudioController.audioControllerInstance.Play("BuildingSound");
                    if (doorsType.currentMoney == doorsType.totalMoney)
                    {
                        // Binan toplanan meyve say�s� max oldu�u zaman kilit kald�r ve ses �al
                        AudioController.audioControllerInstance.Play("BuildingOpenedSound");
                        doorsType.locked = false;    // Kap� a��l�r
                        lockedPlane.GetComponent<MeshRenderer>().material.color = doorsType.unlockedColor;
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetComponent<BoxCollider>().isTrigger = true;
                    }
                }
                else
                {
                    Debug.Log("Para eksik...");                    
                }
            }
            else
            {
                //kap� a��ld� ise
                Debug.Log("Bina a��ld�");
                transform.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
