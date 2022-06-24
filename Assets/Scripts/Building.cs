using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingSO buildingType = null;    // Scriptable Objects eri�ir  
    [SerializeField] private BasketSO basketType = null;    // Scriptable Objects eri�ir  
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
            // Para �retme
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.gamemanagerInstance.startTheGame = false;
            if (basketType.totalFruit > basketType.minFruit & buildingType.locked)
            {
                basketType.totalFruit--;    // Toplanan meyve say�s� azalt
                buildingType.currentValue++;    // Binan�n toplanan meyve say�s�n� artt�r
                AudioController.audioControllerInstance.Play("BuildingSound");
                if (buildingType.currentValue == buildingType.maxValue)
                {
                    // Binan toplanan meyve say�s� max oldu�u zaman kilit kald�r ve ses �al
                    AudioController.audioControllerInstance.Play("BuildingOpenedSound");
                    buildingType.locked = false;
                }
            }
            else
            {
                Debug.Log("Eksik Malzeme veya Bina a��ld�");
            }
        }        
    }
}
