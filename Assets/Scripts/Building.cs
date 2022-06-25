using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingSO buildingType = null;    // Scriptable Objects eri�ir  
    [SerializeField] private BasketSO basketType = null;    // Scriptable Objects eri�ir  
    [SerializeField] private GameObject lockedPlane;
    [SerializeField] private TextMeshPro buildingText;
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private Transform moneyPos;
    [SerializeField] private float x;
    void Start()
    {
        x = buildingType.monkeyMaking;
        if (buildingType.locked)
        {
            lockedPlane.GetComponent<MeshRenderer>().material.color = buildingType.lockedColor;
        }
        else
        {
            lockedPlane.GetComponent<MeshRenderer>().material.color = buildingType.unlockedColor;
            
            for (int i = 0; i < 10; i++)
            {
                // moneyPos etraf�nda random para �retir ve binada �retlien parad�n de�eri paraya aktar�l�r
                var position = new Vector3(Random.Range(moneyPos.position.x - 1f, moneyPos.position.x + 1f),
            moneyPos.position.y,
            Random.Range(moneyPos.position.z - 1f, moneyPos.position.z + 1f));
                GameObject newObj=Instantiate(moneyPrefab, position, Quaternion.identity);
                newObj.GetComponent<Money>().value = buildingType.monkeyMaking; // Binan�n para �retme de�eri eklenir
                newObj.transform.parent = moneyPos;
            }            
        }

        buildingText.text = buildingType.currentValue.ToString() + " / " + buildingType.maxValue.ToString();       
        
    }    
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (basketType.totalFruit > basketType.minFruit & buildingType.locked)
            {
                basketType.totalFruit--;    // Toplanan meyve say�s� azalt
                buildingType.currentValue++;    // Binan�n toplanan meyve say�s�n� artt�r
                buildingText.text = buildingType.currentValue.ToString() + " / " + buildingType.maxValue.ToString();
                AudioController.audioControllerInstance.Play("BuildingSound");
                if (buildingType.currentValue == buildingType.maxValue)
                {
                    // Binan�n toplanan meyve say�s� max oldu�u zaman kilit kald�r ve ses �al
                    AudioController.audioControllerInstance.Play("BuildingOpenedSound");
                    buildingType.locked = false;    // Bina kiliti a��l�r
                    lockedPlane.GetComponent<MeshRenderer>().material.color = buildingType.unlockedColor;   // Rengi Yeil olsun
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
