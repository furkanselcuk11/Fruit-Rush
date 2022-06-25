using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingSO buildingType = null;    // Scriptable Objects eriþir  
    [SerializeField] private BasketSO basketType = null;    // Scriptable Objects eriþir  
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
                // moneyPos etrafýnda random para üretir ve binada üretlien paradýn deðeri paraya aktarýlýr
                var position = new Vector3(Random.Range(moneyPos.position.x - 1f, moneyPos.position.x + 1f),
            moneyPos.position.y,
            Random.Range(moneyPos.position.z - 1f, moneyPos.position.z + 1f));
                GameObject newObj=Instantiate(moneyPrefab, position, Quaternion.identity);
                newObj.GetComponent<Money>().value = buildingType.monkeyMaking; // Binanýn para üretme deðeri eklenir
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
                basketType.totalFruit--;    // Toplanan meyve sayýsý azalt
                buildingType.currentValue++;    // Binanýn toplanan meyve sayýsýný arttýr
                buildingText.text = buildingType.currentValue.ToString() + " / " + buildingType.maxValue.ToString();
                AudioController.audioControllerInstance.Play("BuildingSound");
                if (buildingType.currentValue == buildingType.maxValue)
                {
                    // Binanýn toplanan meyve sayýsý max olduðu zaman kilit kaldýr ve ses çal
                    AudioController.audioControllerInstance.Play("BuildingOpenedSound");
                    buildingType.locked = false;    // Bina kiliti açýlýr
                    lockedPlane.GetComponent<MeshRenderer>().material.color = buildingType.unlockedColor;   // Rengi Yeil olsun
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
