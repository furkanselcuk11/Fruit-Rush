using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingSO buildingType = null;    // Scriptable Objects eriþir  
    [SerializeField] private BasketSO basketType = null;    // Scriptable Objects eriþir  
    [SerializeField] private GameObject lockedPlane;
    void Start()
    {
        lockedPlane.GetComponent<MeshRenderer>().material.color = buildingType.lockedColor;
    }    
    void Update()
    {
        if (!buildingType.locked)
        {
            lockedPlane.GetComponent<MeshRenderer>().material.color = buildingType.unlockedColor;    
            // Para üretme
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.gamemanagerInstance.startTheGame = false;
            if (basketType.totalFruit > basketType.minFruit & buildingType.locked)
            {
                basketType.totalFruit--;    // Toplanan meyve sayýsý azalt
                buildingType.currentValue++;    // Binanýn toplanan meyve sayýsýný arttýr
                AudioController.audioControllerInstance.Play("BuildingSound");
                if (buildingType.currentValue == buildingType.maxValue)
                {
                    // Binan toplanan meyve sayýsý max olduðu zaman kilit kaldýr ve ses çal
                    AudioController.audioControllerInstance.Play("BuildingOpenedSound");
                    buildingType.locked = false;
                }
            }
            else
            {
                Debug.Log("Eksik Malzeme veya Bina açýldý");
            }
        }        
    }
}
