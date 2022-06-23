using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitRotate : MonoBehaviour
{
    [SerializeField] float turnSpeed = 100f;    // Obejelerin dönme hızı
    [SerializeField] bool turnDirection;  // Obejelerin dönme yönü

    // Update is called once per frame
    void Update()
    {
        if (turnDirection == false)
        {   // Eger dönme yönü false ise saga dön
            transform.Rotate(0f, turnSpeed * Time.deltaTime, 0f);
        }
        else
        {   // Eger dönme yönü false ise sola dön
            transform.Rotate(0f, -turnSpeed * Time.deltaTime, 0f);
        }
    }
}
