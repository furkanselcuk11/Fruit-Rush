using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{    
    [SerializeField] private float speed=500f;
    [SerializeField] float turnSpeed = 100f;    // Obejelerin dönme hýzý

    private Vector3 startpos1;
    private Vector3 startpos2;

    public Transform currentPoint;
    void Start()
    {        
        startpos1 = transform.position;
        startpos2 = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
    }
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, startpos1, Time.fixedDeltaTime * speed);
        if (transform.position == startpos1)
        {
            startpos1 = startpos2;
            if (startpos1 == startpos2)
            {
                startpos2 = transform.position;
            }
        }
        transform.Rotate(0f, 0f, turnSpeed * Time.fixedDeltaTime);
    }
}
