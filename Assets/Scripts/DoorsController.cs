using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField] private DoorsSO doorsType = null;    // Scriptable Objects eriþir 
    [SerializeField] private MoneySO moneyType = null;    // Scriptable Objects eriþir 
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
            // Kapý açýlýr
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
                moneyType.totalMoney--;    // Toplanan meyve sayýsý azalt
                GameManager.gamemanagerInstance.totalMoneyTxt.text = moneyType.totalMoney.ToString();
                doorsType.currentMoney++;    // Binanýn toplanan meyve sayýsýný arttýr
                doorText.text = doorsType.currentMoney.ToString() + " / " + doorsType.totalMoney.ToString();
                AudioController.audioControllerInstance.Play("BuildingSound");
                if (doorsType.currentMoney == doorsType.totalMoney)
                {
                    // Binan toplanan meyve sayýsý max olduðu zaman kilit kaldýr ve ses çal
                    AudioController.audioControllerInstance.Play("BuildingOpenedSound");
                    doorsType.locked = false;    // Kapý açýlýr
                }
            }
            else
            {
                Debug.Log("Eksik Malzeme veya Bina açýldý");
                transform.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
