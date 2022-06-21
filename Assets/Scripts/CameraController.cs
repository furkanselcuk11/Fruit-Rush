using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target; // Takip edilecek obje
    [SerializeField] private Vector3 _offset;   // Kamera ile takip edilecek obje aras�ndaki mesafe
    [SerializeField] private float _chaseSpeed = 5; // Takip etme h�z�

    private void LateUpdate()
    {
        Vector3 desPos = _target.position + _offset;  // Kamera ile takip edilen obje aras�ndaki mesafe
        transform.position = Vector3.Lerp(transform.position, desPos, _chaseSpeed);   // Kamera pozisyonu yumu�ak ge�i� ile aradaki mesafe kadar uzaktan takip eder
    }
}