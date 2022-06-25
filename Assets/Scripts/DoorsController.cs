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
        lockedPlane.GetComponent<MeshRenderer>().material.color = doorsType.lockedColor;
        doorText.text = doorsType.currentMoney.ToString() + " / " + doorsType.totalMoney.ToString();
    }
    void Update()
    {
        if (!doorsType.locked)
        {
            lockedPlane.GetComponent<MeshRenderer>().material.color = doorsType.unlockedColor;
            // Kap� a��l�r
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (moneyType.totalMoney > moneyType.minMoney & doorsType.locked)
            {
                moneyType.totalMoney--;    // Toplanan meyve say�s� azalt
                GameManager.gamemanagerInstance.totalMoneyTxt.text = moneyType.totalMoney.ToString();
                doorsType.currentMoney++;    // Binan�n toplanan meyve say�s�n� artt�r
                doorText.text = doorsType.currentMoney.ToString() + " / " + doorsType.totalMoney.ToString();
                AudioController.audioControllerInstance.Play("BuildingSound");
                if (doorsType.currentMoney == doorsType.totalMoney)
                {
                    // Binan toplanan meyve say�s� max oldu�u zaman kilit kald�r ve ses �al
                    AudioController.audioControllerInstance.Play("BuildingOpenedSound");
                    doorsType.locked = false;    // Kap� a��l�r
                }
            }
            else
            {
                Debug.Log("Eksik Malzeme veya Bina a��ld�");
                transform.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
