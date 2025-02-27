using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotateVector;
    [SerializeField]
    private float rotateMultiplier = 3f;


    private void LateUpdate()
    {
        transform.Rotate(rotateVector * rotateMultiplier * Time.deltaTime, Space.World);
    }
}
