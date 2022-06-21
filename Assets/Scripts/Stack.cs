using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            // Wood objesi ekler
            GameManager.gamemanagerInstance.Add(other.gameObject);
        }
        if (other.CompareTag("Fail"))
        {
            // Toplanan tüm objeler yok olur
            GameManager.gamemanagerInstance.Fail(other.gameObject);
            Debug.Log("Fail");
        }
        if (other.CompareTag("Finish"))
        {
            
        }
    }
}