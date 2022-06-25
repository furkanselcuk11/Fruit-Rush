using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            // Wood objesi ekler
            GameManager.gamemanagerInstance.Add(other.gameObject);
        }
        if (other.CompareTag("Fail"))
        {
            // Toplanan t�m objeler yok olur
            GameManager.gamemanagerInstance.Fail(other.gameObject);
        }
        if (other.CompareTag("Finish"))
        {
            
        }
    }
}