using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] float turnSpeed = 100f;    // Obejelerin dönme hýzý
    public float value = 1;
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.Rotate(0f, turnSpeed * Time.fixedDeltaTime, 0f);
    }

}
