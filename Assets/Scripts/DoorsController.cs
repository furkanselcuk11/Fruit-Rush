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
        if (doorsType.locked)
        {
            // Kapý kapalý ise
            lockedPlane.GetComponent<MeshRenderer>().material.color = doorsType.lockedColor;
        }
        else
        {
            // Kapý açýk ise
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
        //    // Kapý açýlýr
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
                // Kapý kapalý ise
                if (moneyType.totalMoney > moneyType.minMoney)
                {
                    moneyType.totalMoney--;    // Toplanan para sayýsýný azalt
                    GameManager.gamemanagerInstance.totalMoneyTxt.text = moneyType.totalMoney.ToString();
                    doorsType.currentMoney++;    // Binanýn toplanan para sayýsýný arttýr
                    doorText.text = doorsType.currentMoney.ToString() + " / " + doorsType.totalMoney.ToString();
                    AudioController.audioControllerInstance.Play("BuildingSound");
                    if (doorsType.currentMoney == doorsType.totalMoney)
                    {
                        // Binan toplanan meyve sayýsý max olduðu zaman kilit kaldýr ve ses çal
                        AudioController.audioControllerInstance.Play("BuildingOpenedSound");
                        doorsType.locked = false;    // Kapý açýlýr
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
                //kapý açýldý ise
                Debug.Log("Bina açýldý");
                transform.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
